using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using System.IO;

namespace Endgame
{
    class EndgameWorld : ModWorld
    {
        public static bool DurthuSpawn = false;
        public static bool SudarinSpawn = false;
        public static bool ZeerckSpawn = false;

        public static bool foundHomeAnarchist = false;
        public static bool foundHomeLordPepegon = false;
        public static bool foundHomeFatherOfAllMilfs = false;

        public static bool conspectusReturned = false;

        public static bool borisichDefeated = false;

        public override void Initialize()
        {
            ZeerckSpawn = false;
            SudarinSpawn = false;
            DurthuSpawn = false;

            foundHomeAnarchist = false;
            foundHomeLordPepegon = false;
            foundHomeFatherOfAllMilfs = false;

            conspectusReturned = false;

            borisichDefeated = false;
        }

        public override TagCompound Save()
        {
            var Defeated = new List<string>();
            TagCompound tagCompound = new TagCompound();

            if (borisichDefeated)
                Defeated.Add("borisich");

            return tagCompound;
        }

        public override void Load(TagCompound tag)
        {
            var Defeated = tag.GetList<string>("Defeated");
            borisichDefeated = Defeated.Contains("borisichBoss");
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = borisichDefeated;

            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            borisichDefeated = flags[0];
        }
    }
}
