using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace KovanMod.KovanModCode.Cards.Power;

[Pool(typeof(KovanModCardPool))]
public sealed class Determination : KovanModCardModel
{
    public 
        Determination() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<DeterminationPower>(1);
        WithVar("amount", 1, 1);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.ApplySelf<DeterminationPower>(choiceContext, this, DynamicVars["amount"].IntValue);
    }
}