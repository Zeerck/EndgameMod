using Terraria;
using Terraria.ModLoader;

namespace Endgame
{
    public class EndgameGlobalNPC : GlobalNPC
    {
        public override void GetChat(NPC npc, ref string chat) => EndgameGlobalTownNPC.ExtraNPCQoutes(npc, mod, ref chat);

        public override void NPCLoot(NPC npc)
        {
            if (!npc.boss && !npc.friendly && npc.CanBeChasedBy())
                EndgameDropper.DropItem(npc, ModContent.ItemType<Items.ProgramerSoul>(), 1, 10);
            if(npc.boss)
                EndgameDropper.DropItem(npc, ModContent.ItemType<Items.ProgramerSoul>(), 20000, 50000);
        }
    }
}
