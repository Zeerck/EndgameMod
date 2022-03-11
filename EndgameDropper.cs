using Terraria;
using Terraria.DataStructures;

namespace Endgame
{
    public class EndgameDropper
    {
        public static int DropItemCondition(IEntitySource entitySource, NPC NPC, int itemID, bool dropPerPlayer, bool condition, int minQuantity = 1, int maxQuantity = 0) => !condition ? 0 : DropItem(entitySource, NPC, itemID, dropPerPlayer, minQuantity, maxQuantity);

        public static int DropItem(IEntitySource entitySource, NPC NPC, int itemID, bool dropPerPlayer, int minQuantity = 1, int maxQuantity = 0)
        {
            int num = maxQuantity > minQuantity ? Main.rand.Next(minQuantity, maxQuantity + 1) : minQuantity;

            if (num <= 0)
                return 0;

            if (dropPerPlayer)
                NPC.DropItemInstanced(NPC.position, NPC.Size, itemID, num);
            else
                Item.NewItem(entitySource, NPC.Hitbox, itemID, num);

            return num;
        }

        public static int DropItem(IEntitySource entitySource, NPC NPC, int itemID, int minQuantity = 1, int maxQuantity = 0) => DropItem(entitySource, NPC, itemID, false, minQuantity, maxQuantity);

        public static int DropItemChance(IEntitySource entitySource, NPC NPC, int itemID, bool dropPerPlayer, float chance, int minQuantity = 1, int maxQuantity = 0) => Main.rand.NextFloat() > chance ? 0 : DropItem(entitySource, NPC, itemID, dropPerPlayer, minQuantity, maxQuantity);

        public static int DropItemChance(IEntitySource entitySource, NPC NPC, int itemID, float chance, int minQuantity = 1, int maxQuantity = 0) => DropItemChance(entitySource, NPC, itemID, false, chance, minQuantity, maxQuantity);

        public static int DropItemChance(IEntitySource entitySource, NPC NPC, int itemID, bool dropPerPlayer, int oneInXChance, int minQuantity = 1, int maxQuantity = 0) => Main.rand.Next(oneInXChance) != 0 ? 0 : DropItem(entitySource, NPC, itemID, dropPerPlayer, minQuantity, maxQuantity);

        public static int DropItemChance(IEntitySource entitySource, NPC NPC, int itemID, int oneInXChance, int minQuantity = 1, int maxQuantity = 0) => DropItemChance(entitySource, NPC, itemID, false, oneInXChance, minQuantity, maxQuantity);
    }
}
