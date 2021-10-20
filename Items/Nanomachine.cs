using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Endgame.Items
{
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
            
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
            
            item.maxStack = 1;
            item.rare = ItemRarityID.Expert;
            item.consumable = false;
        }

        public override bool UseItem(Player player)
        {
            Main.PlaySound(mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Borisich_mozhno_ne_pisat"), player.position);

            return true;
        }
    }
}
