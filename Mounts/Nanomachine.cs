using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Endgame.Mounts
{
    public class Nanomachine : ModMount
    {
        public override void SetStaticDefaults()
        {
			MountData.spawnDust = DustID.Smoke;
			MountData.buff = ModContent.BuffType<Buffs.Nanomachine>();
			MountData.heightBoost = 20;
			MountData.fallDamage = 0.5f;
			MountData.runSpeed = 11f;
			MountData.dashSpeed = 8f;
			MountData.flightTimeMax = 0;
			MountData.fatigueMax = 0;
			MountData.jumpHeight = 5;
			MountData.acceleration = 0.19f;
			MountData.jumpSpeed = 4f;
			MountData.blockExtraJumps = false;
			MountData.totalFrames = 4;
			MountData.constantJump = true;

			int[] array = new int[MountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 20;
			}

			MountData.playerYOffsets = array;
			MountData.xOffset = 13;
			MountData.bodyFrame = 3;
			MountData.yOffset = -12;
			MountData.playerHeadOffset = 22;
			MountData.standingFrameCount = 4;
			MountData.standingFrameDelay = 12;
			MountData.standingFrameStart = 0;
			MountData.runningFrameCount = 4;
			MountData.runningFrameDelay = 12;
			MountData.runningFrameStart = 0;
			MountData.flyingFrameCount = 0;
			MountData.flyingFrameDelay = 0;
			MountData.flyingFrameStart = 0;
			MountData.inAirFrameCount = 1;
			MountData.inAirFrameDelay = 12;
			MountData.inAirFrameStart = 0;
			MountData.idleFrameCount = 4;
			MountData.idleFrameDelay = 12;
			MountData.idleFrameStart = 0;
			MountData.idleFrameLoop = true;
			MountData.swimFrameCount = MountData.inAirFrameCount;
			MountData.swimFrameDelay = MountData.inAirFrameDelay;
			MountData.swimFrameStart = MountData.inAirFrameStart;

			if (Main.netMode == NetmodeID.Server)
			{
				return;
			}
		}

		public override void UpdateEffects(Player player)
		{
			var balloons = (CarSpecificData)player.mount._mountSpecificData;
			float ballonMovementScale = 0.05f;

			for (int i = 0; i < balloons.count; i++)
			{
				if (Math.Abs(balloons.rotations[i]) > Math.PI / 2)
					ballonMovementScale *= -1;
				balloons.rotations[i] += -player.velocity.X * ballonMovementScale * Main.rand.NextFloat();
				balloons.rotations[i] = balloons.rotations[i].AngleLerp(0, 0.05f);
			}

			if (!(Math.Abs(player.velocity.X) > 4f))
			{
				return;
			}

			Rectangle rect = player.getRect();
			Dust.NewDust(new Vector2(rect.X, rect.Y), rect.Width, rect.Height, DustID.Smoke);
		}

		internal class CarSpecificData
		{
			internal int count;
			internal float[] rotations;
			internal static float[] offsets = new float[] { 0, 14, -14 };

			public CarSpecificData()
			{
				count = 3;
				rotations = new float[count];
			}
		}

		public override void SetMount(Player player, ref bool skipDust)
		{
			player.mount._mountSpecificData = new CarSpecificData();

			for (int i = 0; i < 16; i++)
			{
				Dust.NewDustPerfect(player.Center + new Vector2(80, 0).RotatedBy(i * Math.PI * 2 / 16f), MountData.spawnDust);
			}

			skipDust = true;
		}

		public override bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
		{
			if (drawType == 0)
			{
				var balloons = (CarSpecificData)drawPlayer.mount._mountSpecificData;
				int timer = DateTime.Now.Millisecond % 800 / 200;
				Texture2D balloonTexture = Mod.Assets.Request<Texture2D>("Items/Balloon").Value;
				for (int i = 0; i < balloons.count; i++)
				{
					playerDrawData.Add(new DrawData(balloonTexture, drawPosition + new Vector2((-36 + CarSpecificData.offsets[i]) * drawPlayer.direction, 14), new Rectangle(28, balloonTexture.Height / 4 * ((timer + i) % 4), 28, 42), drawColor, rotation + balloons.rotations[i], new Vector2(14 + drawPlayer.direction * 7, 42), drawScale, spriteEffects ^ SpriteEffects.FlipHorizontally, 0));
				}
			}

			return true;
		}
	}
}
