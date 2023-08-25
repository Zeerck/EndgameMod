using Terraria;
using Terraria.ID;

namespace Endgame.Items.Weapons
{
    internal class HellstoneNuts : NutsWeaponItem
	{
		private const int _maxTargets = 11;
		private const int _hitFrequency = 40;
		private const int _dustType = DustID.Lava;
		private const int _dustTypeTarget = DustID.Torch;
		private const float _focusRadius = 250f;
		
		HellstoneNuts()
        {
            MaxTargets = _maxTargets;
            HitFrequency = _hitFrequency;
            DustType = _dustType;
            FocusRadius = _focusRadius;
        }

		public override void SetDefaults()
		{
			base.SetDefaults();
			Item.damage = 30;
			Item.knockBack = 6;
			Item.crit = 8;
		}

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.HellstoneBar, 9)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}