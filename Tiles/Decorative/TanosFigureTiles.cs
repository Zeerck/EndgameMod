using Terraria;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

namespace Endgame.Tiles.Decorative
{
    public class TanosFigureTiles : ModTile
    {
        public override void SetDefaults()
        {
            ModTranslation mapEntryName = CreateMapEntryName();
            mapEntryName.SetDefault("Tanos Figure");

            this.SetUpFigure();

            AddMapEntry(new Color(191, 142, 111), mapEntryName);

            disableSmartCursor = true;
            adjTiles = new int[1] { 15 };
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(i * 16, j * 16, 16, 32, ModContent.ItemType<Items.TanosFigure>());
    }
}
