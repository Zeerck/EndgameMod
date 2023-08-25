using Terraria;
using Terraria.ID;

namespace Endgame.Items.Weapons
{
    internal class TungstenNuts : NutsWeaponItem
	{
		private const int _maxTargets = 12;
		private const int _hitFrequency = 45;
		private const int _dustType = DustID.Tungsten;
		private const float _focusRadius = 165f;

        TungstenNuts()
        {
            MaxTargets = _maxTargets;
            HitFrequency = _hitFrequency;
            DustType = _dustType;
            FocusRadius = _focusRadius;
        }

		public override void SetDefaults()
		{
			base.SetDefaults();
			Item.damage = 13;
			Item.knockBack = 5;
			Item.crit = 5;
		}

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.TungstenBar, 8)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}