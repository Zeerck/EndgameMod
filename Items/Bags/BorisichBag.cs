using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Endgame.NPCs.Bosses;

namespace Endgame.Items.Bags
{
    public class BorisichBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.BossBag[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 32;
            
            Item.maxStack = 999;
            Item.consumable = true;
            Item.rare = ItemRarityID.Cyan;
            Item.expert = true;
        }

        public override bool CanRightClick() => true;

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<BorisichEndgameBoss>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Conspectus>(), 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.WaterBucket, 1, 42, 42));
        }
    }
}
