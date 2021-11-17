using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Endgame.Buffs
{
    public class Nanomachine : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.NanomachineName"));
            Description.SetDefault(Language.GetTextValue("Mods.Endgame.NanomachineMountDescription"));
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<Mounts.Nanomachine>(), player);
            player.buffTime[buffIndex] = 10;
        }
    }
}
