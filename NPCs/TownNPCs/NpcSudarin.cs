using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Endgame.NPCs.TownNPCs
{
    [AutoloadHead]
    class NpcSudarin : ModNPC
    {
        private static readonly List<string> _names = new List<string>()
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
            if (EndgameWorld.SudarinSpawn)
                return;
            EndgameWorld.SudarinSpawn = true;
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
                    return EndgameWorld.SudarinSpawn;
            }
            return EndgameWorld.SudarinSpawn;
        }

        public override string TownNPCName()
        {
            return _names[WorldGen.genRand.Next(_names.Count)];
        }

        public override void NPCLoot()
        {
            EndgameWorld.SudarinSpawn = false;
        }

        public override string GetChat()
        {
            string DurthuNpcName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcDurthu>())].GivenName;
            string SudarinNpcName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcSudarin>())].GivenName;
            string ZeerckNpcName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcZeerck>())].GivenName;

            List<string> chatList = new List<string>();

            chatList.Add($"{DurthuNpcName} was our chief commander in the times, when we was in Storm Team.");
            chatList.Add("I participated in the war against the aliens. They sent at us giant ants, frogs, bees and etc.");
            chatList.Add($"An interesting fact, once {ZeerckNpcName} name was \"Majima Bitch\".");
            chatList.Add("Nanomachines SON!");
            chatList.Add("Napas - Lavandos.");
            chatList.Add("After all, there are really a lot of cops in Los Santos.");
            chatList.Add("**\"Simon: You owe me a favor, remember?\"**\nAh, Again...");
            chatList.Add("Everyone is afraid of Jumbo, but no one knows what he is afraid of.");

            if (Main.LocalPlayer.HasItem(ModContent.ItemType<Items.Conspectus>()))
            {
                chatList.Add("Oh! My Conspectus! With their help, I can teach you the skills of a shadow scribe.");
            }

            return chatList[Main.rand.Next(chatList.Count)];
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Give Green Man";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            EndgameDropper.DropItem(npc, ModContent.ItemType<Items.TanosFigure>());
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

        public override void TownNPCAttackProjSpeed(
          ref float multiplier,
          ref float gravityCorrection,
          ref float randomOffset)
        {
            multiplier = 2f;
        }
    }
}