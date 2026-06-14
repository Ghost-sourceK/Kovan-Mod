using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.KeyWords;
using KovanMod.KovanModCode.Utility;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace KovanMod.KovanModCode.Cards.Common;

[Pool(typeof(KovanModCardPool))]
public sealed class IronStrike : KovanModCardModel
{
    public IronStrike() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(6, 2);
        WithVars(new RepeatVar(2));
        WithTip(NailsKeyWord.Nails);
        WithTags(CardTag.Strike);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int hitCount =  NailsSystem.HasNails(cardPlay.Target) ? DynamicVars.Repeat.IntValue : 1;
        if (cardPlay.Target != null)
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue).WithHitCount(hitCount).FromCard(this).Targeting(cardPlay.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
    }
}