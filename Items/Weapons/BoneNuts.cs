using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

namespace Endgame.Items.Weapons
{
	internal class BoneNuts : ModItem
	{
		private const int _maxTargets = 15;
		private const int _hitFrequency = 45;
		private const int _dustType = DustID.Bone;
		private const float _focusRadius = 190f;

		public override void SetStaticDefaults()
		{
			double delayBetweenHits = Math.Round(_hitFrequency / 60f, 2);
			double focusRadiusInBlocks = Math.Round(_focusRadius / 10f / 2, 2);

			DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.Items.Weapons.BoneNutsName"));
			Tooltip.SetDefault(Language.GetTextValue("Mods.Endgame.Items.Weapons.BoneNutsDescription", _maxTargets, delayBetweenHits, focusRadiusInBlocks));
			SacrificeTotal = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 18;
			Item.knockBack = 5;
			Item.crit = 6;
			Item.holdStyle = ItemHoldStyleID.HoldLamp;
		}

		public override bool CanUseItem(Player player) => false;

		public override void HoldItem(Player player)
		{
			Vector2 mouse = new(Main.screenPosition.X + Main.mouseX, Main.screenPosition.Y + Main.mouseY);
			List<NPC> enemiesList = EndgameUtils.GetTargettableNPCs(player.Center, mouse, _focusRadius, _maxTargets);

			EndgameUtils.DrawDustRadius(player, _focusRadius, _dustType);

			if (enemiesList.Count > 0)
			{
				foreach (NPC enemy in enemiesList)
				{
					if ((int)Main.time % _hitFrequency == 1)
						enemy.StrikeNPC(Item.damage, Item.knockBack, -(int)(enemy.DirectionTo(player.position).X * 1.5f), Main.rand.NextBool(Item.crit, 100));

					if (Main.rand.NextBool(5))
						EndgameUtils.DrawTargettableEffect(enemy, _dustType);
				}
			}
		}
	}
}
