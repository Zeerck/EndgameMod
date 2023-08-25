using Terraria;
using Terraria.ID;

namespace Endgame.Items.Weapons
{
    internal class WoodNuts : NutsWeaponItem
    {
        private const int _maxTargets = 5;
        private const int _hitFrequency = 60;
        private const int _dustType = DustID.WoodFurniture;
        private const float _focusRadius = 120f;

        WoodNuts()
        {
            MaxTargets = _maxTargets;
            HitFrequency = _hitFrequency;
            DustType = _dustType;
            FocusRadius = _focusRadius;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.damage = 6;
            Item.knockBack = 4;
            Item.crit = 4;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 20)
                .AddIngredient(ItemID.DirtBlock, 10)
                .AddIngredient(ItemID.Silk, 2)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
