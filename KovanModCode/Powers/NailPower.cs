using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace KovanMod.KovanModCode.Powers;

public sealed class NailPower : KovanModPower
{
    public override PowerType Type => PowerType.Debuff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task BeforeSideTurnEndEarly(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner))
            return;

        if (Amount <= 1)
        {
            await PowerCmd.Remove(this);
            return;
        }

        if (Owner.IsAlive)
            await PowerCmd.Decrement(this);
    }
    
    public async Task<int> Detonate(PlayerChoiceContext choiceContext, Creature source, bool consume = true)
    {
        int damage = Amount;

        if (damage <= 0)
            return 0;

        await CreatureCmd.Damage(choiceContext, Owner, damage, ValueProp.SkipHurtAnim, source, null);

        if (consume)
            await PowerCmd.Remove(this);

        return damage;
    }
    
    public async Task<bool> Consume(PlayerChoiceContext choiceContext, Creature source, int amount)
    {
        if (Amount < amount)
            return false;
        
        if (Amount == amount)
            await PowerCmd.Remove(this);
        else
            await PowerCmd.ModifyAmount(choiceContext, this, -amount, source, null);
        
        return true;
    }
}