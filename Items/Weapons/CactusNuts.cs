﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

namespace Endgame.Items.Weapons
{
	internal class CactusNuts : ModItem
	{
		private const int _maxTargets = 7;
		private const int _hitFrequency = 50;
		private const int _dustType = DustID.OasisCactus;
		private const float _focusRadius = 130f;

		public override void SetStaticDefaults()
		{
			double delayBetweenHits = Math.Round(_hitFrequency / 60f, 2);
			double focusRadiusInBlocks = Math.Round(_focusRadius / 10f / 2, 2);

			// DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.Items.Weapons.CactusNutsName"));
			// Tooltip.SetDefault(Language.GetTextValue("Mods.Endgame.Items.Weapons.CactusNutsDescription", _maxTargets, delayBetweenHits, focusRadiusInBlocks));
			Item.ResearchUnlockCount = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 11;
			Item.knockBack = 4;
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
				.AddIngredient(ItemID.Cactus, 15)
				.AddIngredient(ItemID.SandBlock, 5)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}