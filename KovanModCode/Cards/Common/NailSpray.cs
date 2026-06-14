using BaseLib.Utils;
using Godot;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.KeyWords;
using KovanMod.KovanModCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace KovanMod.KovanModCode.Cards.Common;

[Pool(typeof(KovanModCardPool))]
public sealed class NailSpray : KovanModCardModel
{
    public NailSpray() : base(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies)
    {
        WithDamage(7, 3);
        WithVar("nails", 1, 1);
        WithTip(NailsKeyWord.Nails);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_giant_horizontal_slash").Execute(choiceContext);
        if (cardPlay.Target != null)
        {
            NDaggerSprayImpactVfx.Create(cardPlay.Target, new Color("#b1ccca"), true);
        }
        
        var enemies = Owner.Creature.CombatState?.HittableEnemies.Where(e => e.IsAlive).ToList();
        if (enemies is not { Count: > 0 })
            return;
        await PowerCmd.Apply<NailPower>(choiceContext, enemies, DynamicVars["nails"].IntValue, Owner.Creature, this);
    }
}