using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

namespace Endgame.Items.Weapons
{
	internal class PlatinumNuts : ModItem
	{
		private const int _maxTargets = 14;
		private const int _hitFrequency = 40;
		private const int _dustType = DustID.PlatinumCoin;
		private const float _focusRadius = 180f;

		public override void SetStaticDefaults()
		{
			double delayBetweenHits = Math.Round(_hitFrequency / 60f, 2);
			double focusRadiusInBlocks = Math.Round(_focusRadius / 10f / 2, 2);

			DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.Items.Weapons.PlatinumNutsName"));
			Tooltip.SetDefault(Language.GetTextValue("Mods.Endgame.Items.Weapons.PlatinumNutsDescription", _maxTargets, delayBetweenHits, focusRadiusInBlocks));
			SacrificeTotal = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 17;
			Item.knockBack = 4;
			Item.crit = 4;
			Item.holdStyle = ItemHoldStyleID.HoldLamp;
		}

		public override bool CanUseItem(Player player) => false;

		public override void HoldItem(Player player)
		{
			Vector2 mouse = new(Main.screenPosition.X + Main.mouseX, Main.screenPosition.Y + Main.mouseY);
			List<NPC> enemiesList = EndgameUtils.GetTargettableNPCs(player.Center, mouse, _focusRadius, _maxTargets);

			EndgameUtils.DrawDustRadius(player, _focusRadius, _dustType);

			if(enemiesList.Count > 0)
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

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.PlatinumBar, 12)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}