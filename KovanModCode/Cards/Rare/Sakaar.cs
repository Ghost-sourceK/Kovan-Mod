using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Cards.Token;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.Utility;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace KovanMod.KovanModCode.Cards.Rare;

[Pool(typeof(KovanModCardPool))]
public sealed class Sakaar : KovanModCardModel
{
    public Sakaar() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithUpgradingCardTip<Junk>();
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int num = CardPile.MaxCardsInHand - CardPile.GetCards(Owner, PileType.Hand).Count();
        var cards = new List<CardModel?>();
        for (int index = 0; index < num; ++index)
            cards.Add(CombatState?.CreateCard<Junk>(Owner));
        if (IsUpgraded)
        {
            foreach (var card in cards.OfType<Junk>())
            {
                CardCmd.Upgrade(card);
            }
        }
        await CardPileCmd.AddGeneratedCardsToCombat((IEnumerable<CardModel>) cards, PileType.Hand, Owner);
        
        var enemies = Owner.Creature.CombatState?.HittableEnemies.Where(e => e.IsAlive).ToList();
        if (enemies is not { Count: > 0 })
            return;
        foreach (var enemy in enemies)
        {
            await PowerCmd.Apply<StrengthPower>(choiceContext, enemy, 1, Owner.Creature, this);
        }
        
    }
}