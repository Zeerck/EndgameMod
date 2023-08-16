using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace Endgame.Tiles.Decorative
{
    public class TanosFigureTiles : ModTile
    {
        public override void SetStaticDefaults()
        {
            LocalizedText mapEntryName = CreateMapEntryName();

            this.SetUpFigure();

            AddMapEntry(new Color(191, 142, 111), mapEntryName);

            TileID.Sets.DisableSmartCursor[Type] = true;
            AdjTiles = new int[1] { 15 };
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(new EntitySource_TileBreak(i,j), i * 16, j * 16, 16, 32, ModContent.ItemType<Items.TanosFigure>());
    }
}
