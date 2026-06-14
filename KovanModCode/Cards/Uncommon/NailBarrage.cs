using BaseLib.Utils;
using Godot;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace KovanMod.KovanModCode.Cards.Uncommon;

[Pool(typeof(TokenCardPool))]
public sealed class NailBarrage : KovanModCardModel
{
    public NailBarrage() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.RandomEnemy)
    {
        WithDamage(10, 3);
        WithVar("nails", 2, 1);
    }
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var enemies = Owner.Creature.CombatState?.HittableEnemies.Where(e => e.IsAlive && e != cardPlay.Target).ToList();
        if (enemies is not { Count: > 0 })
            return;
        var randomTarget = enemies[Owner.RunState.Rng.Shuffle.NextInt(enemies.Count)];
        
        for (int i = 0; i < 3; i++)
        {
            NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NShivThrowVfx.Create(Owner.Creature, randomTarget, Colors.Green));
            await CreatureCmd.Damage(choiceContext, randomTarget, DynamicVars.Damage, Owner.Creature, this);
        
            await PowerCmd.Apply<NailPower>(choiceContext, randomTarget, DynamicVars["nails"].IntValue, Owner.Creature, this);
        }
    }
}