using Terraria;
using Terraria.ID;

namespace Endgame.Items.Weapons
{
    internal class IronNuts : NutsWeaponItem
	{
		private const int _maxTargets = 9;
		private const int _hitFrequency = 48;
		private const int _dustType = DustID.Iron;
		private const float _focusRadius = 155f;

		IronNuts()
        {
            MaxTargets = _maxTargets;
            HitFrequency = _hitFrequency;
            DustType = _dustType;
            FocusRadius = _focusRadius;
        }

		public override void SetDefaults()
		{
			base.SetDefaults();
			Item.damage = 12;
			Item.knockBack = 4;
			Item.crit = 4;
		}

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IronBar, 10)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}