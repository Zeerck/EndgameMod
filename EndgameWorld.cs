using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using System.IO;

namespace Endgame
{
    class EndgameWorld : ModWorld
    {
        public static bool borisichDefeated = false;
        public static bool ZeerckSpawn = false;
        public static bool SudarinSpawn = false;
        public static bool DurthuSpawn = false;
        public static bool foundHomeAnarchist = false;
        public static bool conspectusReturned = false;

        public override void Initialize()
        {
            borisichDefeated = false;
            ZeerckSpawn = false;
            SudarinSpawn = false;
            DurthuSpawn = false;
            foundHomeAnarchist = false;
            conspectusReturned = false;
        }

        public override TagCompound Save()
        {
            var Defeated = new List<string>();
            if (borisichDefeated) Defeated.Add("borisich");

            TagCompound tagCompound = new TagCompound();

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
