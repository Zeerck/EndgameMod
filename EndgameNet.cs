using Terraria;
using Terraria.ID;

namespace Endgame
{
    public class EndgameNet
    {        
        public static void SyncWorld()
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                return;
            else
                NetMessage.SendData(MessageID.WorldData);

            //if (Main.netMode != NetmodeID.Server || Main.netMode != NetmodeID.MultiplayerClient)
            //    return;
        }
    }
}
