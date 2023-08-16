using Terraria.ID;
using Terraria.ModLoader;

namespace Endgame.Items
{
    public class Conspectus : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 36;
            
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Expert;
        }
    }
}
