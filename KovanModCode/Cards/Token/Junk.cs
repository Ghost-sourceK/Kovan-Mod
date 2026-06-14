using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.KeyWords;
using KovanMod.KovanModCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace KovanMod.KovanModCode.Cards.Token;

[Pool(typeof(TokenCardPool))]
public sealed class Junk : KovanModCardModel
{
    public Junk() : base(0, CardType.Skill, CardRarity.Token, TargetType.None)
    {
        WithBlock(4, 2);
        WithCards(1,1);
        WithVar("nails", 2, 2);
        WithKeywords(CardKeyword.Exhaust, CardKeyword.Retain);
        WithTip(NailsKeyWord.Nails);
    }
    
    public static IEnumerable<Junk?> Create(Player owner, int amount, ICombatState? combatState)
    {
        var junkList = new List<Junk?>();
        for (int index = 0; index < amount; ++index)
            junkList.Add(combatState?.CreateCard<Junk>(owner));
        return junkList;
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var enemies = Owner.Creature.CombatState?.HittableEnemies.Where(e => e.IsAlive).ToList();
        if (enemies is not { Count: > 0 })
            return;
        var randomTarget = enemies[Owner.RunState.Rng.Shuffle.NextInt(enemies.Count)];
        
        int roll = Owner.RunState.Rng.Shuffle.NextInt(3);
        switch (roll)
        {
            case 0:
                await CommonActions.CardBlock(this, cardPlay);
                break;
            case 1:
                await PowerCmd.Apply<NailPower>(choiceContext, randomTarget, DynamicVars["nails"].IntValue, Owner.Creature, this);
                break;
            case 2:
                await CommonActions.Draw(this, choiceContext);
                break;
        }
    }
}