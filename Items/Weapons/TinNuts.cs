using Terraria;
using Terraria.ID;

namespace Endgame.Items.Weapons
{
    internal class TinNuts : NutsWeaponItem
	{
		private const int _maxTargets = 6;
		private const int _hitFrequency = 55;
		private const int _dustType = DustID.TintableDust;
		private const float _focusRadius = 135f;

        TinNuts()
        {
            MaxTargets = _maxTargets;
            HitFrequency = _hitFrequency;
            DustType = _dustType;
            FocusRadius = _focusRadius;
        }

		public override void SetDefaults()
		{
			base.SetDefaults();
			Item.damage = 9;
			Item.knockBack = 4;
			Item.crit = 4;
		}

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.TinBar, 14)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}