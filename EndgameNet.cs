using Terraria;
using Terraria.ID;

namespace Endgame
{
    public class EndgameNet
    {        
        public static void SyncWorld()
        {
            if (Main.netMode != NetmodeID.Server)
                return;

            NetMessage.SendData(MessageID.WorldData);
        }
    }
}
