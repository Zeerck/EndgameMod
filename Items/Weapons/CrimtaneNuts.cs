using Terraria;
using Terraria.ID;

namespace Endgame.Items.Weapons
{
    internal class CrimtaneNuts : NutsWeaponItem
	{
		private const int _maxTargets = 12;
		private const int _hitFrequency = 30;
		private const int _dustType = DustID.CrimsonTorch;
		private const float _focusRadius = 180f;

        CrimtaneNuts()
        {
            MaxTargets = _maxTargets;
            HitFrequency = _hitFrequency;
            DustType = _dustType;
            FocusRadius = _focusRadius;
        }

        public override void SetDefaults()
		{
			base.SetDefaults();
			Item.damage = 21;
			Item.knockBack = 5;
			Item.crit = 7;
		}

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.CrimtaneBar, 12)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}