using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Endgame
{
    public class EndgameConfig : ModConfig
    {
        public static EndgameConfig Instance;
        public override ConfigScope Mode => (ConfigScope)1;

        [Header("Graphics Changes")]
        [Label("Afterimages")]
        [BackgroundColor(192, 54, 64, 192)]
        [DefaultValue(true)]
        [Tooltip("Enables rendering afterimages for Endgame NPCs, projectiles, etc.")]
        public bool Afterimages { get; set; }
    }
}