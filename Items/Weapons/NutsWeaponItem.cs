using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Humanizer;
using Endgame.DamageClasses;

namespace Endgame.Items.Weapons
{
    public abstract class NutsWeaponItem : ModItem
    {
        public int MaxTargets { get; set; }
        public int HitFrequency { get; set; }
        public int DustType { get; set; }
        public float FocusRadius { get; set; }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            double delayBetweenHits = Math.Round(HitFrequency / 60f, 2);
            double focusRadiusInBlocks = Math.Round(FocusRadius / 10f / 2, 2);

            int index = tooltips.FindIndex(line => line.Mod == "Terraria" && line.Name == "Knockback");

            if (index < 0)
                index = tooltips.Count - 1;

            tooltips.Insert(index + 1, new TooltipLine(Mod, "Focus Radius", Language.GetTextValue("Mods.Endgame.NutsWeaponsTooltip.Radius").FormatWith(focusRadiusInBlocks)));
            tooltips.Insert(index + 1, new TooltipLine(Mod, "Hit Friquency", Language.GetTextValue("Mods.Endgame.NutsWeaponsTooltip.HitDelay").FormatWith(delayBetweenHits)));
            tooltips.Insert(index + 1, new TooltipLine(Mod, "Maximum targets", Language.GetTextValue("Mods.Endgame.NutsWeaponsTooltip.MaximumTargets").FormatWith(MaxTargets)));
        }

        public override void SetDefaults()
        {
            Item.DamageType = ModContent.GetInstance<NutsDamageClass>();
            Item.holdStyle = ItemHoldStyleID.HoldLamp;
        }

        public override bool CanUseItem(Player player) => false;

        public override void HoldItem(Player player)
        {
            EndgameUtils.DoNutsDamage(player, Item, MaxTargets, HitFrequency, DustType, FocusRadius, ModContent.GetInstance<NutsDamageClass>());
        }
    }
}
