using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace KovanMod.KovanModCode.Utility;

public static class CombatTargeting
{
    public static Creature? GetRightMostEnemy(IEnumerable<Creature> enemies)
    {
        Creature? rightMost = null;
        float maxX = float.MinValue;

        foreach (var e in enemies)
        {
            var node = NCombatRoom.Instance?.GetCreatureNode(e);
            if (node == null)
                continue;

            float x = node.VfxSpawnPosition.X;

            if (x > maxX)
            {
                maxX = x;
                rightMost = e;
            }
        }

        return rightMost;
    }
}