using Terraria;
using Terraria.ID;
using Endgame.Buffs;
using Terraria.ModLoader;

namespace Endgame.Items
{
    public class Conspectus : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Conspectus");
            Tooltip.SetDefault("You feel moisture.\nIt looks like there is a lot of water in it.\nGives immunity to fire, water walking and underwater breathing.");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 36;

            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = ItemUseStyleID.EatingUsing;

            item.maxStack = 1;
            item.buffTime = 160000;
            item.rare = ItemRarityID.Expert;
            item.buffType = ModContent.BuffType<WhoWroteEverythingBuff>();
        }

        public override bool UseItem(Player player)
        {
            Main.PlaySound(mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Borisich_davaite_zapishem"), player.position);

            //mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Borisich_davaite_zapishem");
            return true;
        }
    }
}
