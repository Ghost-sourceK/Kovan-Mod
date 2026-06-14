using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.KeyWords;
using KovanMod.KovanModCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace KovanMod.KovanModCode.Cards.Common;

[Pool(typeof(KovanModCardPool))]
public sealed class ScatteredNails : KovanModCardModel
{
    public ScatteredNails() : base(1, CardType.Skill, CardRarity.Common, TargetType.AllEnemies)
    {
        WithVar("nails", 2, 2);
        WithTip(NailsKeyWord.Nails);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CardPileCmd.Draw(choiceContext, 2, Owner);
        
        var enemies = Owner.Creature.CombatState?.HittableEnemies.Where(e => e.IsAlive).ToList();
        if (enemies != null)
            foreach (var enemy in enemies)
            {
                await PowerCmd.Apply<NailPower>(choiceContext, enemy, DynamicVars["nails"].IntValue, Owner.Creature, this);
            }
    }
}