using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace KovanMod.KovanModCode.Powers;

public sealed class DeterminationPower : KovanModPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    
    private bool _isTriggering;

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (_isTriggering)
            return;
        _isTriggering = true;
        
        try
        {
            if (amount <= 0 || applier != Owner || power is not StrengthPower)
                return;
            
            Flash();
            await PowerCmd.Apply<StrengthPower>(choiceContext, Owner, amount, Owner, null);
        }
        finally
        {
            _isTriggering = false;
        }
    }
}