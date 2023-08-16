using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

namespace Endgame.Items.Weapons
{
	internal class DemoniteNuts : ModItem
	{
		private const int _maxTargets = 12;
		private const int _hitFrequency = 35;
		private const int _dustType = DustID.DemonTorch;
		private const float _focusRadius = 175f;

		public override void SetStaticDefaults()
		{
			double delayBetweenHits = Math.Round(_hitFrequency / 60f, 2);
			double focusRadiusInBlocks = Math.Round(_focusRadius / 10f / 2, 2);

			// DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.Items.Weapons.DemoniteNutsName"));
			// Tooltip.SetDefault(Language.GetTextValue("Mods.Endgame.Items.Weapons.DemoniteNutsDescription", _maxTargets, delayBetweenHits, focusRadiusInBlocks));
			Item.ResearchUnlockCount = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 22;
			Item.knockBack = 5;
			Item.crit = 6;
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
				.AddIngredient(ItemID.DemoniteBar, 12)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}