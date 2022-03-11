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
            DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.Common.NanomachineName"));
            Tooltip.SetDefault(Language.GetTextValue("Mods.Endgame.Common.NanomachineDescription"));
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 30;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = 30000;
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item79;
            Item.noMelee = true;
            Item.mountType = ModContent.MountType<Mounts.Nanomachine>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(4)
                .AddIngredient(ModContent.ItemType<ProgramerSoul>(), 57000)
                .AddIngredient(ItemID.Nanites, 450)
                .AddIngredient(ItemID.ChlorophyteBar, 5)
                .AddIngredient(ItemID.HallowedBar, 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
