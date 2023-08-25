using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Enums;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Endgame.DamageClasses;

namespace Endgame
{
    public static class EndgameUtils
    {
        public static bool InventoryHas(this Player player, params int[] items) => player.inventory.Any(item => items.Contains(item.type));

        public static bool PortableStorageHas(this Player player, params int[] items)
        {
            bool flag = false;

            if (player.bank.item.Any(item => items.Contains(item.type)))
                flag = true;
            if (player.bank2.item.Any(item => items.Contains(item.type)))
                flag = true;
            if (player.bank3.item.Any(item => items.Contains(item.type)))
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

        public static bool TownNpcSpawn()
        {
            for (int index = 0; index < byte.MaxValue; ++index)
            {
                int num;
                Player player = Main.player[index];

                if (!player.InventoryHas(74))
                    num = player.PortableStorageHas(74) ? 1 : 0;
                else
                    num = 1;

                bool flag = num != 0;

                if (player.active & flag)
                    return EndgameWorld.DurthuExist;
            }
            return EndgameWorld.DurthuExist;
        }

        public static void DisplayLocalizedText(string key, Color? textColor = null)
        {
            if (!textColor.HasValue)
                textColor = new Color?(Color.BlueViolet);

            if (Main.netMode == NetmodeID.SinglePlayer)
                Main.NewText(Language.GetTextValue(key), textColor.Value);
            else
            {
                if (Main.netMode == NetmodeID.MultiplayerClient)
                    return;
                NetMessage.SendData(MessageID.ChatText);
            }
        }

        public async static void DisplayDelayLocalizedText(string key, int delay, Color? textColor = null)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(delay);

                if (!textColor.HasValue)
                    textColor = new Color?(Color.BlueViolet);

                if (Main.netMode == NetmodeID.SinglePlayer)
                    Main.NewText(Language.GetTextValue(key), textColor.Value);
                else
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        return;
                    NetMessage.SendData(MessageID.ChatText);
                    //NetMessage.SendData(NetworkText.FromKey(key, new object[0]), textColor.Value, -1);
                }
            });
        }

        public static async void PlayCustomLocalDelaySound(Vector2 position, string path, int delay)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(delay);

                SoundEngine.PlaySound(new SoundStyle(path), position);
            });
        }

        public static void AhChooKill(string deathReasonText, NPC npc)
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active)
                {
                    if (EndgameWorld.DurthuExist && Main.npc[i] == Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NPCs.TownNPCs.NpcDurthu>())])
                        continue;
                    if (Main.npc[i] == npc)
                        continue;
                    if (EndgameWorld.ZeerckExist && Main.npc[i] == Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NPCs.TownNPCs.NpcZeerck>())])
                        continue;

                    Main.npc[i].SimpleStrikeNPC(Main.npc[i].lifeMax, -Main.npc[i].direction, true, 0, null, false, 0, true);
                }
            }
            Main.LocalPlayer.KillMe(PlayerDeathReason.ByCustomReason(deathReasonText), Main.LocalPlayer.statLife, -Main.LocalPlayer.direction);
        }

        public static void DrawTargettableEffect(NPC enemy, int type)
        {
            Vector2 dustVector = new(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
            dustVector.Normalize();
            dustVector.X *= 0.66f;
            dustVector.Y = Math.Abs(dustVector.Y);
            Vector2 effectDustVector = dustVector * Main.rand.Next(3, 5) * 0.25f;
            Dust effectDust = Dust.NewDustDirect(enemy.position, enemy.width, enemy.height, type, effectDustVector.X, effectDustVector.Y * 0.5f, 100, default, 1.5f);
            effectDust.velocity *= 0.1f;
            effectDust.velocity.Y -= 0.5f;
        }

        public static void DrawDustRadius(Player player, float radius, int type, int amount = 16)
        {
            for (int i = 0; i < amount; i++)
            {
                Vector2 offset = new();
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                offset.X += (float)(Math.Sin(angle) * radius);
                offset.Y += (float)(Math.Cos(angle) * radius);

                Dust dust = Dust.NewDustPerfect(player.Center + offset, type, player.velocity, 0, default, 1f);
                dust.fadeIn = 0.1f;
                dust.noGravity = true;
            }
        }

        public static List<NPC> GetTargettableNPCs(Vector2 center, Vector2 targetCenter, float radius, int targetsLimit)
        {
            Dictionary<NPC, float> targets = new();

            foreach (NPC npc in Main.npc)
            {
                if (!npc.active || npc.life == 0) continue;

                if (npc.CanBeChasedBy() || npc.type == NPCID.TargetDummy)
                {
                    float distance = (center - npc.Center).Length();

                    if (distance <= radius)
                    {
                        float distanceToFocus = (targetCenter - npc.Center).Length();
                        // TODO: Может бить сквозь стены или нет. Сделать через настройку конфига "CanHitThroughWalls" или создать аксессуар
                        //if (Collision.CanHit(center - new Vector2(12, 12), 24, 24, npc.position, npc.width, npc.height))
                        //{
                        targets.Add(npc, distanceToFocus);
                        //}
                    }
                }
            }

            List<NPC> targetList = new();

            var targetsCount = targets.Reverse().OrderBy(pair => pair.Value).Take(targets.Count);

            foreach (KeyValuePair<NPC, float> keyValuePair in targetsCount)
            {
                if (targetsLimit > 0)
                {
                    targetList.Add(keyValuePair.Key);
                }
                targetsLimit--;
            }

            return targetList;
        }

        public static void DoNutsDamage(Player player, Item item, int maxTargets, int hitFrequency, int dustType, float focusRadius, DamageClass damageClass = null)
        {
            Vector2 mouse = new(Main.screenPosition.X + Main.mouseX, Main.screenPosition.Y + Main.mouseY);
            List<NPC> enemiesList = GetTargettableNPCs(player.Center, mouse, focusRadius, maxTargets);

            DrawDustRadius(player, focusRadius, dustType);

            if (enemiesList.Count > 0)
            {
                foreach (NPC enemy in enemiesList)
                {
                    if ((int)Main.time % hitFrequency == 1)
                    {
                        NPC.HitInfo hitInfo = enemy.CalculateHitInfo(
                            damage: item.damage,
                            hitDirection: -(int)(enemy.DirectionTo(player.position).X * 1.5f),
                            crit: Main.rand.NextBool(item.crit, 100),
                            knockBack: item.knockBack,
                            damageType: ModContent.GetInstance<NutsDamageClass>()
                            );

                        enemy.StrikeNPC(hitInfo);
                    }

                    if (Main.rand.NextBool(5))
                        DrawTargettableEffect(enemy, dustType);
                }
            }
        }
    }
}
