using System;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.Localization;
using Endgame.NPCs.TownNPCs;
using Microsoft.Xna.Framework;

namespace Endgame.NPCs.Bosses
{
    //TODO: Make Borisich AI smarter
    [AutoloadBossHead]
    public class BorisichEndgameBoss : ModNPC
    {
        private int _ai;
        private int _frame = 0;
        private int _stunnedTimer;
        private int _attackTimer = 0;

        private bool _noHighDefence = false;
        private bool _playerDead = false;
        private bool _fastSpeed = false;
        private bool _stunned;
        private bool _standingHere = false;

        private double _counting;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.Common.BorisichBossName"));

            Main.npcFrameCount[NPC.type] = 6;
            NPCID.Sets.TrailingMode[NPC.type] = 1;
        }

        public override void SetDefaults()
        {
            NPC.width = 120;
            NPC.height = 165;

            NPC.boss = true;
            NPC.aiStyle = -1;
            NPC.npcSlots = 5f;

            NPC.lifeMax = 4000;
            NPC.damage = 100;
            NPC.defense = 20;
            NPC.knockBackResist = 0f;

            NPC.value = Item.buyPrice(gold: 1);

            NPC.noGravity = true;
            NPC.lavaImmune = true;
            NPC.trapImmune = true;
            NPC.noTileCollide = true;

            NPC.DeathSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Custom/Borisich_mozhno_ne_pisat");

            Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/ItWasToBeThisWay");
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            if (numPlayers > 1)
                NPC.lifeMax = (int)(NPC.lifeMax * ((numPlayers * NPC.lifeMax) / 1.5) * bossLifeScale);
            else
                NPC.lifeMax = (int)(NPC.lifeMax * bossLifeScale);

            NPC.damage = (int)(NPC.damage + 1.3f);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            SoundEngine.PlaySound(SoundLoader.GetSoundSlot(Mod, "Sounds/Custom/Borisich_raz"), NPC.position);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Custom/Borisich_raz"), NPC.position);
            _playerDead = false;
        }


        public override void AI()
        {
            Player player = Main.player[NPC.target];
            Vector2 target = NPC.HasPlayerTarget ? player.Center : Main.npc[NPC.target].Center;

            NPC.rotation = 0.0f;
            NPC.netAlways = true;
            NPC.TargetClosest(true);
            NPC.HitSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Custom/Borisich_DOS");

            if (player.statLife <= 70)
            {
                NPC.ai[0] = 0;
                _standingHere = true;
            }

            if(NPC.ai[0] == 0 && _standingHere)
            {
                int distance2 = (int)Vector2.Distance(target, NPC.Center);

                //if(!EndgamePlayer.PlayerStanding)
                    player.AddBuff(ModContent.BuffType<Buffs.StandingHereBuff>(), 3000, false, true);

                NPC.damage = 0;
                NPC.defense = 700;
                MoveTowards(target, distance2 > 300 ? 13f : 7f, 30f);

                if (NPC.life == NPC.life * 90 / 100)
                {
                    player.ClearBuff(ModContent.BuffType<Buffs.StandingHereBuff>());
                    player.statLife += 400;
                    _standingHere = false;
                    NPC.ai[0]++;
                }
            }

            if (NPC.life >= NPC.lifeMax)
                NPC.life = NPC.lifeMax;

            if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active && !_standingHere)
            {
                if (!_playerDead)
                {
                    EndgameUtils.PlayCustomLocalDelaySound(Mod, Main.LocalPlayer.position, "Sounds/Custom/Borisich_vot_i_vsya_prog", 7000);
                    EndgameUtils.DisplayDelayLocalizedText("Mods.Endgame.Common.BorisichBossText2", 7000);
                    _playerDead = true;
                }

                NPC.TargetClosest(false);
                NPC.direction = 1;
                NPC.velocity.Y = NPC.velocity.Y - 0.1f;

                if (NPC.timeLeft > 20)
                {
                    NPC.timeLeft = 20;
                    return;
                }
            }

            if (_stunned && !_standingHere)
            {
                NPC.velocity.X = 0.0f;
                NPC.velocity.Y = 0.0f;

                _stunnedTimer++;

                if (_stunnedTimer >= 100)
                {
                    _stunned = false;
                    _stunnedTimer = 0;
                }
            }

            _ai++;
            NPC.ai[0] = _ai * 1f;

            int distance = (int)Vector2.Distance(target, NPC.Center);

            if (NPC.ai[0] < 300 && NPC.ai[0] > 0 && !_standingHere)
            {
                _frame = 0;
                MoveTowards(target, distance > 300 ? 13f : 7f, 30f);
                NPC.netUpdate = true;
            }

            else if (NPC.ai[0] >= 350 && NPC.ai[0] < 1450.0 && !_standingHere)
            {
                if(!_noHighDefence)
                {
                    EndgameUtils.PlayCustomLocalDelaySound(Mod, NPC.position, "Sounds/Custom/Borisich_ne_schitaetsa", 500);
                    _noHighDefence = true;
                }

                _stunned = true;
                _frame = 1;
                NPC.defense = 9900;
                NPC.damage = 42;
                MoveTowards(target, distance > 300 ? 13f : 7f, 30f);
                NPC.netUpdate = true;
            }

            else if (NPC.ai[0] >= 1450.0 && !_standingHere)
            {
                _noHighDefence = false;
                _frame = 2;
                _stunned = false;
                NPC.damage = 100;
                NPC.defense = 15;

                if (!_fastSpeed)
                    _fastSpeed = true;
                else
                {
                    if (NPC.ai[0] % 50 == 1)
                    {
                        float speed = 12f;
                        Vector2 vector = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                        float x = player.position.X + (player.width / 2) - vector.X;
                        float y = player.position.Y + (player.height / 2) - vector.Y;
                        float distance2 = (float)Math.Sqrt(x * x + y * y);
                        float factor = speed / distance2;
                        NPC.velocity.X = x * factor;
                        NPC.velocity.Y = y * factor;
                    }
                }
                NPC.netUpdate = true;
            }

            if (NPC.ai[0] % (Main.expertMode ? 100 : 150) == 1 && !_stunned && !_fastSpeed && !_standingHere)
            {
                _attackTimer++;

                if (_attackTimer <= 2)
                {
                    _frame = 2;
                    NPC.velocity.X = 0f;
                    NPC.velocity.Y = 0f;

                    Vector2 shootPos = NPC.Center;
                    float accuracy = 5f * (NPC.life / NPC.lifeMax);
                    Vector2 shootVel = target - shootPos + new Vector2(Main.rand.NextFloat(-accuracy, accuracy), Main.rand.NextFloat(-accuracy, accuracy));
                    shootVel.Normalize();
                    shootVel *= 7.5f;

                    for (int i = 0; i < (Main.expertMode ? 5 : 3); i++)
                    {
                        Projectile.NewProjectile(NPC.GetSpawnSourceForProjectileNPC(), shootPos.X + (-100 * NPC.direction) + Main.rand.Next(-40, 41), shootPos.Y - Main.rand.Next(-50, 40), shootVel.X, shootVel.Y, ModContent.ProjectileType<Projectiles.Bosses.BorisichEndgameProjectile>(), NPC.damage / 3, 5f);
                    }
                }
                else
                {
                    _attackTimer = 0;
                }
            }

            if (NPC.ai[0] >= 1650.0 && !_standingHere)
            {
                _ai = 0;
                NPC.alpha = 0;
                _fastSpeed = false;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (_frame == 0)
            {
                _counting += 1.0;

                if (_counting < 8.0)
                    NPC.frame.Y = 0;
                else if (_counting < 16.0)
                    NPC.frame.Y = frameHeight;
                else if (_counting < 24.0)
                    NPC.frame.Y = frameHeight * 2;
                else if (_counting < 32.0)
                    NPC.frame.Y = frameHeight * 3;
                else
                    _counting = 0;
            }
            else if (_frame == 1)
                NPC.frame.Y = frameHeight * 4;
            else
                NPC.frame.Y = frameHeight * 5;
        }

        private void MoveTowards(Vector2 playerTarget, float speed, float turnResistance)
        {
            var move = playerTarget - NPC.Center;
            float lenght = move.Length();

            if (lenght > speed)
                move *= speed / lenght;

            move = (NPC.velocity * turnResistance + move) / (turnResistance + 1f);
            lenght = move.Length();

            if (lenght > speed)
                move *= speed / lenght;

            NPC.velocity = move;
        }

        private void MoveToPlayer(Vector2 playerTarget) //bad thing
        {
            var move = playerTarget - NPC.Center;

            NPC.velocity = move;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            var source = NPC.GetSpawnSourceForProjectileNPC();

            //SoundEngine.PlaySound(type: 50, Main.LocalPlayer.position, SoundLoader.GetSoundSlot(Mod, "Sounds/Custom/Borisich_mozhno_ne_pisat"));

            EndgameDropper.DropItemChance(source, NPC, ModContent.ItemType<Items.AssemblerTrophy>(), 10, 1, 0);

            if (EndgameWorld.borisichDefeated == false)
                EndgameDropper.DropItem(source, NPC, ModContent.ItemType<Items.TanosFigure>());

            if (!Main.expertMode)
            {
                EndgameDropper.DropItem(source, NPC, ItemID.GlowingMushroom, 20, 30);
                EndgameDropper.DropItem(source, NPC, ItemID.MushroomGrassSeeds, 3, 6);
            }
            else
                EndgameDropper.DropItem(source, NPC, ModContent.ItemType<Items.Bags.BorisichBag>());

            if (EndgameWorld.DurthuExist == false)
            {
                NPC.NewNPC(source, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<NpcDurthu>());
                EndgameWorld.DurthuExist = true;
            }

            if (EndgameWorld.SudarinExist == false)
            {
                NPC.NewNPC(source, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<NpcSudarin>());
                EndgameWorld.SudarinExist = true;
            }

            if (EndgameWorld.ZeerckExist == false)
            {
                NPC.NewNPC(source, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<NpcZeerck>());
                EndgameWorld.ZeerckExist = true;
            }

            EndgameUtils.DisplayLocalizedText("Mods.Endgame.Common.OldPowersOfAnarchizmText", Color.Lime);

            EndgameWorld.borisichDefeated = true;
            EndgameNet.SyncWorld();
        }
    }
}
