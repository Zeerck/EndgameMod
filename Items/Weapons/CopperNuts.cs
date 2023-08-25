using Terraria;
using Terraria.ID;

namespace Endgame.Items.Weapons
{
    internal class CopperNuts : NutsWeaponItem
	{
		private const int _maxTargets = 6;
		private const int _hitFrequency = 55;
		private const int _dustType = DustID.CopperCoin;
		private const float _focusRadius = 135f;

        CopperNuts()
        {
            MaxTargets = _maxTargets;
            HitFrequency = _hitFrequency;
            DustType = _dustType;
            FocusRadius = _focusRadius;
        }

		public override void SetDefaults()
		{
			base.SetDefaults();
			Item.damage = 8;
			Item.knockBack = 4;
			Item.crit = 3;
		}

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.CopperBar, 8)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}