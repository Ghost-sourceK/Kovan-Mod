using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Cards.Basic;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.Enchantments;
using KovanMod.KovanModCode.KeyWords;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace KovanMod.KovanModCode.Cards.Common;

[Pool(typeof(KovanModCardPool))]
public sealed class Salvage : KovanModCardModel
{
    public Salvage() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithTip(BarbedKeyWord.Barbed);
        WithUpgradingCardTip<StrikeKovan>();
    }
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, 1);
        CardModel? original = (await CardSelectCmd.FromHand(choiceContext, Owner, prefs, null, this)).FirstOrDefault();
        if (original == null)
            return;
        CardModel? card = CombatState?.CreateCard<StrikeKovan>(Owner);
        if (card != null)
        {
            CardCmd.Enchant<Barbed>(card, 1);
            CardCmd.ApplyKeyword(card, CardKeyword.Exhaust);
            if (IsUpgraded)
                CardCmd.Upgrade(card);
            await CardCmd.Transform(original, card);
        }
    }
}