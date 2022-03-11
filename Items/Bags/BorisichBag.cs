using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace Endgame.Items.Bags
{
    public class BorisichBag : ModItem
    {
        public override int BossBagNPC => ModContent.NPCType<NPCs.Bosses.BorisichEndgameBoss>();

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.Common.BorisichBagName"));
            Tooltip.SetDefault(Language.GetTextValue("Mods.Endgame.Common.BorisichBagDescription"));
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 32;
            
            Item.maxStack = 999;
            Item.consumable = true;
            Item.rare = ItemRarityID.Cyan;
            Item.expert = true;
        }

        public override void OpenBossBag(Player player)
        {
            var source = player.GetItemSource_OpenItem(Type);

            player.QuickSpawnItem(source, ItemID.GoldCoin, 42);
            player.QuickSpawnItem(source, ItemID.WaterBucket, 42);
            player.QuickSpawnItem(source, ModContent.ItemType<Conspectus>());
        }
    }
}
