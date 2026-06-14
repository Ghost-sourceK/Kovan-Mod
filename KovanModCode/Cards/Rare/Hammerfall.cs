using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.KeyWords;
using KovanMod.KovanModCode.Powers;
using KovanMod.KovanModCode.Utility;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace KovanMod.KovanModCode.Cards.Rare;

[Pool(typeof(KovanModCardPool))]
public sealed class Hammerfall : KovanModCardModel
{
    public Hammerfall() : base(3, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
    {
        WithDamage(32, 6);
        WithVar("nails", 6, 4);
        WithTip(NailsKeyWord.Nails);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_attack_blunt").Execute(choiceContext);

        if (cardPlay.Target != null)
        {
            await NailsSystem.TriggerNails(choiceContext, cardPlay.Target, Owner.Creature, false);
        
            await PowerCmd.Apply<NailPower>(choiceContext, cardPlay.Target, DynamicVars["nails"].IntValue, Owner.Creature, this);
        }
    }
}