using Terraria;
using Terraria.ID;

namespace Endgame.Items.Weapons
{
    internal class FrostyNuts : NutsWeaponItem
	{
		private const int _maxTargets = 10;
		private const int _hitFrequency = 35;
		private const int _dustType = DustID.Frost;
		private const float _focusRadius = 180f;

		FrostyNuts()
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
			Item.knockBack = 4.75f;
			Item.crit = 6;
		}
    }
}