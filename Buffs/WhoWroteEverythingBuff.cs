using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace Endgame.Buffs
{
    public class WhoWroteEverythingBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.Common.WhoWroteEverythingBuffName"));
            Description.SetDefault(Language.GetTextValue("Mods.Endgame.Common.WhoWroteEverythingBuffDescription"));

            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.gills = true;
            player.fireWalk = true;
            player.waterWalk = true;
            player.accFlipper = true;
            player.lavaImmune = true;
        }
    }
}
