using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace KovanMod.KovanModCode.Cards.Basic;

[Pool(typeof(KovanModCardPool))]
public sealed class DefendKovan : KovanModCardModel
{
    public DefendKovan() : base(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
    {
        WithBlock(5, 3);
        WithTags(CardTag.Defend);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardBlock(this, cardPlay);
    }
}