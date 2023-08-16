using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Endgame.Tiles.Decorative;

namespace Endgame.Items
{
    public class TanosFigure : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.Common.GreenManName"));
            // Tooltip.SetDefault(Language.GetTextValue("Mods.Endgame.Common.GreenManDescription"));
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 34;
            
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            
            Item.rare = ItemRarityID.LightPurple;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<TanosFigureTiles>();
        }
    }
}
