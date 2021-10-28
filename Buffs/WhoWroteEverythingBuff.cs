using Terraria.ModLoader;
using Terraria;
using Terraria.Localization;

namespace Endgame.Buffs
{
    public class WhoWroteEverythingBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.WhoWroteEverythingBuffName"));
            Description.SetDefault(Language.GetTextValue("Mods.Endgame.WhoWroteEverythingBuffDescription"));

            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.waterWalk = true;
            player.gills = true;
            player.accFlipper = true;
            player.fireWalk = true;
            player.lavaImmune = true;
        }
    }
}
