using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.Extensions;
using KovanMod.KovanModCode.Utility;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace KovanMod.KovanModCode.Abstract;

[Pool(typeof(KovanModCardPool))]
public abstract class KovanModCardModel(int canonicalEnergyCost, CardType type, CardRarity rarity, TargetType targetType, bool shouldShowInCardLibrary = true) :
    ConstructedCardModel(canonicalEnergyCost, type, rarity, targetType, shouldShowInCardLibrary)
{
    //Image size:
    //Normal art: 1000x760 (Using 500x380 should also work, it will simply be scaled.)
    //Full art: 606x852
    //public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    public override string CustomPortraitPath
    {
        get
        {
            string customPath = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
            
            return Godot.ResourceLoader.Exists(customPath) ? customPath : VanillaArtPool.Get(Id.Entry, rarity);
        }
    }

    //Smaller variants of card images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190

    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    //public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string PortraitPath
    {
        get
        {
            string customPath = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
            
            return Godot.ResourceLoader.Exists(customPath) ? customPath : VanillaArtPool.Get(Id.Entry, rarity);
        }
    }
    
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
}