using Terraria.ModLoader;

namespace Endgame
{
    public class EndgamePlayer : ModPlayer
    {
        //public static bool TheOneWhoCould;
        public static bool PlayerStanding;

        public override void ResetEffects()
        {
            if (EndgameWorld.conspektyReturned)
                Player.AddBuff(ModContent.BuffType<Buffs.WhoWroteEverythingBuff>(), 1000);
        }
    }
}
