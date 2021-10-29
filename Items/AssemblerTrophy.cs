using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace Endgame.Items
{
    public class AssemblerTrophy : ModItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault(Language.GetTextValue("Mods.Endgame.AssemblerVirtualMachineName"));

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
       
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.Stabbing;
    
            item.maxStack = 99;
            item.consumable = true;
            item.value = 50000;
            item.rare = ItemRarityID.Blue;
            item.createTile = ModContent.TileType<Tiles.Trophies.BossTrophies>();
            item.placeStyle = 0;
        }
    }
}
