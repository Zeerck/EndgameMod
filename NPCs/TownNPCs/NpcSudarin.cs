using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.Localization;

using System.Collections.Generic;

namespace Endgame.NPCs.TownNPCs
{
    [AutoloadHead]
    class NpcSudarin : ModNPC
    {
        private string _durthuNPCName;
        private string _zeerckNPCName;

        private readonly List<string> _names = new()
        {
            Language.GetTextValue("Mods.Endgame.NPCs.NpcSudarin.Names.SudarinName"),
            Language.GetTextValue("Mods.Endgame.NPCs.NpcSudarin.Names.SuadrinName"),
            Language.GetTextValue("Mods.Endgame.NPCs.NpcSudarin.Names.SudorinName"),
            Language.GetTextValue("Mods.Endgame.NPCs.NpcSudarin.Names.SDOGEName")
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
            if (EndgameWorld.SudarinExist)
                return;
            EndgameWorld.SudarinExist = true;
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
                    return EndgameWorld.SudarinExist;
            }
            return EndgameWorld.SudarinExist;
        }

        public override List<string> SetNPCNameList() => _names;

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            if (EndgameWorld.SudarinExist)
                EndgameWorld.SudarinExist = false;
        }

        public override bool CheckDead()
        {
            if (EndgameWorld.SudarinExist)
                EndgameWorld.SudarinExist = false;

            return true;
        }

        public override string GetChat()
        {
            if (EndgameWorld.DurthuExist)
                _durthuNPCName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcDurthu>())].GivenName;
            if (EndgameWorld.ZeerckExist)
                _zeerckNPCName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcZeerck>())].GivenName;

            List<string> chatList = new()
            {
                Language.GetTextValue("Mods.Endgame.Dialogue.NpcSudarin.StandartDialogue2"),
                Language.GetTextValue("Mods.Endgame.Dialogue.NpcSudarin.StandartDialogue4"),
                Language.GetTextValue("Mods.Endgame.Dialogue.NpcSudarin.StandartDialogue5"),
                Language.GetTextValue("Mods.Endgame.Dialogue.NpcSudarin.StandartDialogue6"),
                Language.GetTextValue("Mods.Endgame.Dialogue.NpcSudarin.StandartDialogue7"),
                Language.GetTextValue("Mods.Endgame.Dialogue.NpcSudarin.StandartDialogue8")
            };

            if (EndgameWorld.DurthuExist)
                chatList.Add(Language.GetTextValue("Mods.Endgame.Dialogue.NpcSudarin.StandartDialogue1", _durthuNPCName));

            if (EndgameWorld.ZeerckExist)
                chatList.Add(Language.GetTextValue("Mods.Endgame.Dialogue.NpcSudarin.StandartDialogue3", _zeerckNPCName));

            if (Main.LocalPlayer.HasItem(ModContent.ItemType<Items.Conspectus>()) && !EndgameWorld.conspektyReturned)
                chatList.Add(Language.GetTextValue("Mods.Endgame.Dialogue.NpcSudarin.ConspectusDialogue1"));

            return chatList[Main.rand.Next(chatList.Count)];
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("Mods.Endgame.Button.NpcSudarin.TextButton1");

            if (EndgameWorld.SudarinExist && Main.LocalPlayer.HasItem(ModContent.ItemType<Items.Conspectus>()) && !EndgameWorld.conspektyReturned)
                button2 = Language.GetTextValue("Mods.Endgame.Button.NpcSudarin.TextButton2");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            var source = NPC.GetSource_Loot();

            if (firstButton)
                EndgameDropper.DropItem(source, NPC, ModContent.ItemType<Items.TanosFigure>());
            else if (Main.LocalPlayer.InventoryHas(ModContent.ItemType<Items.Conspectus>()) && !EndgameWorld.conspektyReturned)
            {
                SoundEngine.PlaySound(new SoundStyle("Endgame/Sounds/Custom/Sudarin_AHCHO"), NPC.position);
                Main.LocalPlayer.ConsumeItem(ModContent.ItemType<Items.Conspectus>());
                EndgameWorld.conspektyReturned = true;
                EndgameUtils.DisplayLocalizedText("AH CHOOOOO!", Colors.RarityBlue); //TODO: Localize this
                EndgameUtils.AhChooKill(Language.GetTextValue("Mods.Endgame.DeathReason.NpcSudarin.ReturnConspectusDeathReason", Main.LocalPlayer.name, NPC.GivenName), NPC);
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
            multiplier = 2f;
        }
    }
}