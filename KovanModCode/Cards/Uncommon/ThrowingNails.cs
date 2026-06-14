using BaseLib.Utils;
using Godot;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.Enchantments;
using KovanMod.KovanModCode.KeyWords;
using KovanMod.KovanModCode.Powers;
using KovanMod.KovanModCode.Utility;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace KovanMod.KovanModCode.Cards.Uncommon;

[Pool(typeof(KovanModCardPool))]
public sealed class ThrowingNails : KovanModCardModel
{
    private int _playsThisCombat;
    
    public ThrowingNails() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.RandomEnemy)
    {
        WithDamage(10, 3);
        WithVar("nails", 2, 1);
        WithUpgradingCardTip<NailBarrage>();
        WithTip(ExpertiseKeyWord.Expertise);
    }
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        _playsThisCombat++;
    
        var enemies = Owner.Creature.CombatState?.HittableEnemies.Where(e => e.IsAlive && e != cardPlay.Target).ToList();
        if (enemies is not { Count: > 0 })
            return;
        var randomTarget = enemies[Owner.RunState.Rng.Shuffle.NextInt(enemies.Count)];
        
        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NShivThrowVfx.Create(Owner.Creature, randomTarget, Colors.Green));
        await CreatureCmd.Damage(choiceContext, randomTarget, DynamicVars.Damage, Owner.Creature, this);
        
        await PowerCmd.Apply<NailPower>(choiceContext, randomTarget, DynamicVars["nails"].IntValue, Owner.Creature, this);
        
        if (_playsThisCombat >= 2)
        {
            CardModel? card = CombatState?.CreateCard<NailBarrage>(Owner);
            if (card != null)
            {
                if (IsUpgraded)
                    CardCmd.Upgrade(card);
                await CreateCardCmd.GiveCard<NailBarrage>(Owner, PileType.Draw, CardPilePosition.Random);
                await CardCmd.Exhaust(choiceContext, this);
            }
        }
        else
            await CardPileCmd.Add(this, PileType.Draw, CardPilePosition.Random);
    }
    
    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel cardModel, bool causedByEthereal)
    {
        if (cardModel != this || CombatState == null)
            return;
        if (causedByEthereal)
            return;
        
        await PurgeCardCmd.Purge(this);
    }
}