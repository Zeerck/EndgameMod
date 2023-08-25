using Terraria;
using Terraria.ID;

namespace Endgame.Items.Weapons
{
    internal class DemoniteNuts : NutsWeaponItem
	{
		private const int _maxTargets = 12;
		private const int _hitFrequency = 35;
		private const int _dustType = DustID.DemonTorch;
		private const float _focusRadius = 175f;

		DemoniteNuts()
        {
            MaxTargets = _maxTargets;
            HitFrequency = _hitFrequency;
            DustType = _dustType;
            FocusRadius = _focusRadius;
        }

		public override void SetDefaults()
		{
			base.SetDefaults();
			Item.damage = 22;
			Item.knockBack = 5;
			Item.crit = 6;
		}

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.DemoniteBar, 12)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}