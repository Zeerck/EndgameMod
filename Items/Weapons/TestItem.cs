using Terraria;

namespace Endgame.Items.Weapons
{
    public class TestItem : NutsWeaponItem
    {
        public TestItem() => MaxTargets = 5;

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.damage = 1;
            Item.knockBack = 2;
            Item.crit = 3;
        }

        public override bool CanUseItem(Player player) => false;
    }
}
