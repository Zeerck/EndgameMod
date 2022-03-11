using Terraria;
using Terraria.ModLoader;

namespace Endgame
{
    public class EndgameGlobalNPC : GlobalNPC
    {
        public override void GetChat(NPC npc, ref string chat) => EndgameGlobalTownNPC.ExtraNPCQoutes(npc, Mod, ref chat);

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            var source = npc.GetItemSource_Loot();

            if (!npc.boss && !npc.friendly && npc.CanBeChasedBy())
                EndgameDropper.DropItem(source, npc, ModContent.ItemType<Items.ProgramerSoul>(), 1, 10);
            if(npc.boss && !Main.hardMode)
                EndgameDropper.DropItem(source, npc, ModContent.ItemType<Items.ProgramerSoul>(), 5000, 20000);
            if (npc.boss && Main.hardMode)
                EndgameDropper.DropItem(source, npc, ModContent.ItemType<Items.ProgramerSoul>(), 10000, 30000);
        }
    }
}
