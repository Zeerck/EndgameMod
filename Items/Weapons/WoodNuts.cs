using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Endgame.Items.Weapons
{
    internal class WoodNuts : ModItem
    {
        private const int _maxTargets = 5;
        private const int _hitFrequency = 60;
        private const int _dustType = DustID.WoodFurniture;
        private const float _focusRadius = 120f;

        public override void SetStaticDefaults()
        {
            double delayBetweenHits = Math.Round(_hitFrequency / 60f, 2);
            double focusRadiusInBlocks = Math.Round(_focusRadius / 10f / 2, 2);

            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 6;
            Item.knockBack = 4;
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
                .AddIngredient(ItemID.Wood, 20)
                .AddIngredient(ItemID.DirtBlock, 10)
                .AddIngredient(ItemID.Silk, 2)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
