﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace Endgame.Projectiles.NPCs
{
    public class NPCsShoot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.Common.NpcShootName"));

            Main.projFrames[Projectile.type] = 3;

            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 62;
            Projectile.height = 60;

            AIType = ProjectileID.Bullet;

            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 500;
            Projectile.tileCollide = true;
            Projectile.scale = 1.2f;
        }

        public override void AI()
        {
            ++Projectile.frameCounter;

            if (Projectile.frameCounter > 4)
            {
                ++Projectile.frame;
                Projectile.frameCounter = 0;
            }

            if (Projectile.frame > 2)
                Projectile.frame = 0;

            if (Projectile.velocity.X < 0.0)
            {
                Projectile.spriteDirection = -1;
                Projectile.rotation = (float)Math.Atan2(-Projectile.velocity.Y, -Projectile.velocity.X);
            }
            else
            {
                Projectile.spriteDirection = 1;
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
            }

            Lighting.AddLight(Projectile.Center, 0.3f, 0.3f, 0.0f);

            ++Projectile.ai[0];

            if (Projectile.ai[0] <= 9.0 || Projectile.alpha <= 0)
                return;

            Projectile.alpha -= 5;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            EndgameGlobalProjectile.DrawCenteredAndAfterimage(Projectile, lightColor, ProjectileID.Sets.TrailingMode[Projectile.type]);
            return false;
        }

        public override Nullable<bool> CanDamage()/* tModPorter Suggestion: Return null instead of true */ => Projectile.timeLeft >= 85;

        public override Color? GetAlpha(Color lightColor)
        {
            byte num1 = (byte)(Projectile.timeLeft * 3);
            byte num2 = (byte)((double)Projectile.alpha * (num1 / byte.MaxValue));

            if (Projectile.timeLeft >= 85)
                return new Color?(new Color(byte.MaxValue, byte.MaxValue, byte.MaxValue, Projectile.alpha));

            return new Color?(new Color(num1, num1, num1, num2));
        }
    }
}
