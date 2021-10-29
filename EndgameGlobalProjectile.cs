using Terraria;
using Terraria.ID;
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
                texture = Main.projectileTexture[projectile.type];

            int height = texture.Height / Main.projFrames[projectile.type];
            int y = height * projectile.frame;

            float scale = projectile.scale;
            float rotation1 = projectile.rotation;

            Rectangle r = new Rectangle(0, y, texture.Width, height);
            Vector2 origin = r.Size() / 2f;

            SpriteEffects effects1 = SpriteEffects.None;

            if (projectile.spriteDirection == -1)
                effects1 = SpriteEffects.FlipHorizontally;

            if (EndgameConfig.Instance.Afterimages)
            {
                Vector2 vector2 = drawCentered ? projectile.Size / 2f : Vector2.Zero;

                switch (trailingMode)
                {
                    case 0:
                        for (int index = 0; index < projectile.oldPos.Length; ++index)
                        {
                            Vector2 position = projectile.oldPos[index] + vector2 - Main.screenPosition + new Vector2(0.0f, projectile.gfxOffY);
                            Color color = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - index) / projectile.oldPos.Length);
                            Main.spriteBatch.Draw(texture, position, new Rectangle?(r), color, rotation1, origin, scale, effects1, 0.0f);
                        }
                        break;

                    case 1:
                        Color alpha = projectile.GetAlpha(lightColor);

                        int num1 = ProjectileID.Sets.TrailCacheLength[projectile.type];

                        for (int index = 0; index < num1; index += typeOneDistanceMultiplier)
                        {
                            Vector2 position = projectile.oldPos[index] + vector2 - Main.screenPosition + new Vector2(0.0f, projectile.gfxOffY);
                            if (index > 0)
                            {
                                float num2 = (num1 - index);
                                alpha *= num2 / (num1 * 1.5f);
                            }
                            Main.spriteBatch.Draw(texture, position, new Rectangle?(r), alpha, rotation1, origin, scale, effects1, 0.0f);
                        }
                        break;

                    case 2:
                        for (int index = 0; index < projectile.oldPos.Length; ++index)
                        {
                            float rotation2 = projectile.oldRot[index];
                            SpriteEffects effects2 = projectile.oldSpriteDirection[index] == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                            Vector2 position = projectile.oldPos[index] + vector2 - Main.screenPosition + new Vector2(0.0f, projectile.gfxOffY);
                            Color color = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - index) / projectile.oldPos.Length);
                            Main.spriteBatch.Draw(texture, position, new Rectangle?(r), color, rotation2, origin, scale, effects2, 0.0f);
                        }
                        break;
                }
            }

            if (EndgameConfig.Instance.Afterimages && ProjectileID.Sets.TrailCacheLength[projectile.type] > 0)
                return;

            Vector2 vector2_1 = drawCentered ? projectile.Center : projectile.position;

            Main.spriteBatch.Draw(texture, vector2_1 - Main.screenPosition + new Vector2(0.0f, projectile.gfxOffY), new Rectangle?(r), projectile.GetAlpha(lightColor), rotation1, origin, scale, effects1, 0.0f);
        }
    }
}