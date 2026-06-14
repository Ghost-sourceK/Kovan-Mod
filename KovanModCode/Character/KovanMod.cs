using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using KovanMod.KovanModCode.Extensions;
using Godot;
using KovanMod.KovanModCode.Cards.Basic;
using KovanMod.KovanModCode.Relics;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;

namespace KovanMod.KovanModCode.Character;

public class KovanMod : PlaceholderCharacterModel
{
    public const string CharacterId = "KovanMod";

    public static readonly Color Color = new("95a7ad");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Neutral;
    public override int StartingHp => 70;

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<StrikeKovan>(),
        ModelDb.Card<StrikeKovan>(),
        ModelDb.Card<StrikeKovan>(),
        ModelDb.Card<StrikeKovan>(),
        ModelDb.Card<DefendKovan>(),
        ModelDb.Card<DefendKovan>(),
        ModelDb.Card<DefendKovan>(),
        ModelDb.Card<DefendKovan>(),
        ModelDb.Card<PinDown>(),
        ModelDb.Card<RipFree>(),
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<FirstStrike>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<KovanModCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<KovanModRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<KovanModPotionPool>();

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets.
        These are just some of the simplest assets, given some placeholders to differentiate your character with.
        You don't have to, but you're suggested to rename these images. */
    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }

    public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();
}