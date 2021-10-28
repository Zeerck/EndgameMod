using Terraria;
using System.Linq;
using Terraria.ID;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using System.Threading.Tasks;
using System.Threading;
using Terraria.DataStructures;
using System.IO;

namespace Endgame
{
    public static class EndgameUtils
    {
        public static bool InventoryHas(this Player player, params int[] items) => ((IEnumerable<Item>)player.inventory).Any(item => ((IEnumerable<int>)items).Contains(item.type));

        public static bool PortableStorageHas(this Player player, params int[] items)
        {
            bool flag = false;
            if (((IEnumerable<Item>)player.bank.item).Any(item => ((IEnumerable<int>)items).Contains(item.type)))
                flag = true;
            if (((IEnumerable<Item>)player.bank2.item).Any(item => ((IEnumerable<int>)items).Contains(item.type)))
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

        //huh, fuck this shit
        public async static void DisplayDelayLocalizedText(string key, int delay, Color? textColor = null)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(delay);

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
            });
        }

        public static async void PlayCustomLocalDelaySound(Mod mod, Vector2 position, string path, int delay)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(delay);

                Main.PlaySound((int)SoundType.Custom, position, mod.GetSoundSlot(SoundType.Custom, path));
            });
        }

        public static void AhChooKill()
        {
            int npcSudarin = NPC.FindFirstNPC(ModContent.NPCType<NPCs.TownNPCs.NpcSudarin>());

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active)
                {
                    if (EndgameWorld.DurthuSpawn && Main.npc[i] == Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NPCs.TownNPCs.NpcDurthu>())])
                        continue;
                    if (Main.npc[i] == Main.npc[npcSudarin])
                        continue;
                    if (EndgameWorld.ZeerckSpawn && Main.npc[i] == Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NPCs.TownNPCs.NpcZeerck>())])
                        continue;

                    Main.npc[i].StrikeNPCNoInteraction(Main.npc[i].lifeMax, 0f, -Main.npc[i].direction, true);
                }
            }
            Main.LocalPlayer.KillMe(PlayerDeathReason.ByCustomReason(Language.GetTextValue("Mods.Endgame.DeathReason1") + Main.LocalPlayer.name + Language.GetTextValue("Mods.Endgame.DeathReason11") + Main.npc[npcSudarin].GivenName + "'a..."), Main.LocalPlayer.statLife, -Main.LocalPlayer.direction);
        }
    }
}
