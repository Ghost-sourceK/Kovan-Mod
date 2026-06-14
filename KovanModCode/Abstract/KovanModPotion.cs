using BaseLib.Abstracts;
using BaseLib.Utils;
using KovanMod.KovanModCode.Character;

namespace KovanMod.KovanModCode.Potions;

[Pool(typeof(KovanModPotionPool))]
public abstract class KovanModPotion : CustomPotionModel;