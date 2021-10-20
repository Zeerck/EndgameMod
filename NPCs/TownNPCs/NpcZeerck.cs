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
        private string _npcName;

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
            if (EndgameWorld.ZeerckSpawn)
                return;
            EndgameWorld.ZeerckSpawn = true;
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
                    return EndgameWorld.ZeerckSpawn;
            }
            return EndgameWorld.ZeerckSpawn;
        }

        public override string TownNPCName()
        {
            return _npcName = _names[WorldGen.genRand.Next(_names.Count)];
        }

        public override void NPCLoot()
        {
            EndgameWorld.ZeerckSpawn = false;
        }

        public override string GetChat()
        {
            string DurthuNpcName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcDurthu>())].GivenName;
            string SudarinNpcName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcSudarin>())].GivenName;
            string ZeerckNpcName = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NpcZeerck>())].GivenName;

            List<string> chatList = new List<string>();

            chatList.Add("Check!");
            chatList.Add("I have a vehicle that breaks from almost any spit, and it costs me 24,000 pieces of silver to fix it. I think to replace it with something, but I don't even know what...");
            chatList.Add($"I heard that {SudarinNpcName} wrote everything.");
            chatList.Add($"Have you seen the icon of the holy all-writing? Yes, there is a photo of {SudarinNpcName} himself.");
            chatList.Add($"Be careful, {DurthuNpcName} weighs 300 kilograms and is capable of breaking through a wall if he wants to.");
            chatList.Add($"During the 40,000 year war, {DurthuNpcName} and I fought against everyone and spread corruption everywhere. Yes, those were good times...");
            chatList.Add("Borisich called us demagogues. I still didn’t understand what he didn’t like in the phrase \"You are wrong.\"");
            chatList.Add("I hate gnomes...");
            chatList.Add("Where are you, Jabroni v Tanke?...");
            chatList.Add("I hate logs...");
            chatList.Add("EGGS");
            chatList.Add("**\"Simon: You owe me a favor, remember?\"**\nI will return everything! Just leave me alone!");
            chatList.Add("I'm just a number riding solo in the code. Half Human, half Machine.");


            if (!Main.dayTime && !Main.bloodMoon)
                chatList.Add("Sometimes I have strange dreams, where I am in the same bed with Borisich. It horny me...");

            if (ZeerckNpcName == "Zeerck")
            {
                chatList.Add("My jokes have ruined millions of lives. Everyone says they are not funny. I don't think so.");
                chatList.Add("Infinity Gauntlet... Thanos' Wish... My jokes... I think it all has something in common.");
            }

            if (ZeerckNpcName == "Zeercky")
                chatList.Add("We are Zeercky, there are many of us...");

            if (ZeerckNpcName == "Zeer4eck")
                chatList.Add("Have you heard anything about Zeerck? I hope he didn't give you any trouble with his jokes.");

            if (ZeerckNpcName == "Dmitrix")
                chatList.Add("I have a brother on the moon with a huge hammer. He also has a very strange head and pale skin.");

            if (ZeerckNpcName == "Dima 800")
            {
                chatList.Add("I got my name from the great war against billions of zombies.");
                chatList.Add("Have you ever heard about challenges?");
            }

            if (Main.LocalPlayer.HasItem(ModContent.ItemType<Items.Conspectus>()))
                chatList.Add($"Oh, you have Conspectus, I think you should return them to {SudarinNpcName}.");


            return chatList[Main.rand.Next(chatList.Count)];
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Give Icon";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            EndgameDropper.DropItem(npc, ModContent.ItemType<Items.IconOfHolyAllWriting>());
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
