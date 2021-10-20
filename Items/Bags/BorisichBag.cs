using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Endgame.Items.Bags
{
    public class BorisichBag : ModItem
    {
        public override int BossBagNPC => ModContent.NPCType<NPCs.Bosses.BorisichEndgameBoss>();

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.BorisichBagName"));
            Tooltip.SetDefault(Language.GetTextValue("Mods.Endgame.BorisichBagDescription"));
        }

        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 32;
            
            item.maxStack = 999;
            item.consumable = true;
            item.rare = ItemRarityID.Cyan;
            item.expert = true;
        }

        public override void OpenBossBag(Player player)
        {
            player.QuickSpawnItem(ItemID.GoldCoin, 42);
            player.QuickSpawnItem(ItemID.WaterBucket, 42);
            player.QuickSpawnItem(ModContent.ItemType<Conspectus>());
        }
    }
}
