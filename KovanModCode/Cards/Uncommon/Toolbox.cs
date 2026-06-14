using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.KeyWords;
using KovanMod.KovanModCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace KovanMod.KovanModCode.Cards.Uncommon;

[Pool(typeof(KovanModCardPool))]
public sealed class Toolbox : KovanModCardModel
{
    public Toolbox() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithVar("nails", 5, 3);
        WithTip(NailsKeyWord.Nails);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Target != null)
        {
            await PowerCmd.Apply<NailPower>(choiceContext, cardPlay.Target, DynamicVars["nails"].IntValue, Owner.Creature, this);
            
            await PowerCmd.Apply<DrawCardsNextTurnPower>(choiceContext, Owner.Creature, 2, Owner.Creature, this);
            
            await PowerCmd.Apply<StrengthPower>(choiceContext, cardPlay.Target, 1, Owner.Creature, this);
        }
    }
}