using Terraria;
using Terraria.ID;
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
                case NPCID.Guide:
                    if (Utils.NextBool(Main.rand, 5) && EndgameWorld.ZeerckExist)
                        chat = Language.GetTextValue("Mods.Endgame.Common.ExtraGuideQuote1", Main.npc[zeerckNPC].GivenName);
                    break;
            }
        }
    }
}
