using Terraria.ID;
using Terraria.ModLoader;
using Endgame.Tiles.Decorative;

namespace Endgame.Items
{
    public class TanosFigure : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Green Man");
            Tooltip.SetDefault("Ah, again, your little green mens...");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 34;
            
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.Stabbing;
            
            item.rare = ItemRarityID.LightPurple;
            item.maxStack = 99;
            item.consumable = true;
            item.createTile = ModContent.TileType<TanosFigureTiles>();
        }
    }
}
