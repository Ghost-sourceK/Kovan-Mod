using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.KeyWords;
using KovanMod.KovanModCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace KovanMod.KovanModCode.Cards.Common;

[Pool(typeof(KovanModCardPool))]
public sealed class TwistKnife : KovanModCardModel
{
    public TwistKnife() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(7, 3);
        WithVar("nails", 3, 1);
        WithTip(NailsKeyWord.Nails);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_attack_blunt").Execute(choiceContext);

        var enemyNails = cardPlay.Target?.GetPower<NailPower>();
        int nailsApply = enemyNails is { Amount: > 0 } ? DynamicVars["nails"].IntValue : 1;
        if (cardPlay.Target != null)
            await PowerCmd.Apply<NailPower>(choiceContext, cardPlay.Target, nailsApply, Owner.Creature, this);
    }
}