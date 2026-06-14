using BaseLib.Abstracts;
using KovanMod.KovanModCode.Extensions;
using Godot;

namespace KovanMod.KovanModCode.Character;

public class KovanModRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => KovanMod.Color;

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}