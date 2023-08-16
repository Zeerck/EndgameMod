using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace Endgame.Items
{
    public class AssemblerTrophy : ModItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.Common.AssemblerVirtualMachineName"));

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            
            Item.maxStack = 99;
            Item.consumable = true;
            Item.value = 50000;
            Item.rare = ItemRarityID.Blue;
            Item.createTile = ModContent.TileType<Tiles.Trophies.BossTrophies>();
            Item.placeStyle = 0;
        }
    }
}
