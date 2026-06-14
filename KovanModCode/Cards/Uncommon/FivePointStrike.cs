using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.KeyWords;
using KovanMod.KovanModCode.Powers;
using KovanMod.KovanModCode.Utility;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace KovanMod.KovanModCode.Cards.Uncommon;

[Pool(typeof(KovanModCardPool))]
public sealed class FivePointStrike : KovanModCardModel
{
    public FivePointStrike() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(8, 2);
        WithVar("nails", 3, 2);
        WithTip(NailsKeyWord.Nails);
        WithTags(CardTag.Strike);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitCount(2).WithHitFx("vfx/vfx_attack_blunt").Execute(choiceContext);

        if (cardPlay.Target != null)
        {
            await NailsSystem.TriggerNails(choiceContext, cardPlay.Target, Owner.Creature);
            
            await PowerCmd.Apply<NailPower>(choiceContext, cardPlay.Target, DynamicVars["nails"].IntValue, Owner.Creature, this);
        }
    }
}