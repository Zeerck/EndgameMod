using Terraria;
using Terraria.ID;

namespace Endgame.Items.Weapons
{
    //TODO: Опционально, добавить больше урона нежити
    internal class SilverNuts : NutsWeaponItem
	{
		private const int _maxTargets = 12;
		private const int _hitFrequency = 45;
		private const int _dustType = DustID.SilverCoin;
		private const float _focusRadius = 170f;

        SilverNuts()
        {
            MaxTargets = _maxTargets;
            HitFrequency = _hitFrequency;
            DustType = _dustType;
            FocusRadius = _focusRadius;
        }

		public override void SetDefaults()
		{
			base.SetDefaults();
			Item.damage = 14;
			Item.knockBack = 4;
			Item.crit = 4;
		}

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.SilverBar, 11)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}