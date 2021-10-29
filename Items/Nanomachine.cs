using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace Endgame.Items
{
    //TODO: Nanomachine - Mount
    public class Nanomachine : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.NanomachineName"));
            Tooltip.SetDefault(Language.GetTextValue("Mods.Endgame.NanomachineDescription"));
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            
            item.maxStack = 1;
            item.rare = ItemRarityID.Expert;
        }
    }
}
