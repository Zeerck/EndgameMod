using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Endgame.Items.Weapons
{
	internal class HellstoneNuts : ModItem
	{
		private const int _maxTargets = 11;
		private const int _hitFrequency = 40;
		private const int _dustType = DustID.Lava;
		private const int _dustTypeTarget = DustID.Torch;
		private const float _focusRadius = 250f;

		public override void SetStaticDefaults()
		{
			double delayBetweenHits = Math.Round(_hitFrequency / 60f, 2);
			double focusRadiusInBlocks = Math.Round(_focusRadius / 10f / 2, 2);

			Item.ResearchUnlockCount = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.knockBack = 6;
			Item.crit = 8;
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
				.AddIngredient(ItemID.HellstoneBar, 9)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}