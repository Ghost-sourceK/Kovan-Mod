using KovanMod.KovanModCode.Powers;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace KovanMod.KovanModCode.Utility;

public static class NailsSystem
{
    public static bool HasNails(Creature? creature)
    {
        var power = creature?.GetPower<NailPower>();
        return power is { Amount: > 0 };
    }
    
    public static async Task<int> TriggerNails(PlayerChoiceContext choiceContext, Creature target, Creature source, bool consume = true)
    {
        var power = target.GetPower<NailPower>();

        if (power == null)
            return 0;

        return await power.Detonate(choiceContext, source, consume);
    }
}