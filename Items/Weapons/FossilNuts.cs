using Terraria;
using Terraria.ID;

namespace Endgame.Items.Weapons
{
    internal class FossilNuts : NutsWeaponItem
	{
		private const int _maxTargets = 14;
		private const int _hitFrequency = 45;
		private const int _dustType = DustID.Sand;
		private const float _focusRadius = 185f;

		FossilNuts()
        {
            MaxTargets = _maxTargets;
            HitFrequency = _hitFrequency;
            DustType = _dustType;
            FocusRadius = _focusRadius;
        }

		public override void SetDefaults()
		{
			base.SetDefaults();
			Item.damage = 10;
			Item.knockBack = 3;
			Item.crit = 4;
		}

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.FossilOre, 22)
				.AddIngredient(ItemID.Amber, 2)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}