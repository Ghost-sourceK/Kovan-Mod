using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace KovanMod.KovanModCode.Cards.Power;

[Pool(typeof(KovanModCardPool))]
public sealed class Recycle : KovanModCardModel
{
    public 
        Recycle() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<RecyclePower>(1);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.ApplySelf<RecyclePower>(choiceContext, this, 1);
    }
    
    protected override void OnUpgrade() => AddKeyword(CardKeyword.Innate);
}