using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Endgame
{
    public class EndgameGlobalProjectile : GlobalProjectile
    {
        public static void DrawCenteredAndAfterimage(Projectile projectile, Color lightColor, int trailingMode, int typeOneDistanceMultiplier = 1, Texture2D texture = null, bool drawCentered = true)
        {
            if (texture == null)
                texture = Terraria.GameContent.TextureAssets.Projectile[projectile.type].Value;

            int height = texture.Height / Main.projFrames[projectile.type];
            int y = height * projectile.frame;

            float scale = projectile.scale;
            float rotation1 = projectile.rotation;

            Rectangle r = new Rectangle(0, y, texture.Width, height);
            Vector2 origin = r.Size() / 2f;

            SpriteEffects effects1 = SpriteEffects.None;

            if (projectile.spriteDirection == -1)
                effects1 = SpriteEffects.FlipHorizontally;

            Vector2 vector2_1 = drawCentered ? projectile.Center : projectile.position;

            Main.spriteBatch.Draw(texture, vector2_1 - Main.screenPosition + new Vector2(0.0f, projectile.gfxOffY), new Rectangle?(r), projectile.GetAlpha(lightColor), rotation1, origin, scale, effects1, 0.0f);
        }
    }
}