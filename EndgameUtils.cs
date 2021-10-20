using Terraria;
using System.Linq;
using Terraria.ID;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Endgame
{
    public static class EndgameUtils
    {
        public static bool InventoryHas(this Player player, params int[] items) => ((IEnumerable<Item>)player.inventory).Any(item => ((IEnumerable<int>)items).Contains(item.type));

        public static bool PortableStorageHas(this Player player, params int[] items)
        {
            bool flag = false;
            if (((IEnumerable<Item>)player.bank.item).Any((item => ((IEnumerable<int>)items).Contains(item.type))))
                flag = true;
            if (((IEnumerable<Item>)player.bank2.item).Any((item => ((IEnumerable<int>)items).Contains(item.type))))
                flag = true;
            if (((IEnumerable<Item>)player.bank3.item).Any(item => ((IEnumerable<int>)items).Contains(item.type)))
                flag = true;
            return flag;
        }

        internal static void SetUpFigure(this ModTile modTile, bool lavaImmune = true)
        {
            Main.tileFrameImportant[modTile.Type] = true;
            Main.tileNoAttach[modTile.Type] = true;
            Main.tileLavaDeath[modTile.Type] = !lavaImmune;
            Main.tileWaterDeath[modTile.Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
            TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
            TileObjectData.newTile.StyleWrapLimit = 2;
            TileObjectData.newTile.StyleMultiplier = 2;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = !lavaImmune;
            TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
            TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
            TileObjectData.addAlternate(1);
            TileObjectData.addTile(modTile.Type);

            modTile.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);
        }

        public static void DisplayLocalizedText(string key, Color? textColor = null)
        {
            if (!textColor.HasValue)
                textColor = new Color?(Color.BlueViolet);

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(Language.GetTextValue(key), textColor.Value, false);
            }
            else
            {
                if (Main.netMode == NetmodeID.MultiplayerClient)
                    return;
                NetMessage.BroadcastChatMessage(NetworkText.FromKey(key, new object[0]), textColor.Value, -1);
            }
        }
    }
}
