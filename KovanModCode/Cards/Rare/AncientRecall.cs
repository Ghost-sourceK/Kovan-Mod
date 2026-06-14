using BaseLib.Utils;
using Godot;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.KeyWords;
using KovanMod.KovanModCode.Powers;
using KovanMod.KovanModCode.Utility;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace KovanMod.KovanModCode.Cards.Rare;

[Pool(typeof(KovanModCardPool))]
public sealed class AncientRecall : KovanModCardModel
{
    public AncientRecall() : base(2, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy)
    {
        WithCostUpgradeBy(-1);
        WithTip(NailsKeyWord.Nails);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Target == null)
            return;
        
        int totalNails = 0;
        
        var enemies = Owner.Creature.CombatState?.HittableEnemies.Where(e => e.IsAlive).ToList();
        if (enemies is not { Count: > 0 })
            return;

        SfxCmd.Play("event:/sfx/characters/silent/silent_dagger_spray");
        foreach (var enemy in enemies)
        {
            if(enemy.HasPower<NailPower>())
                NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NDaggerSprayImpactVfx.Create(enemy, new Color("#b1ccca"), false));
            totalNails += await NailsSystem.TriggerNails(choiceContext, enemy, Owner.Creature);
        }
        await Cmd.Wait(0.4f);
        SfxCmd.Play("event:/sfx/characters/silent/silent_dagger_spray");
        if (totalNails > 0)
        {
            NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NDaggerSprayFlurryVfx.Create(Owner.Creature, new Color("#b1ccca"), goingRight: true));
            await PowerCmd.Apply<NailPower>(choiceContext, cardPlay.Target, totalNails, Owner.Creature, this);
        }
    }
}