using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Endgame.NPCs.TownNPCs;

namespace Endgame
{
    public class EndgameGlobalTownNPC
    {
        public static void ExtraNPCQoutes(NPC npc, Mod mod, ref string chat)
        {
            int durthuNpc = NPC.FindFirstNPC(ModContent.NPCType<NpcDurthu>());
            int sudarinNpc = NPC.FindFirstNPC(ModContent.NPCType<NpcSudarin>());
            int zeerckNpc = NPC.FindFirstNPC(ModContent.NPCType<NpcZeerck>());

            switch(npc.type)
            {
                case NPCID.Guide:
                    if (Utils.NextBool(Main.rand, 5) && EndgameWorld.DurthuExist)
                        chat = Language.GetTextValue("Mods.Endgame.ExtraNPCQoutes.Guide.ExtraQuote1", Main.npc[durthuNpc].GivenName);
                    if (Utils.NextBool(Main.rand, 5) && EndgameWorld.SudarinExist)
                        chat = Language.GetTextValue("Mods.Endgame.ExtraNPCQoutes.Guide.ExtraQuote2", Main.npc[sudarinNpc].GivenName);
                    if (Utils.NextBool(Main.rand, 5) && EndgameWorld.ZeerckExist)
                        chat = Language.GetTextValue("Mods.Endgame.ExtraNPCQoutes.Guide.ExtraQuote3", Main.npc[zeerckNpc].GivenName);
                    break;
            }
        }
    }
}
