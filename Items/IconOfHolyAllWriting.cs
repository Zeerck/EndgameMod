using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace Endgame.Items
{
    public class IconOfHolyAllWriting : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.IconOfHolyAllWritingName"));
            Tooltip.SetDefault(Language.GetTextValue("Mods.Endgame.IconOfHolyAllWritingDescription"));
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;

            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;

            item.maxStack = 1;
            item.rare = ItemRarityID.Pink;
            item.consumable = true;
        }

        public override bool UseItem(Player player)
        {
            Main.PlaySound((int)SoundType.Custom, (int)player.position.X, (int)player.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Borisich_vi_to_genii"));

            if (Main.netMode != NetmodeID.MultiplayerClient)
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<NPCs.Bosses.BorisichEndgameBoss>());
            else
                NetMessage.SendData(MessageID.SpawnBoss, player.whoAmI, ModContent.NPCType<NPCs.Bosses.BorisichEndgameBoss>());

            EndgameUtils.DisplayLocalizedText("Mods.Endgame.BorisichBossText1");

            return true;
        }
    }
}
