using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Endgame.Items
{
    public class Nanomachine : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Icon of the Holy All-Writing");
            Tooltip.SetDefault("The beginning of the Endgame");
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
