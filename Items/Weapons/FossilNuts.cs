using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Endgame.Items.Weapons
{
	internal class FossilNuts : ModItem
	{
		private const int _maxTargets = 14;
		private const int _hitFrequency = 45;
		private const int _dustType = DustID.Sand;
		private const float _focusRadius = 185f;

		public override void SetStaticDefaults()
		{
			double delayBetweenHits = Math.Round(_hitFrequency / 60f, 2);
			double focusRadiusInBlocks = Math.Round(_focusRadius / 10f / 2, 2);

			Item.ResearchUnlockCount = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.knockBack = 3;
			Item.crit = 4;
			Item.holdStyle = ItemHoldStyleID.HoldLamp;
		}

		public override bool CanUseItem(Player player) => false;

		public override void HoldItem(Player player)
		{
            EndgameUtils.DoNutsDamage(player, Item, _maxTargets, _hitFrequency, _dustType, _focusRadius);
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