using Terraria;
using Terraria.ID;

namespace Endgame.Items.Weapons
{
    internal class GoldNuts : NutsWeaponItem
	{
		private const int _maxTargets = 11;
		private const int _hitFrequency = 40;
		private const int _dustType = DustID.GoldCoin;
		private const float _focusRadius = 170f;

        GoldNuts()
		{
            MaxTargets = _maxTargets;
            HitFrequency = _hitFrequency;
            DustType = _dustType;
            FocusRadius = _focusRadius;
        }

		public override void SetDefaults()
		{
			base.SetDefaults();
			Item.damage = 16;
			Item.knockBack = 4;
			Item.crit = 5;
		}

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.GoldBar, 14)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}