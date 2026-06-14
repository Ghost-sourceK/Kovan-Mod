using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace KovanMod.KovanModCode.Cards.Uncommon;

[Pool(typeof(KovanModCardPool))]
public sealed class TitForTat : KovanModCardModel
{
    public TitForTat() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithCostUpgradeBy(-1);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var strAmount = cardPlay.Target?.GetPower<StrengthPower>();
        if (strAmount is not { Amount: > 0 })
            return;
        int dexAmount = strAmount.Amount;
        
        await PowerCmd.Remove(strAmount);

        await PowerCmd.Apply<DexterityPower>(choiceContext, Owner.Creature, dexAmount, Owner.Creature, this);
    }
}