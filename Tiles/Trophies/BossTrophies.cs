using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;

namespace Endgame.Tiles.Trophies
{
    public class BossTrophies : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 36;
            TileObjectData.addTile(Type);

            dustType = 7;
            disableSmartCursor = true;

            ModTranslation mapEntryName = CreateMapEntryName();
            mapEntryName.SetDefault("Trophy");
            AddMapEntry(new Color(120, 85, 60), mapEntryName);
            TileID.Sets.FramesOnKillWall[Type] = true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int Type = 0;

            switch (frameX / 54)
            {
                case 0:
                    Type = ModContent.ItemType<Items.AssemblerTrophy>();
                    break;
            }

            if (Type <= 0)
                return;

            Item.NewItem(i * 16, j * 16, 48, 48, Type);
        }
    }
}
