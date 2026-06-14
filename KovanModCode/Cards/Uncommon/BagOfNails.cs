using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.KeyWords;
using KovanMod.KovanModCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace KovanMod.KovanModCode.Cards.Uncommon;

[Pool(typeof(KovanModCardPool))]
public sealed class BagOfNails : KovanModCardModel
{
    public BagOfNails() : base(5, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithVar("nails", 8, 4);
        WithEnergyTip();
        WithTip(NailsKeyWord.Nails);
    }

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (amount <= 0 || power is not NailPower)
            return;
            
        int currentCost = EnergyCost.GetAmountToSpend();

        if (currentCost <= 0)
            return;
        
        EnergyCost.SetThisCombat(currentCost - 1, true);
    }
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Target != null)
        {
            await PowerCmd.Apply<NailPower>(choiceContext, cardPlay.Target, DynamicVars["nails"].IntValue, Owner.Creature, this);
        }
    }
}