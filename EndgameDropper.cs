﻿using Terraria;

namespace Endgame
{
    public class EndgameDropper
    {
        public static int DropItemCondition(NPC NPC, int itemID, bool dropPerPlayer, bool condition, int minQuantity = 1, int maxQuantity = 0)
        {
            return !condition ? 0 : DropItem(NPC, itemID, dropPerPlayer, minQuantity, maxQuantity);
        }

        public static int DropItem(NPC NPC, int itemID, bool dropPerPlayer, int minQuantity = 1, int maxQuantity = 0)
        {
            int num = maxQuantity > minQuantity ? Main.rand.Next(minQuantity, maxQuantity + 1) : minQuantity;

            if (num <= 0)
                return 0;

            if (dropPerPlayer)
                NPC.DropItemInstanced(NPC.position, NPC.Size, itemID, num);
            else
                Item.NewItem(NPC.Hitbox, itemID, num);

            return num;
        }

        public static int DropItem(NPC NPC, int itemID, int minQuantity = 1, int maxQuantity = 0) => DropItem(NPC, itemID, false, minQuantity, maxQuantity);

        public static int DropItemChance(NPC NPC, int itemID, bool dropPerPlayer, float chance, int minQuantity = 1, int maxQuantity = 0)
        {
            return Main.rand.NextFloat() > chance ? 0 : DropItem(NPC, itemID, dropPerPlayer, minQuantity, maxQuantity);
        }

        public static int DropItemChance(NPC NPC, int itemID, float chance, int minQuantity = 1, int maxQuantity = 0)
        {
            return DropItemChance(NPC, itemID, false, chance, minQuantity, maxQuantity);
        }

        public static int DropItemChance(NPC NPC, int itemID, bool dropPerPlayer, int oneInXChance, int minQuantity = 1, int maxQuantity = 0)
        {
            return Main.rand.Next(oneInXChance) != 0 ? 0 : DropItem(NPC, itemID, dropPerPlayer, minQuantity, maxQuantity);
        }

        public static int DropItemChance(NPC NPC, int itemID, int oneInXChance, int minQuantity = 1, int maxQuantity = 0)
        {
            return DropItemChance(NPC, itemID, false, oneInXChance, minQuantity, maxQuantity);
        }
    }
}
