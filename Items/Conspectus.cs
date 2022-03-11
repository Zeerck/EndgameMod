using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace Endgame.Items
{
    public class Conspectus : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.Common.ConspectusName"));
            Tooltip.SetDefault(Language.GetTextValue("Mods.Endgame.Common.ConspectusDescription"));
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 36;
            
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Expert;
        }
    }
}
