using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.KeyWords;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace KovanMod.KovanModCode.Cards.Common;

[Pool(typeof(KovanModCardPool))]
public sealed class BodyArmor : KovanModCardModel
{
    public BodyArmor() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithBlock(6, 2);
        WithCards(1,1);
        WithTip(NailsKeyWord.Nails);
        WithTip(CardKeyword.Retain);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardBlock(this, cardPlay);
        
        CardSelectorPrefs prefs = new CardSelectorPrefs( new LocString("cards", "KOVANMOD-RETAIN_SELECT_PROMPT"), 0, DynamicVars.Cards.IntValue);
        var list = (await CardSelectCmd.FromHand(choiceContext, Owner, prefs, RetainFilter, this)).ToList();
        if (list.Count == 0)
            return;
        foreach (CardModel cardModel in list)
            cardModel.GiveSingleTurnRetain();
    }
    
    private static bool RetainFilter(CardModel card) => !card.ShouldRetainThisTurn;
}