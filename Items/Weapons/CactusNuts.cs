using Terraria;
using Terraria.ID;

namespace Endgame.Items.Weapons
{
    internal class CactusNuts : NutsWeaponItem
	{
		private const int _maxTargets = 7;
		private const int _hitFrequency = 50;
		private const int _dustType = DustID.OasisCactus;
		private const float _focusRadius = 130f;

        CactusNuts()
        {
            MaxTargets = _maxTargets;
            HitFrequency = _hitFrequency;
            DustType = _dustType;
            FocusRadius = _focusRadius;
        }

        public override void SetDefaults()
		{
			base.SetDefaults();
			Item.damage = 11;
			Item.knockBack = 4;
			Item.crit = 5;
		}

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Cactus, 15)
				.AddIngredient(ItemID.SandBlock, 5)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}