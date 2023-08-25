using Terraria;
using Terraria.DataStructures;

namespace Endgame
{
    public class EndgameDropper
    {
        public static int DropItemCondition(IEntitySource entitySource, NPC npc, int itemID, bool dropPerPlayer, bool condition, int minQuantity = 1, int maxQuantity = 0) => !condition ? 0 : DropItem(entitySource, npc, itemID, dropPerPlayer, minQuantity, maxQuantity);

        public static int DropItem(IEntitySource entitySource, NPC npc, int itemID, bool dropPerPlayer, int minQuantity = 1, int maxQuantity = 0)
        {
            int itemDropCount = maxQuantity > minQuantity ? Main.rand.Next(minQuantity, maxQuantity + 1) : minQuantity;
            if (itemDropCount <= 0)
                return 0;

            if (dropPerPlayer)
                npc.DropItemInstanced(npc.position, npc.Size, itemID, itemDropCount);
            else
                Item.NewItem(entitySource, npc.Hitbox, itemID, itemDropCount);

            return itemDropCount;
        }

        public static int DropItem(IEntitySource entitySource, NPC npc, int itemID, int minQuantity = 1, int maxQuantity = 0) => DropItem(entitySource, npc, itemID, false, minQuantity, maxQuantity);

        public static int DropItemChance(IEntitySource entitySource, NPC npc, int itemID, bool dropPerPlayer, float chance, int minQuantity = 1, int maxQuantity = 0) => Main.rand.NextFloat() > chance ? 0 : DropItem(entitySource, npc, itemID, dropPerPlayer, minQuantity, maxQuantity);

        public static int DropItemChance(IEntitySource entitySource, NPC npc, int itemID, float chance, int minQuantity = 1, int maxQuantity = 0) => DropItemChance(entitySource, npc, itemID, false, chance, minQuantity, maxQuantity);

        public static int DropItemChance(IEntitySource entitySource, NPC npc, int itemID, bool dropPerPlayer, int oneInXChance, int minQuantity = 1, int maxQuantity = 0) => Main.rand.Next(oneInXChance) != 0 ? 0 : DropItem(entitySource, npc, itemID, dropPerPlayer, minQuantity, maxQuantity);

        public static int DropItemChance(IEntitySource entitySource, NPC npc, int itemID, int oneInXChance, int minQuantity = 1, int maxQuantity = 0) => DropItemChance(entitySource, npc, itemID, false, oneInXChance, minQuantity, maxQuantity);
    }
}
