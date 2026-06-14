using BaseLib.Utils;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.Powers;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;

namespace KovanMod.KovanModCode.Relics;

[Pool(typeof(KovanModRelicPool))]
public sealed class FirstStrike : KovanModRelic
{
    public override RelicRarity Rarity => RelicRarity.Starter;

    private bool _triggeredThisCombat;

    public override Task AfterCombatEnd(CombatRoom _)
    {
        _triggeredThisCombat = false;
        return Task.CompletedTask;
    }

    public override decimal ModifyPowerAmountGivenAdditive(PowerModel power, Creature giver, decimal amount, Creature? target, CardModel? cardSource)
    {
        if (_triggeredThisCombat)
            return 0;
        if (power is not NailPower)
            return 0;
        if (giver != Owner.Creature)
            return 0;
        
        return 2m;
    }

    public override Task AfterModifyingPowerAmountGiven(PowerModel power)
    {
        if (power is NailPower)
        {
            _triggeredThisCombat = true;
            Flash();
        }

        return Task.CompletedTask;
    }
    
    /*public override RelicModel? GetUpgradeReplacement()
    {
        return ModelDb.Relic<HollowHammer>();
    }*/
}