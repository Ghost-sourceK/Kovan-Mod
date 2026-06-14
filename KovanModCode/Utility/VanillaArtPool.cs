using Godot;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace KovanMod.KovanModCode.Utility;

public static class VanillaArtPool
{
    private static readonly string[] BasePaths =
    {
        "res://images/packed/card_portraits/ironclad",
        "res://images/packed/card_portraits/silent",
        "res://images/packed/card_portraits/defect"
    };

    private static List<string>? _cache;
    
    private static readonly Dictionary<string, string> ReservedArts = new()
    {
        { "strike", "res://images/packed/card_portraits/ironclad/strike_ironclad.png" },
        { "defend", "res://images/packed/card_portraits/ironclad/defend_ironclad.png" }
    };


    private static List<string> Load()
    {
        var list = new List<string>();
        
        foreach (var basePath in BasePaths)
        {
            var files = DirAccess.GetFilesAt(basePath);
            
            list.AddRange(from file in files where file.Contains(".png") select file.Replace(".import", "") into pngName select $"{basePath}/{pngName}");
        }

        return list;
    }
    
    private static bool TryGetReserved(string seed, out string path)
    {
        var lower = seed.ToLowerInvariant();

        foreach (var kv in ReservedArts.Where(kv => lower.Contains(kv.Key)))
        {
            path = kv.Value;
            return true;
        }

        path = "";
        return false;
    }

    public static string Get(string seed, CardRarity rarity)
    {
        _cache ??= Load();
        
        if (rarity == CardRarity.Basic && TryGetReserved(seed, out var reserved))
            return reserved;

        if (_cache.Count == 0)
            return "res://images/packed/card_portraits/ironclad/strike_ironclad.png";

        int hash = seed.Aggregate(0, (a, c) => a * 31 + c);
        int index = Math.Abs(hash) % _cache.Count;

        return _cache[index];
    }
}