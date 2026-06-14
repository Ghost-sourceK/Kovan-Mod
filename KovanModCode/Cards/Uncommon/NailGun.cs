using BaseLib.Utils;
using KovanMod.KovanModCode.Abstract;
using KovanMod.KovanModCode.Character;
using KovanMod.KovanModCode.KeyWords;
using KovanMod.KovanModCode.Powers;
using KovanMod.KovanModCode.Utility;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Cards;

namespace KovanMod.KovanModCode.Cards.Uncommon;

[Pool(typeof(KovanModCardPool))]
public sealed class NailGun : KovanModCardModel
{
    public NailGun() : base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(10, 3);
        WithVar("nails", 2, 1);
        WithTip(NailsKeyWord.Nails);
        WithTip(typeof(Dazed));
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_attack_blunt").Execute(choiceContext);
        
        if (cardPlay.Target != null)
            await PowerCmd.Apply<NailPower>(choiceContext, cardPlay.Target, DynamicVars["nails"].IntValue, Owner.Creature, this);
        
        var currentCost = EnergyCost.GetAmountToSpend() + 1;
        EnergyCost.SetThisTurn(currentCost);
        
        await CreateCardCmd.GiveCards<Dazed>(Owner, 1, PileType.Draw, CardPilePosition.Random, animationTime: 0.1f);
        await Cmd.Wait(0.25f);
    }
    
    protected override PileType GetResultPileTypeForCardPlay()
    {
        PileType pileTypeForCardPlay = base.GetResultPileTypeForCardPlay();
        return pileTypeForCardPlay != PileType.Discard ? pileTypeForCardPlay : PileType.Hand;
    }
}