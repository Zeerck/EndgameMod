using Terraria.ModLoader;

namespace Endgame
{
    public class EndgamePlayer : ModPlayer
    {
        //public static bool TheOneWhoCould;

        public override void ResetEffects()
        {
            if (EndgameWorld.conspectusReturned)
                player.AddBuff(ModContent.BuffType<Buffs.WhoWroteEverythingBuff>(), 10);
        }
    }
}
