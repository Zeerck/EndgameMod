using System;
using Terraria;
using System.IO;
using Terraria.ID;
using Terraria.ModLoader;
using Endgame.NPCs.TownNPCs;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Endgame.NPCs.Bosses
{
    [AutoloadBossHead]
    public class BorisichEndgameBoss : ModNPC
    {
        private int ai;
        private int attackTimer = 0;
        private bool fastSpeed = false;
        private bool stunned;
        private int stunnedTimer;

        private int frame = 0;
        private double counting;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.BorisichBossName"));
            Main.npcFrameCount[npc.type] = 6;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.width = 120;
            npc.height = 165;

            npc.boss = true;
            npc.aiStyle = -1;
            npc.npcSlots = 5f;

            npc.lifeMax = 4000;
            npc.damage = 100;
            npc.defense = 20;
            npc.knockBackResist = 0f;

            npc.value = Item.buyPrice(gold: 1);

            npc.lavaImmune = true;
            npc.trapImmune = true;
            npc.noTileCollide = true;
            npc.noGravity = true;

            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Borisich_mozhno_ne_pisat");

            Mod getMod = ModLoader.GetMod("CalamityModMusic");

            if (getMod != null)
            {
                music = getMod.GetSoundSlot(SoundType.Music, "Sounds/Music/UniversalCollapse");
            }
            else
            {
                music = 5;
            }

            bossBag = ModContent.ItemType<Items.Bags.BorisichBag>();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Main.PlaySound(mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Borisich_raz"), Main.LocalPlayer.position);
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * bossLifeScale);
            npc.damage = (int)(npc.damage + 1.3f);
        }

        public override void AI()
        {
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Borisich_DOS");

            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            Vector2 target = npc.HasPlayerTarget ? player.Center : Main.npc[npc.target].Center;

            npc.rotation = 0.0f;
            npc.netAlways = true;
            npc.TargetClosest(true);

            if (npc.life >= npc.lifeMax)
                npc.life = npc.lifeMax;

            if (npc.target < 0 || npc.target == 255 || player.dead || !player.active)
            {
                npc.TargetClosest(false);
                npc.direction = 1;
                npc.velocity.Y = npc.velocity.Y - 0.1f;

                if (npc.timeLeft > 20)
                {
                    npc.timeLeft = 20;
                    return;
                }
            }

            if (stunned)
            {
                npc.velocity.X = 0.0f;
                npc.velocity.Y = 0.0f;

                stunnedTimer++;

                if (stunnedTimer >= 100)
                {
                    stunned = false;
                    stunnedTimer = 0;
                }
            }

            ai++;

            npc.ai[0] = ai * 1f;
            int distance = (int)Vector2.Distance(target, npc.Center);

            if (npc.ai[0] < 300)
            {
                frame = 0;
                MoveTowards(npc, target, distance > 300 ? 13f : 7f, 30f);
                npc.netUpdate = true;
            }

            else if (npc.ai[0] >= 350 && npc.ai[0] < 450.0)
            {
                stunned = true;
                frame = 1;
                npc.defense = 40;
                npc.damage = 42;
                MoveTowards(npc, target, distance > 300 ? 13f : 7f, 30f);
                npc.netUpdate = true;
            }

            else if (npc.ai[0] >= 450.0)
            {
                frame = 2;
                stunned = false;
                npc.damage = 100;
                npc.defense = 15;

                if (!fastSpeed)
                {
                    fastSpeed = true;
                }
                else
                {
                    if (npc.ai[0] % 50 == 0)
                    {
                        float speed = 12f;
                        Vector2 vector = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                        float x = player.position.X + (player.width / 2) - vector.X;
                        float y = player.position.Y + (player.height / 2) - vector.Y;
                        float distance2 = (float)Math.Sqrt(x * x + y * y);
                        float factor = speed / distance2;
                        npc.velocity.X = x * factor;
                        npc.velocity.Y = y * factor;
                    }
                }
                npc.netUpdate = true;
            }

            if (npc.ai[0] % (Main.expertMode ? 100 : 150) == 0 && !stunned && !fastSpeed)
            {
                attackTimer++;

                if (attackTimer <= 2)
                {
                    frame = 2;
                    npc.velocity.X = 0f;
                    npc.velocity.Y = 0f;

                    Vector2 shootPos = npc.Center;
                    float accuracy = 5f * (npc.life / npc.lifeMax);
                    Vector2 shootVel = target - shootPos + new Vector2(Main.rand.NextFloat(-accuracy, accuracy), Main.rand.NextFloat(-accuracy, accuracy));
                    shootVel.Normalize();
                    shootVel *= 7.5f;

                    for (int i = 0; i < (Main.expertMode ? 5 : 3); i++)
                    {
                        Projectile.NewProjectile(shootPos.X + (-100 * npc.direction) + Main.rand.Next(-40, 41), shootPos.Y - Main.rand.Next(-50, 40), shootVel.X, shootVel.Y, ModContent.ProjectileType<Projectiles.Bosses.BorisichEndgameProjectile>(), npc.damage / 3, 5f);
                    }
                }
                else
                {
                    attackTimer = 0;
                }
            }

            if (npc.ai[0] >= 650.0)
            {
                ai = 0;
                npc.alpha = 0;
                fastSpeed = false;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (frame == 0)
            {
                counting += 1.0;

                if (counting < 8.0)
                {
                    npc.frame.Y = 0;
                }
                else if (counting < 16.0)
                {
                    npc.frame.Y = frameHeight;
                }
                else if (counting < 24.0)
                {
                    npc.frame.Y = frameHeight * 2;
                }
                else if (counting < 32.0)
                {
                    npc.frame.Y = frameHeight * 3;
                }
                else
                {
                    counting = 0;
                }
            }
            else if (frame == 1)
            {
                npc.frame.Y = frameHeight * 4;
            }
            else
            {
                npc.frame.Y = frameHeight * 5;
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
        }

        private void MoveTowards(NPC NPC, Vector2 playerTarget, float speed, float turnResistance)
        {
            var move = playerTarget - npc.Center;
            float lenght = move.Length();

            if (lenght > speed)
            {
                move *= speed / lenght;
            }

            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            lenght = move.Length();

            if (lenght > speed)
            {
                move *= speed / lenght;
            }

            npc.velocity = move;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            EndgameDropper.DropItemChance(npc, ModContent.ItemType<Items.AssemblerTrophy>(), 10, 1, 0);

            if (EndgameWorld.GreenManSpawn == false)
            {
                EndgameDropper.DropItemCondition(npc, ModContent.ItemType<Items.TanosFigure>(), true, !EndgameWorld.borisichDefeated);
                EndgameWorld.GreenManSpawn = true;
            }

            EndgameDropper.DropItem(npc, ModContent.ItemType<Items.Bags.BorisichBag>());

            if (!Main.expertMode)
            {
                EndgameDropper.DropItem(npc, 183, 20, 30);
                EndgameDropper.DropItem(npc, 194, 3, 6);
            }

            if (EndgameWorld.DurthuSpawn == false)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<NpcDurthu>());
                EndgameWorld.DurthuSpawn = true;
            }

            if (EndgameWorld.SudarinSpawn == false)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<NpcSudarin>());
                EndgameWorld.SudarinSpawn = true;
            }

            if (EndgameWorld.ZeerckSpawn == false)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<NpcZeerck>());
                EndgameWorld.ZeerckSpawn = true;
            }

            EndgameWorld.borisichDefeated = true;
            EndgameNet.SyncWorld();
        }
    }
}
