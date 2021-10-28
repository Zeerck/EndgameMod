using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace Endgame.NPCs.TownNPCs
{
    [AutoloadHead]
    public class NpcDurthu : ModNPC
    {
        private string _durthuNpcName;
        private string _sudarinNpcName;
        private string _zeerckNpcName;

        private static readonly List<string> _names = new List<string>()
        {
            Language.GetTextValue("Mods.Endgame.DurthuName"),
            Language.GetTextValue("Mods.Endgame.ComradeGruzoffName"),
            Language.GetTextValue("Mods.Endgame.ComradeGruzonName")
        };

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Father of all Milfs");
            Main.npcFrameCount[npc.type] = 23;
            NPCID.Sets.ExtraFramesCount[npc.type] = 9;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 500;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 60;
            NPCID.Sets.AttackAverageChance[npc.type] = 10;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.lavaImmune = false;
            npc.width = 18;
            npc.height = 44;
            npc.aiStyle = 7;
            npc.damage = 10;
            npc.defense = 15;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            animationType = 208;
        }

        public override void AI()
        {
            if (EndgameWorld.DurthuSpawn)
                return;
            EndgameWorld.DurthuSpawn = true;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
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
                    return EndgameWorld.DurthuSpawn;
            }
            return EndgameWorld.DurthuSpawn;
        }

        public override string TownNPCName()
        {
            return _names[WorldGen.genRand.Next(_names.Count)];
        }

        public override void NPCLoot()
        {
            EndgameWorld.DurthuSpawn = false;
        }

        public override string GetChat()
        {
            if (EndgameWorld.DurthuSpawn)
                _durthuNpcName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcDurthu>())].GivenName;
            if (EndgameWorld.SudarinSpawn)
                _sudarinNpcName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcSudarin>())].GivenName;
            if (EndgameWorld.ZeerckSpawn)
                _zeerckNpcName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcZeerck>())].GivenName;

            List<string> chatList = new List<string>();

            chatList.Add(Language.GetTextValue("Mods.Endgame.NpcDurthuText1"));
            chatList.Add(Language.GetTextValue("Mods.Endgame.NpcDurthuText2"));
            chatList.Add(Language.GetTextValue("Mods.Endgame.NpcDurthuText3"));
            chatList.Add(Language.GetTextValue("Mods.Endgame.NpcDurthuText5"));
            chatList.Add(Language.GetTextValue("Mods.Endgame.NpcDurthuText6") + _durthuNpcName);
            chatList.Add(Language.GetTextValue("Mods.Endgame.NpcDurthuText7"));
            chatList.Add(Language.GetTextValue("Mods.Endgame.NpcDurthuText8"));
            chatList.Add(Language.GetTextValue("Mods.Endgame.NpcDurthuText9"));
            chatList.Add(Language.GetTextValue("Mods.Endgame.NpcDurthuText10"));
            chatList.Add(Language.GetTextValue("Mods.Endgame.NpcDurthuText11"));
            chatList.Add(Language.GetTextValue("Mods.Endgame.NpcDurthuText12"));

            if (EndgameWorld.ZeerckSpawn)
                chatList.Add(Language.GetTextValue("Mods.Endgame.NpcDurthuText4") + _zeerckNpcName + Language.GetTextValue("Mods.Endgame.NpcDurthuText41"));

            if (!Main.dayTime && Main.bloodMoon)
            {
                chatList.Add(Language.GetTextValue("Mods.Endgame.NpcDurthuTextBloodMoon"));
            }

            if (Main.LocalPlayer.HasItem(ModContent.ItemType<Items.Conspectus>()) && !EndgameWorld.conspectusReturned && EndgameWorld.SudarinSpawn)
            {
                chatList.Add(Language.GetTextValue("Mods.Endgame.NpcDurthuTextConspectus1") + _sudarinNpcName + Language.GetTextValue("Mods.Endgame.NpcDurthuTextConspectus12"));
            }

            return chatList[Main.rand.Next(chatList.Count)];
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("Mods.Endgame.NpcDurthuTextButton");

            if(EndgameWorld.conspectusReturned)
                button2 = Language.GetTextValue("Mods.Endgame.NpcDurthuTextButton2");
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if(firstButton)
                EndgameDropper.DropItem(npc, ModContent.ItemType<Items.AssemblerTrophy>());
            else if(EndgameWorld.conspectusReturned)
            {
                EndgameUtils.PlayCustomLocalDelaySound(mod, Main.LocalPlayer.position,"Sounds/Custom/Yarik_beautiful", 500);
                EndgameUtils.DisplayLocalizedText(Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcDurthu>())].GivenName + Language.GetTextValue("Mods.Endgame.ForgetConspectus"), Colors.RarityGreen);
                Main.LocalPlayer.KillMe(PlayerDeathReason.ByCustomReason(Language.GetTextValue("Mods.Endgame.DeathReason2") + Main.LocalPlayer.name + Language.GetTextValue("Mods.Endgame.DeathReason21")), Main.LocalPlayer.statLife, -Main.LocalPlayer.direction);
                EndgameWorld.conspectusReturned = false;
            }
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
