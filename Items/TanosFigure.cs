using Terraria.ID;
using Terraria.ModLoader;
using Endgame.Tiles.Decorative;
using Terraria.Localization;

namespace Endgame.Items
{
    public class TanosFigure : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.GreenManName"));
            Tooltip.SetDefault(Language.GetTextValue("Mods.Endgame.GreenManDescription"));
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
