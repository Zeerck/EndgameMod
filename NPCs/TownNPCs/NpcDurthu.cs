using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Endgame.NPCs.TownNPCs
{
    [AutoloadHead]
    public class NpcDurthu : ModNPC
    {
        private string _npcName;

        private static readonly List<string> _names = new List<string>()
        {
            "Durthu",
            "Comrade Gruzoff",
            "Comrade",
            "Gruzoff"
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
            _npcName = _names[Main.rand.Next(_names.Count)];
            return _npcName;
        }

        public override void NPCLoot()
        {
            EndgameWorld.DurthuSpawn = false;
        }

        public override string GetChat()
        {
            string DurthuNpcName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcDurthu>())].GivenName;
            string SudarinNpcName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcSudarin>())].GivenName;
            string ZeerckNpcName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcZeerck>())].GivenName;

            List<string> chatList = new List<string>();

            chatList.Add("Many people say I'm greedy, but as for me 26 million pieces of silver is not much.");
            chatList.Add("You know, actually, i'm too all-writing.");
            chatList.Add("I trust Vizart before...");
            chatList.Add($"There was a war in a galaxy just because {ZeerckNpcName} invited a wife of galactic emperor to \"drink some tea\".");
            chatList.Add("You know, i still have flashbacks from the time we were in Storm Team... I just hope i will never ever meet this black-yellow \"bakugans\" again.");
            chatList.Add($"Fabrics to Factories, Workers to Peasants, Milf's to {DurthuNpcName}.");
            chatList.Add("Do not trust the samurai of the Trotsky clan.");
            chatList.Add("I tried to reach him, but get name \"Demogog\".");
            chatList.Add("I have a device in my pocket that contains over billions universes.");
            chatList.Add("Words like \"Continue\" and \"Why you don't writing?...\" i still get shivers.");
            chatList.Add("**\"Simon: You owe me a favor, remember?\"**\nNo, i don't remember.");
            chatList.Add("Please never say \"LET'S GO\", \"COME WITH ME\", \"POSHLI\" and \"POIDEM\". NEVER!");

            if (!Main.dayTime && Main.bloodMoon)
            {
                chatList.Add("YOU DON'T KNOW WHAT IT WILL LEAD TO!");
            }

            if (Main.LocalPlayer.HasItem(ModContent.ItemType<Items.Conspectus>()))
            {
                chatList.Add($"Oh, you have {SudarinNpcName}'s Conspectus! I think you should contact him with them.");
            }

            return chatList[Main.rand.Next(chatList.Count)];
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Give Trophy";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            EndgameDropper.DropItem(npc, ModContent.ItemType<Items.AssemblerTrophy>());
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
