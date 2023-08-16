using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace Endgame.Items
{
    public class IconOfHolyAllWriting : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Pink;
            Item.consumable = false;
        }

        public override bool CanUseItem(Player player)
        {
            SoundEngine.PlaySound(new SoundStyle("Endgame/Sounds/Custom/Borisich_vi_to_genii"), player.position);

            if (Main.netMode != NetmodeID.MultiplayerClient)
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<NPCs.Bosses.BorisichEndgameBoss>());
            else
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, player.whoAmI, ModContent.NPCType<NPCs.Bosses.BorisichEndgameBoss>());

            EndgameUtils.DisplayLocalizedText("Mods.Endgame.Common.BorisichBossText1");

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe(10)
                .AddIngredient(ModContent.ItemType<ProgramerSoul>(), 42000)
                .AddIngredient(ItemID.SoulofFlight, 42)
                .AddIngredient(ItemID.SoulofFright, 42)
                .AddIngredient(ItemID.SoulofLight, 42)
                .AddIngredient(ItemID.SoulofMight, 42)
                .AddIngredient(ItemID.SoulofNight, 42)
                .AddIngredient(ItemID.SoulofSight, 42)
                .AddIngredient(ItemID.PapyrusScarab, 1)
                .AddIngredient(ItemID.BlackInk, 3)
                .AddIngredient(ItemID.Feather, 1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
