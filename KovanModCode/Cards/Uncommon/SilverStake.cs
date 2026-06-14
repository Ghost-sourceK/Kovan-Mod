using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.KeyWords;
using KovanMod.KovanModCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace KovanMod.KovanModCode.Cards.Uncommon;

[Pool(typeof(KovanModCardPool))]
public sealed class SilverStake : KovanModCardModel
{
    public SilverStake() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(10, 4);
        WithTip(NailsKeyWord.Nails);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_attack_blunt").Execute(choiceContext);

        var nails = cardPlay.Target?.GetPower<NailPower>();

        if (nails is not { Amount: > 0 })
            return;
        
        int amount = nails.Amount;
        
        var enemies = Owner.Creature.CombatState?.HittableEnemies.Where(e => e.IsAlive).ToList();
        if (enemies is { Count: 0 })
            return;

        var randomTarget = enemies?[Owner.RunState.Rng.Shuffle.NextInt(enemies.Count)];

        if (randomTarget != null)
            await PowerCmd.Apply<NailPower>(choiceContext, randomTarget, amount, Owner.Creature, this);
    }
}