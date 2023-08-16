using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Endgame.Items.Weapons
{
	internal class TungstenNuts : ModItem
	{
		private const int _maxTargets = 12;
		private const int _hitFrequency = 45;
		private const int _dustType = DustID.Tungsten;
		private const float _focusRadius = 165f;

		public override void SetStaticDefaults()
		{
			double delayBetweenHits = Math.Round(_hitFrequency / 60f, 2);
			double focusRadiusInBlocks = Math.Round(_focusRadius / 10f / 2, 2);

			Item.ResearchUnlockCount = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 13;
			Item.knockBack = 5;
			Item.crit = 5;
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
				.AddIngredient(ItemID.TungstenBar, 8)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}