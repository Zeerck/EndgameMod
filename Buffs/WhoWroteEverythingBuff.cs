using Terraria.ModLoader;
using Terraria;

namespace Endgame.Buffs
{
    public class WhoWroteEverythingBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("The one who wrote everything...");
            Description.SetDefault("You have read all the notes, now you are one with water and are immune to fire.\nYou are the embodiment of water.");

            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = false;
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
