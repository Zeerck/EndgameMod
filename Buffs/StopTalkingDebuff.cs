using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace Endgame.Buffs
{
    public class StopTalkingDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.Common.StopTalkingDebuffName"));
            // Description.SetDefault(Language.GetTextValue("Mods.Endgame.Common.StopTalkingDebuffDescription"));

            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.silence = true;
            player.sailDash = false;
            player.jumpBoost = false;
            player.lifeMagnet = false;
            player.solarDashing = false;
            player.controlUseItem = false;

            player.wingTime = 0;
            player.lifeSteal = 0;
            player.lifeRegen = 0;
            player.manaRegen = 0;
            player.dashDelay = 1;
            player.potionDelay = 1;
            player.rocketBoots = 0;
            player.moveSpeed = 0.3f;
            player.stepSpeed = 0.3f;
            player.maxRunSpeed = 0.3f;
            player.potionDelayTime = 1;
            player.jumpSpeedBoost = -1;
            player.runAcceleration = 0.3f;
        }
    }
}
