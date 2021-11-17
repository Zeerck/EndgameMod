using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace Endgame
{
    public class EndgameGlobalTownNPC
    {
        public static void ExtraNPCQoutes(NPC npc, Mod mod, ref string chat)
        {
            int zeerckNPC = NPC.FindFirstNPC(ModContent.NPCType<NPCs.TownNPCs.NpcZeerck>());

            switch(npc.type)
            {
                case 22:
                    if (Utils.NextBool(Main.rand, 5) && EndgameWorld.ZeerckSpawn)
                        chat = Language.GetTextValue("Mods.Endgame.ExtraGuideQuote1", Main.npc[zeerckNPC].GivenName);
                    break;
            }
        }
    }
}
