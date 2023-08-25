using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace Endgame
{
    public class EndgameGlobalNPC : GlobalNPC
    {
        public override void GetChat(NPC npc, ref string chat) => EndgameGlobalTownNPC.ExtraNPCQoutes(npc, Mod, ref chat);

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (!npc.boss && !npc.friendly && npc.CanBeChasedBy())
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.ProgramerSoul>(), 1, 1, 10));
            if (npc.boss && !Main.hardMode)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.ProgramerSoul>(), 1, 5000, 20000));
            if (npc.boss && Main.hardMode)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.ProgramerSoul>(), 1, 10000, 30000));
        }
    }
}
