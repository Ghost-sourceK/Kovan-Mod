using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.KeyWords;
using KovanMod.KovanModCode.Utility;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace KovanMod.KovanModCode.Cards.Common;

[Pool(typeof(KovanModCardPool))]
public sealed class RustyPlates : KovanModCardModel
{
    public RustyPlates() : base(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithBlock(5, 2);
        WithPower<WeakPower>(1,1);
        WithTip(NailsKeyWord.Nails);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardBlock(this, cardPlay);
        
        if (NailsSystem.HasNails(cardPlay.Target))
        {
            if (cardPlay.Target != null)
                await PowerCmd.Apply<WeakPower>(choiceContext, cardPlay.Target, DynamicVars.Weak.IntValue, Owner.Creature, this);
        }
    }
}