using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace KovanMod.KovanModCode.KeyWords;

public static class ExpertiseKeyWord
{
    [CustomEnum] [KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword Expertise;

    public static bool IsExpertise(this CardModel card)
    {
        return card.Keywords.Contains(Expertise);
    }
}