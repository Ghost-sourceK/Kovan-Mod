using KovanMod.KovanModCode.Cards.Token;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace KovanMod.KovanModCode.Powers;

public sealed class RecyclePower : KovanModPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override int ModifyCardPlayCount(CardModel card, Creature? target, int playCount)
    {
        if (card is not Junk)
            return playCount;
        
        return card.Owner.Creature != Owner || CombatManager.Instance.History.CardPlaysStarted.Count(e => e.Actor == Owner && e.CardPlay.IsFirstInSeries && e.HappenedThisTurn(CombatState)) >= Amount ? playCount : playCount + 1;
    }

    public override Task AfterModifyingCardPlayCount(CardModel card)
    {
        if (card is Junk)
            Flash();
        return Task.CompletedTask;
    }
}