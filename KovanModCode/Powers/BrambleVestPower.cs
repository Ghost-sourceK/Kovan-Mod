using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace KovanMod.KovanModCode.Powers;

public sealed class BrambleVestPower : KovanModPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult damageResult, ValueProp props, Creature? dealer, CardModel? card)
    {
        if (target != Owner)
            return;
        if (dealer == null)
            return;
        if (!props.IsPoweredAttack())
            return;

        await PowerCmd.Apply<NailPower>(choiceContext, dealer, Amount, Owner, null);
    }

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (Owner.Side == side)
            return;

        await PowerCmd.Remove(this);
    }
}