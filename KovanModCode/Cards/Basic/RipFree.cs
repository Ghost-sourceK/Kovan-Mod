using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.KeyWords;
using KovanMod.KovanModCode.Utility;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace KovanMod.KovanModCode.Cards.Basic;

[Pool(typeof(KovanModCardPool))]
public sealed class RipFree : KovanModCardModel
{
    public RipFree() : base(0, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
    {
        WithDamage(3, 3);
        WithTip(NailsKeyWord.Nails);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        
        if (cardPlay.Target != null)
        {
            await NailsSystem.TriggerNails(choiceContext, cardPlay.Target, Owner.Creature);
        }
    }
}