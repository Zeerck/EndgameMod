using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Endgame.Projectiles.Bosses
{
    public class BorisichEndgameProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.BorisichShootName"));

            Main.projFrames[projectile.type] = 3;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;

            projectile.friendly = false; //I'm trying to fix this in order for damage NPCs
            projectile.hostile = true;

            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.alpha = byte.MaxValue;
            projectile.penetrate = -1;
            projectile.timeLeft = 240;
        }

        public override void AI()
        {
            ++projectile.frameCounter;

            if (projectile.frameCounter > 4)
            {
                ++projectile.frame;
                projectile.frameCounter = 0;
            }

            if (projectile.frame > 2)
                projectile.frame = 0;

            if (projectile.velocity.X < 0.0)
            {
                projectile.spriteDirection = -1;
                projectile.rotation = (float)Math.Atan2(-projectile.velocity.Y, -projectile.velocity.X);
            }
            else
            {
                projectile.spriteDirection = 1;
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X);
            }

            Lighting.AddLight(projectile.Center, 0.3f, 0.3f, 0.0f);

            ++projectile.ai[0];

            if (projectile.ai[0] <= 9.0 || projectile.alpha <= 0)
                return;

            projectile.alpha -= 5;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            EndgameGlobalProjectile.DrawCenteredAndAfterimage(projectile, lightColor, ProjectileID.Sets.TrailingMode[projectile.type]);
            return false;
        }

        public override bool CanDamage() => projectile.timeLeft >= 85;

        public override Color? GetAlpha(Color lightColor)
        {
            byte num1 = (byte)(projectile.timeLeft * 3);
            byte num2 = (byte)((double)projectile.alpha * (num1 / byte.MaxValue));

            if (projectile.timeLeft >= 85)
                return new Color?(new Color(byte.MaxValue, byte.MaxValue, byte.MaxValue, projectile.alpha));

            return new Color?(new Color(num1, num1, num1, num2));
        }

        //public override void OnHitPlayer(Player target, int damage, bool crit) => target.AddBuff(ModContent.BuffType<BorisichStopTalking>(), 120, true);
    }
}
