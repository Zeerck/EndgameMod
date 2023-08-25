using Terraria;
using Terraria.ID;

namespace Endgame.Items.Weapons
{
    internal class LeadNuts : NutsWeaponItem
	{
		private const int _maxTargets = 9;
		private const int _hitFrequency = 50;
		private const int _dustType = DustID.Lead;
		private const float _focusRadius = 155f;
		
		LeadNuts()
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
			Item.knockBack = 4;
			Item.crit = 5;
		}

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.LeadBar, 10)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}