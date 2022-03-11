using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using System.IO;

namespace Endgame
{
    public class EndgameWorld : ModSystem
    {
        public static bool borisichDefeated = false;
        public static bool ZeerckExist = false;
        public static bool SudarinExist = false;
        public static bool DurthuExist = false;
        public static bool foundHomeAnarchist = false;
        public static bool conspektyReturned = false;
        public static bool GreenManSpawn = false;

        public override void SaveWorldData(TagCompound tag)
        {
            var Defeated = new List<string>();
            if (borisichDefeated) Defeated.Add("borisichBoss");
        }

        public override void LoadWorldData(TagCompound tag)
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
