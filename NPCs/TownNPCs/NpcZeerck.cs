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
        private string _durthuNpcName;
        private string _sudarinNpcName;
        private string _zeerckNpcName;

        private static readonly List<string> _names = new List<string>()
        {
            Language.GetTextValue("Mods.Endgame.ZeerckName"),
            Language.GetTextValue("Mods.Endgame.ZeerckyName"),
            Language.GetTextValue("Mods.Endgame.Zeer4eckName"),
            Language.GetTextValue("Mods.Endgame.DmitrixName"),
            Language.GetTextValue("Mods.Endgame.Dima800Name"),
            Language.GetTextValue("Mods.Endgame.DipaName")
        };

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Anarchist");

            Main.npcFrameCount[npc.type] = 23;

            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 60;
            NPCID.Sets.ExtraFramesCount[npc.type] = 9;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 500;
            NPCID.Sets.AttackAverageChance[npc.type] = 10;
        }

        public override void SetDefaults()
        {
            npc.width = 18;
            npc.height = 44;

            npc.townNPC = true;
            npc.friendly = true;
            npc.lavaImmune = false;

            npc.aiStyle = 7;
            animationType = 208;

            npc.damage = 10;
            npc.defense = 15;
            npc.lifeMax = 250;
            npc.knockBackResist = 0.5f;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
        }

        public override void AI()
        {
            if (EndgameWorld.ZeerckSpawn)
                return;
            EndgameWorld.ZeerckSpawn = true;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money) => EndgameUtils.TownNpcSpawn();

        public override string TownNPCName() => _names[WorldGen.genRand.Next(_names.Count)];

        public override void NPCLoot()
        {
            if (EndgameWorld.ZeerckSpawn)
                EndgameWorld.ZeerckSpawn = false;
        }

        public override string GetChat()
        {
            if(EndgameWorld.DurthuSpawn)
            _durthuNpcName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcDurthu>())].GivenName;
            if(EndgameWorld.SudarinSpawn)
            _sudarinNpcName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcSudarin>())].GivenName;
            if(EndgameWorld.ZeerckSpawn)
            _zeerckNpcName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcZeerck>())].GivenName;

            List<string> chatList = new List<string>
            {
                Language.GetTextValue("Mods.Endgame.NpcZeerckText1"),
                Language.GetTextValue("Mods.Endgame.NpcZeerckText2"),
                Language.GetTextValue("Mods.Endgame.NpcZeerckText7"),
                Language.GetTextValue("Mods.Endgame.NpcZeerckText8"),
                Language.GetTextValue("Mods.Endgame.NpcZeerckText9"),
                Language.GetTextValue("Mods.Endgame.NpcZeerckText10"),
                Language.GetTextValue("Mods.Endgame.NpcZeerckText11"),
                Language.GetTextValue("Mods.Endgame.NpcZeerckText12"),
                Language.GetTextValue("Mods.Endgame.NpcZeerckText13")
            };

            if (EndgameWorld.SudarinSpawn)
            {
                chatList.Add(Language.GetTextValue("Mods.Endgame.NpcZeerckText3", _sudarinNpcName));
                chatList.Add(Language.GetTextValue("Mods.Endgame.NpcZeerckText4", _sudarinNpcName));
            }

            if (EndgameWorld.DurthuSpawn)
            {
                chatList.Add(Language.GetTextValue("Mods.Endgame.NpcZeerckText5", _durthuNpcName));
                chatList.Add(Language.GetTextValue("Mods.Endgame.NpcZeerckText6", _durthuNpcName));
            }

            if (!Main.dayTime && !Main.bloodMoon)
                chatList.Add(Language.GetTextValue("Mods.Endgame.NpcZeerckNightText"));

            if (_zeerckNpcName == Language.GetTextValue("Mods.Endgame.ZeerckName"))
            {
                chatList.Add(Language.GetTextValue("Mods.Endgame.NpcZeerckToZeerckText1"));
                chatList.Add(Language.GetTextValue("Mods.Endgame.NpcZeerckToZeerckText2"));
            }

            if (_zeerckNpcName == Language.GetTextValue("Mods.Endgame.ZeerckyName"))
                chatList.Add(Language.GetTextValue("Mods.Endgame.NpcZeerckToZeerckyText"));

            if (_zeerckNpcName == Language.GetTextValue("Mods.Endgame.Zeer4eckName"))
                chatList.Add(Language.GetTextValue("Mods.Endgame.NpcZeerckToZeer4eckText"));

            if (_zeerckNpcName == Language.GetTextValue("Mods.Endgame.DmitrixName"))
                chatList.Add(Language.GetTextValue("Mods.Endgame.NpcZeerckToDmitrixText"));

            if (_zeerckNpcName == Language.GetTextValue("Mods.Endgame.Dima800Name"))
            {
                chatList.Add(Language.GetTextValue("Mods.Endgame.NpcZeerckToDima800Text1"));
                chatList.Add(Language.GetTextValue("Mods.Endgame.NpcZeerckToDima800Text2"));
            }

            if (Main.LocalPlayer.HasItem(ModContent.ItemType<Items.Conspectus>()) && EndgameWorld.SudarinSpawn)
                chatList.Add(Language.GetTextValue("Mods.Endgame.NpcZeerckTextConspectus", _sudarinNpcName));

            return chatList[Main.rand.Next(chatList.Count)];
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("Mods.Endgame.NpcZeerckTextButton");
            button2 = Language.GetTextValue("Mods.Endgame.NpcZeerckTextButton2");
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
                EndgameDropper.DropItem(npc, ModContent.ItemType<Items.IconOfHolyAllWriting>());
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
