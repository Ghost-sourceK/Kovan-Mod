using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Cards.Token;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.Utility;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace KovanMod.KovanModCode.Cards.Common;

[Pool(typeof(KovanModCardPool))]
public sealed class Scavenge : KovanModCardModel
{
    public Scavenge() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(8, 1);
        WithCards(1,1);
        WithTip(typeof(Junk));
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_attack_blunt").Execute(choiceContext);
        
        await CreateCardCmd.GiveCards<Junk>(Owner, DynamicVars.Cards.IntValue, PileType.Draw, CardPilePosition.Random, animationTime: 0.1f);
    }
}