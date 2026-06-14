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
public sealed class Ricochet : KovanModCardModel
{
    public Ricochet() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(6, 2);
        WithVar("nails", 1, 1);
        WithTip(NailsKeyWord.Nails);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitVfxNode(t => NShivThrowVfx.Create(Owner.Creature, t, Colors.Green)).Execute(choiceContext);
        if (cardPlay.Target != null)
            await PowerCmd.Apply<NailPower>(choiceContext, cardPlay.Target, DynamicVars["nails"].IntValue, Owner.Creature, this);
        
        var enemies = Owner.Creature.CombatState?.HittableEnemies.Where(e => e.IsAlive && e != cardPlay.Target).ToList();
        if (enemies is not { Count: > 0 })
            return;
        var randomTarget = enemies[Owner.RunState.Rng.Shuffle.NextInt(enemies.Count)];
        await CreatureCmd.Damage(choiceContext, randomTarget, DynamicVars.Damage, Owner.Creature, this);
        VfxCmd.PlayOnCreature(randomTarget, "vfx/vfx_attack_blunt");
        await PowerCmd.Apply<NailPower>(choiceContext, randomTarget, DynamicVars["nails"].IntValue, Owner.Creature, this);
    }
}