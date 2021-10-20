using Terraria.ModLoader;

namespace Endgame
{
    public class EndgamePlayer : ModPlayer
    {
        public static bool TheOneWhoCould;
        public static bool ConspectusReader;

        public override void ResetEffects()
        {
            TheOneWhoCould = false;
        }

        public override void UpdateDead()
        {
            TheOneWhoCould = false;
        }
    }
}
