using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

using System.Collections.Generic;

namespace Endgame.NPCs.TownNPCs
{
    [AutoloadHead]
    class NpcSudarin : ModNPC
    {
        private string _durthuNpcName;
        private string _zeerckNpcName;

        private readonly List<string> _names = new List<string>()
        {
            Language.GetTextValue("Mods.Endgame.SudarinName"),
            Language.GetTextValue("Mods.Endgame.SuadrinName"),
            Language.GetTextValue("Mods.Endgame.SudorinName"),
            Language.GetTextValue("Mods.Endgame.SDOGEName")
        };

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lord Pepegon");

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
            if (EndgameWorld.SudarinSpawn)
                return;
            EndgameWorld.SudarinSpawn = true;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money) => EndgameUtils.TownNpcSpawn();

        public override string TownNPCName() => _names[WorldGen.genRand.Next(_names.Count)];

        public override void NPCLoot()
        {
            if (EndgameWorld.SudarinSpawn)
                EndgameWorld.SudarinSpawn = false;
        }

        public override string GetChat()
        {
            if (EndgameWorld.DurthuSpawn)
                _durthuNpcName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcDurthu>())].GivenName;
            if (EndgameWorld.ZeerckSpawn)
                _zeerckNpcName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcZeerck>())].GivenName;

            List<string> chatList = new List<string>
            {
                Language.GetTextValue("Mods.Endgame.NpcSudarinText2"),
                Language.GetTextValue("Mods.Endgame.NpcSudarinText4"),
                Language.GetTextValue("Mods.Endgame.NpcSudarinText5"),
                Language.GetTextValue("Mods.Endgame.NpcSudarinText6"),
                Language.GetTextValue("Mods.Endgame.NpcSudarinText7"),
                Language.GetTextValue("Mods.Endgame.NpcSudarinText8")
            };

            if (EndgameWorld.DurthuSpawn)
                chatList.Add(_durthuNpcName + Language.GetTextValue("Mods.Endgame.NpcSudarinText1"));

            if (EndgameWorld.ZeerckSpawn)
                chatList.Add(Language.GetTextValue("Mods.Endgame.NpcSudarinText3") + _zeerckNpcName + Language.GetTextValue("Mods.Endgame.NpcSudarinText31"));

            if (Main.LocalPlayer.HasItem(ModContent.ItemType<Items.Conspectus>()) && !EndgameWorld.conspectusReturned)
                chatList.Add(Language.GetTextValue("Mods.Endgame.NpcSudarinTextConspectus"));

            return chatList[Main.rand.Next(chatList.Count)];
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("Mods.Endgame.NpcSudarinTextButton");

            if (EndgameWorld.SudarinSpawn && Main.LocalPlayer.HasItem(ModContent.ItemType<Items.Conspectus>()) && !EndgameWorld.conspectusReturned)
                button2 = Language.GetTextValue("Mods.Endgame.NpcSudarinTextButton2");
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
                EndgameDropper.DropItem(npc, ModContent.ItemType<Items.TanosFigure>());
            else if (Main.LocalPlayer.InventoryHas(ModContent.ItemType<Items.Conspectus>()) && !EndgameWorld.conspectusReturned)
            {
                Main.PlaySound(50, Main.LocalPlayer.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Sudarin_AHCHO"));
                Main.LocalPlayer.ConsumeItem(ModContent.ItemType<Items.Conspectus>());
                EndgameWorld.conspectusReturned = true;
                EndgameUtils.DisplayLocalizedText("AH CHOOOOO!", Colors.RarityBlue);
                EndgameUtils.AhChooKill();
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