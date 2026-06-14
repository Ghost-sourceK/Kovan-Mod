using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace KovanMod.KovanModCode.KeyWords;

public static class BarbedKeyWord
{
    [CustomEnum] [KeywordProperties(AutoKeywordPosition.After)]
    public static CardKeyword Barbed;

    public static bool IsBarbed(this CardModel card)
    {
        return card.Keywords.Contains(Barbed);
    }
}