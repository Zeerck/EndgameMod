using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace Endgame.Items
{
    public class ProgramerSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));

            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item refItem = new();

            refItem.SetDefaults(ItemID.SoulofSight);
            Item.width = refItem.width;
            Item.height = refItem.height;
            Item.maxStack = 999999;
            Item.value = 1000;
            Item.rare = ItemRarityID.Lime;
        }

        public override void GrabRange(Player player, ref int grabRange)
        {
            grabRange *= 4;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.position, Color.LightBlue.ToVector3() * 0.55f * Main.essScale);
        }
    }
}
