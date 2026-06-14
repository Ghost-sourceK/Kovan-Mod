using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.KeyWords;
using KovanMod.KovanModCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace KovanMod.KovanModCode.Cards.Uncommon;

[Pool(typeof(KovanModCardPool))]
public sealed class ChainReaction : KovanModCardModel
{
    protected override bool ShouldGlowGoldInternal => AnyEnemyHas6Nails;
    
    public ChainReaction() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies)
    {
        WithCards(3,1);
        WithTip(NailsKeyWord.Nails);
    }
    
    private bool AnyEnemyHas6Nails => Owner.Creature.CombatState?.HittableEnemies.Where(e => e.IsAlive).Any(e => e.GetPower<NailPower>() is { Amount: >= 6 }) == true;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var validTargets = Owner.Creature.CombatState?.HittableEnemies.Where(e => e.IsAlive && e.GetPower<NailPower>() is { Amount: >= 6 }).ToList();
        if (validTargets is not { Count: > 0 })
            return;
        var target = validTargets[Owner.RunState.Rng.Shuffle.NextInt(validTargets.Count)];
        if (target.GetPower<NailPower>() is not { } nails)
            return;
        
        if (await nails.Consume(choiceContext, Owner.Creature, 6))
        {
            await CommonActions.Draw(this, choiceContext);
        }
    }
}