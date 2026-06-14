using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace KovanMod.KovanModCode.KeyWords;

public static class NailsKeyWord
{
    [CustomEnum] [KeywordProperties(AutoKeywordPosition.After)]
    public static CardKeyword Nails;

    public static bool IsNails(this CardModel card)
    {
        return card.Keywords.Contains(Nails);
    }
}