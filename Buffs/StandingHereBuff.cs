using Terraria;
using Terraria.ModLoader;

namespace Endgame.Buffs
{
    public class StandingHereBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            //Main.buffNoTimeDisplay[Type] = true;

            //EndgamePlayer.PlayerStanding = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
             player.statLife = 10;
             player.lifeRegen = 0;
             player.lifeRegenCount = 0;
             player.lifeRegenTime = 0;
             player.potionDelay = 60;
             player.maxFallSpeed = 0f;
             player.jumpSpeedBoost = 0;
             player.noKnockback = true;
             player.accRunSpeed = 0;
             player.wallSpeed = 0;
             player.moveSpeed = 0;
             player.jumpBoost = false;
             player.jumpSpeedBoost = 0;
             player.jump = 0;
             player.wingTime = 0;
             player.wingTimeMax = 0;
             player.wingAccRunSpeed = 0;
             player.controlJump = false;
             player.controlHook = false;
             player.controlQuickHeal = false;
             player.controlRight = false;
             player.controlLeft = false;
        }
    }
}
