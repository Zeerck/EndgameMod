using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace Endgame.Items
{
    public class Conspectus : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.ConspectusName"));
            Tooltip.SetDefault(Language.GetTextValue("Mods.Endgame.ConspectusDescription"));
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 36;

            item.maxStack = 1;
            item.rare = ItemRarityID.Expert;
        }
    }
}
