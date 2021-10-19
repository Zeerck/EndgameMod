using Terraria.ModLoader;

namespace Endgame
{
    public class EndgamePlayer : ModPlayer
    {
        public bool TheOneWhoCould;

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
