﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;

using System.Collections.Generic;

namespace Endgame.NPCs.TownNPCs
{
    [AutoloadHead]
    public class NpcDurthu : ModNPC
    {
        private string _durthuNPCName;
        private string _sudarinNPCName;
        private string _zeerckNPCName;

        private readonly List<string> _names = new()
        {
            Language.GetTextValue("Mods.Endgame.NPCs.NpcDurthu.Names.DurthuName"),
            Language.GetTextValue("Mods.Endgame.NPCs.NpcDurthu.Names.ComradeGruzoffName"),
            Language.GetTextValue("Mods.Endgame.NPCs.NpcDurthu.Names.ComradeGruzonName")
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
            NPC.DeathSound = SoundID.NPCDeath2;
        }

        public override void AI()
        {
            if (EndgameWorld.DurthuExist)
                return;
            EndgameWorld.DurthuExist = true;
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
                    return EndgameWorld.DurthuExist;
            }
            return EndgameWorld.DurthuExist;
        }

        public override List<string> SetNPCNameList() => _names;

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            if(EndgameWorld.DurthuExist)
                EndgameWorld.DurthuExist = false;
        }

        public override bool CheckDead()
        {
            if (EndgameWorld.DurthuExist)
                EndgameWorld.DurthuExist = false;

            return true;
        }

        public override string GetChat()
        {
            if (EndgameWorld.DurthuExist)
                _durthuNPCName = NPC.GivenName;
            if (EndgameWorld.SudarinExist)
                _sudarinNPCName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcSudarin>())].GivenName;
            if (EndgameWorld.ZeerckExist)
                _zeerckNPCName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcZeerck>())].GivenName;

            List<string> chatList = new()
            {
                Language.GetTextValue("Mods.Endgame.Dialogue.NpcDurthu.StandartDialogue1"),
                Language.GetTextValue("Mods.Endgame.Dialogue.NpcDurthu.StandartDialogue2"),
                Language.GetTextValue("Mods.Endgame.Dialogue.NpcDurthu.StandartDialogue3"),
                Language.GetTextValue("Mods.Endgame.Dialogue.NpcDurthu.StandartDialogue5"),
                Language.GetTextValue("Mods.Endgame.Dialogue.NpcDurthu.StandartDialogue6", _durthuNPCName),
                Language.GetTextValue("Mods.Endgame.Dialogue.NpcDurthu.StandartDialogue7"),
                Language.GetTextValue("Mods.Endgame.Dialogue.NpcDurthu.StandartDialogue8"),
                Language.GetTextValue("Mods.Endgame.Dialogue.NpcDurthu.StandartDialogue9"),
                Language.GetTextValue("Mods.Endgame.Dialogue.NpcDurthu.StandartDialogue10"),
                Language.GetTextValue("Mods.Endgame.Dialogue.NpcDurthu.StandartDialogue11"),
                Language.GetTextValue("Mods.Endgame.Dialogue.NpcDurthu.StandartDialogue12")
            };

            if (EndgameWorld.ZeerckExist)
                chatList.Add(Language.GetTextValue("Mods.Endgame.Dialogue.NpcDurthu.StandartDialogue4", _zeerckNPCName));

            if (!Main.dayTime && Main.bloodMoon)
                chatList.Add(Language.GetTextValue("Mods.Endgame.Dialogue.NpcDurthu.BloodMoonDialogue1"));

            if (Main.LocalPlayer.HasItem(ModContent.ItemType<Items.Conspectus>()) && !EndgameWorld.conspektyReturned && EndgameWorld.SudarinExist)
                chatList.Add(Language.GetTextValue("Mods.Endgame.Dialogue.NpcDurthu.ConspectusDialogue1", _sudarinNPCName));

            return chatList[Main.rand.Next(chatList.Count)];
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("Mods.Endgame.Button.NpcDurthu.TextButton1");

            if(EndgameWorld.conspektyReturned)
                button2 = Language.GetTextValue("Mods.Endgame.Button.NpcDurthu.TextButton2");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            var source = NPC.GetSource_Loot();

            if(firstButton)
                EndgameDropper.DropItem(source, NPC, ModContent.ItemType<Items.AssemblerTrophy>());
            else if(EndgameWorld.conspektyReturned)
            {
                EndgameUtils.PlayCustomLocalDelaySound(Main.LocalPlayer.position,"Endgame/Sounds/Custom/Durthu_beautiful", 500);
                EndgameUtils.DisplayLocalizedText(Language.GetTextValue("Mods.Endgame.ChatMessage.NpcDurthu.ForgetConspectusChatMessage1", NPC.GivenName), Colors.RarityGreen);
                Main.LocalPlayer.KillMe(PlayerDeathReason.ByCustomReason(Language.GetTextValue("Mods.Endgame.DeathReason.NpcDurthu.ForgetConspectusDeathReason", Main.LocalPlayer.name)), Main.LocalPlayer.statLife, -Main.LocalPlayer.direction);
                EndgameWorld.conspektyReturned = false;
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
            projType = ModContent.ProjectileType<Projectiles.NPCs.TownNPCsAttackProjectile>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 4f;
        }
    }
}
