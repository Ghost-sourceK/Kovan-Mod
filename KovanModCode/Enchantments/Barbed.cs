using BaseLib.Abstracts;
using KovanMod.KovanModCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace KovanMod.KovanModCode.Enchantments;

public sealed class Barbed : CustomEnchantmentModel
{
    protected override string CustomIconPath => "res://KovanMod/images/enchantments/KovanMod-barbed.png";
    
    public override bool HasExtraCardText => true;
    
    public override bool ShowAmount => false;

    public override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay? cardPlay)
    {
        if (cardPlay?.Target == null)
            return;

        await PowerCmd.Apply<NailPower>(choiceContext, cardPlay.Target, 3, Card.Owner.Creature, Card);
    }

    public override decimal EnchantDamageAdditive(decimal originalDamage, ValueProp props)
    {
        return !props.IsPoweredAttack() ? 0 : 2;
    }
}