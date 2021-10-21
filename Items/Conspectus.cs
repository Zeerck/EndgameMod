using Terraria.ID;
using Endgame.Buffs;
using Terraria.ModLoader;
using Terraria.Localization;

namespace Endgame.Items
{
    public class Conspectus : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.ConspectusName"));
            Tooltip.SetDefault(Language.GetTextValue("Mods.Endgame.ConspectusDescription"));
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 36;

            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = ItemUseStyleID.EatingUsing;

            item.maxStack = 1;
            item.buffTime = 245000;
            item.rare = ItemRarityID.Expert;
            item.buffType = ModContent.BuffType<WhoWroteEverythingBuff>();

            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Borisich_davaite_zapishem");
        }
    }
}
