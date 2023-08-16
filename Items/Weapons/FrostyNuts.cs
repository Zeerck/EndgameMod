﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Localization;

using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

namespace Endgame.Items.Weapons
{
	internal class FrostyNuts : ModItem
	{
		private const int _maxTargets = 10;
		private const int _hitFrequency = 35;
		private const int _dustType = DustID.Frost;
		private const float _focusRadius = 180f;

		public override void SetStaticDefaults()
		{
			double delayBetweenHits = Math.Round(_hitFrequency / 60f, 2);
			double focusRadiusInBlocks = Math.Round(_focusRadius / 10f / 2, 2);

			// DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.Items.Weapons.FrostyNutsName"));
			// Tooltip.SetDefault(Language.GetTextValue("Mods.Endgame.Items.Weapons.FrostyNutsDescription", _maxTargets, delayBetweenHits, focusRadiusInBlocks));
			Item.ResearchUnlockCount = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 17;
			Item.knockBack = 4.75f;
			Item.crit = 6;
			Item.holdStyle = ItemHoldStyleID.HoldLamp;
		}

		public override bool CanUseItem(Player player) => false;

		public override void HoldItem(Player player)
		{
            EndgameUtils.DoNutsDamage(player, Item, _maxTargets, _hitFrequency, _dustType, _focusRadius);
        }
    }
}