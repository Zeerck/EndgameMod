using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

using System.Collections.Generic;

namespace Endgame.NPCs.TownNPCs
{
    [AutoloadHead]
    public class NpcZeerck : ModNPC
    {
        private string _durthuNPCName;
        private string _sudarinNPCName;
        private string _zeerckNPCName;

        private static readonly List<string> _names = new()
        {
            Language.GetTextValue("Mods.Endgame.Common.ZeerckName"),
            Language.GetTextValue("Mods.Endgame.Common.ZeerckyName"),
            Language.GetTextValue("Mods.Endgame.Common.Zeer4eckName"),
            Language.GetTextValue("Mods.Endgame.Common.DmitrixName"),
            Language.GetTextValue("Mods.Endgame.Common.Dima800Name"),
            Language.GetTextValue("Mods.Endgame.Common.DipaName")
        };

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 23;

            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 60;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 500;
            NPCID.Sets.AttackAverageChance[NPC.type] = 10;
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 44;

            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.lavaImmune = false;

            NPC.aiStyle = 7;
            AnimationType = 208;

            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.knockBackResist = 0.5f;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
        }

        public override void AI()
        {
            if (EndgameWorld.ZeerckExist)
                return;
            EndgameWorld.ZeerckExist = true;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)/* tModPorter Suggestion: Copy the implementation of NPC.SpawnAllowed_Merchant in vanilla if you to count money, and be sure to set a flag when unlocked, so you don't count every tick. */
        {
            for (int index = 0; index < byte.MaxValue; ++index)
            {
                Player player = Main.player[index];
                int num;
                if (!player.InventoryHas(74))
                    num = player.PortableStorageHas(74) ? 1 : 0;
                else
                    num = 1;
                bool flag = num != 0;
                if (player.active & flag)
                    return EndgameWorld.ZeerckExist;
            }
            return EndgameWorld.ZeerckExist;
        }

        public override List<string> SetNPCNameList() => _names;

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            if (EndgameWorld.ZeerckExist)
                EndgameWorld.ZeerckExist = false;
        }

        public override bool CheckDead()
        {
            if (EndgameWorld.ZeerckExist)
                EndgameWorld.ZeerckExist = false;

            return true;
        }

        public override string GetChat()
        {
            if(EndgameWorld.DurthuExist)
            _durthuNPCName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcDurthu>())].GivenName;
            if(EndgameWorld.SudarinExist)
            _sudarinNPCName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcSudarin>())].GivenName;
            if(EndgameWorld.ZeerckExist)
            _zeerckNPCName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcZeerck>())].GivenName;

            List<string> chatList = new()
            {
                Language.GetTextValue("Mods.Endgame.Common.NPCZeerckText1"),
                Language.GetTextValue("Mods.Endgame.Common.NPCZeerckText2"),
                Language.GetTextValue("Mods.Endgame.Common.NPCZeerckText7"),
                Language.GetTextValue("Mods.Endgame.Common.NPCZeerckText8"),
                Language.GetTextValue("Mods.Endgame.Common.NPCZeerckText9"),
                Language.GetTextValue("Mods.Endgame.Common.NPCZeerckText10"),
                Language.GetTextValue("Mods.Endgame.Common.NPCZeerckText11"),
                Language.GetTextValue("Mods.Endgame.Common.NPCZeerckText12"),
                Language.GetTextValue("Mods.Endgame.Common.NPCZeerckText13")
            };

            if (EndgameWorld.SudarinExist)
            {
                chatList.Add(Language.GetTextValue("Mods.Endgame.Common.NPCZeerckText3", _sudarinNPCName));
                chatList.Add(Language.GetTextValue("Mods.Endgame.Common.NPCZeerckText4", _sudarinNPCName));
            }

            if (EndgameWorld.DurthuExist)
            {
                chatList.Add(Language.GetTextValue("Mods.Endgame.Common.NPCZeerckText5", _durthuNPCName));
                chatList.Add(Language.GetTextValue("Mods.Endgame.Common.NPCZeerckText6", _durthuNPCName));
            }

            if (!Main.dayTime && !Main.bloodMoon)
                chatList.Add(Language.GetTextValue("Mods.Endgame.Common.NPCZeerckNightText"));

            if (_zeerckNPCName == Language.GetTextValue("Mods.Endgame.Common.ZeerckName"))
            {
                chatList.Add(Language.GetTextValue("Mods.Endgame.Common.NPCZeerckToZeerckText1"));
                chatList.Add(Language.GetTextValue("Mods.Endgame.Common.NPCZeerckToZeerckText2"));
            }

            if (_zeerckNPCName == Language.GetTextValue("Mods.Endgame.Common.ZeerckyName"))
                chatList.Add(Language.GetTextValue("Mods.Endgame.Common.NPCZeerckToZeerckyText"));

            if (_zeerckNPCName == Language.GetTextValue("Mods.Endgame.Common.Zeer4eckName"))
                chatList.Add(Language.GetTextValue("Mods.Endgame.Common.NPCZeerckToZeer4eckText"));

            if (_zeerckNPCName == Language.GetTextValue("Mods.Endgame.Common.DmitrixName"))
                chatList.Add(Language.GetTextValue("Mods.Endgame.Common.NPCZeerckToDmitrixText"));

            if (_zeerckNPCName == Language.GetTextValue("Mods.Endgame.Common.Dima800Name"))
            {
                chatList.Add(Language.GetTextValue("Mods.Endgame.Common.NPCZeerckToDima800Text1"));
                chatList.Add(Language.GetTextValue("Mods.Endgame.Common.NPCZeerckToDima800Text2"));
            }

            if (Main.LocalPlayer.HasItem(ModContent.ItemType<Items.Conspectus>()) && EndgameWorld.SudarinExist)
                chatList.Add(Language.GetTextValue("Mods.Endgame.Common.NPCZeerckTextConspectus", _sudarinNPCName));

            return chatList[Main.rand.Next(chatList.Count)];
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("Mods.Endgame.Common.NPCZeerckTextButton");
            button2 = Language.GetTextValue("Mods.Endgame.Common.NPCZeerckTextButton2");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            var source = NPC.GetSource_Loot();

            if (firstButton)
                EndgameDropper.DropItem(source, NPC, ModContent.ItemType<Items.IconOfHolyAllWriting>());
            else
                Main.LocalPlayer.AddBuff(ModContent.BuffType<Buffs.StopTalkingDebuff>(), 350);
        }

        public override bool CanGoToStatue(bool toKingStatue) => !toKingStatue;

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 50;
            knockback = 2f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 180;
            randExtraCooldown = 60;
        }
        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<Projectiles.NPCs.NPCsShoot>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 2f;
        }
    }
}
