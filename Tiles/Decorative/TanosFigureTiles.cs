using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace Endgame.Tiles.Decorative
{
    public class TanosFigureTiles : ModTile
    {
        public override void SetStaticDefaults()
        {
            ModTranslation mapEntryName = CreateMapEntryName();
            mapEntryName.SetDefault("Tanos Figure");

            this.SetUpFigure();

            AddMapEntry(new Color(191, 142, 111), mapEntryName);

            TileID.Sets.DisableSmartCursor[Type] = true;
            AdjTiles = new int[1] { 15 };
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(new EntitySource_TileBreak(i,j), i * 16, j * 16, 16, 32, ModContent.ItemType<Items.TanosFigure>());
    }
}
