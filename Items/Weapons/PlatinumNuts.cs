using Terraria;
using Terraria.ID;

namespace Endgame.Items.Weapons
{
    internal class PlatinumNuts : NutsWeaponItem
	{
		private const int _maxTargets = 14;
		private const int _hitFrequency = 40;
		private const int _dustType = DustID.PlatinumCoin;
		private const float _focusRadius = 180f;

        PlatinumNuts()
        {
            MaxTargets = _maxTargets;
            HitFrequency = _hitFrequency;
            DustType = _dustType;
            FocusRadius = _focusRadius;
        }

		public override void SetDefaults()
		{
			base.SetDefaults();
			Item.damage = 17;
			Item.knockBack = 4;
			Item.crit = 4;
		}

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.PlatinumBar, 12)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}