using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Endgame.Items.Weapons
{
    //TODO: Для всех Nuts переработать статы, в основном _maxTargets
    //TODO: Исправить радиус, сейчас цифра врёт примерно на +2 блока в описании и +1 на деле.
    //TODO: Проверить рецепты и добавить если их нету
    //TODO: Проверить DustType для hellstone nuts, эффект пыли неприменим к врагам
    //TODO: Рассчитать DPS и забалансить для всех Nuts
    //TODO: Доработать все значения NutsDamageClass
    //TODO: Возможность в упор наносить только криты
    //TODO: Текстурки и иконки для HolyOfAllWriting и Nanomachine
    //TODO: Проверить имена NPC, вместо поиска должен быть "NPC.GivenName"
    //TODO: Sudarin должен говорить только о конспектах, когда игрок имеет их в инвентаре
    //TODO: Прописать настроение для NPC
    //TODO: Добавить описание summary ко всем классам
    //TODO: Пересмотреть методы DropItem's
    //TODO: Текстурка для Programmer soul
    //TODO: Добавить, нарисовать или изменить большинство текстур для Projectile, nanomachine, mount
    //BUG: Борисыч не уходит после смерти игрока
    //TODO: Джунглевые ЯЙЦА

    internal class BoneNuts : NutsWeaponItem
    {
        private const int _maxTargets = 15;
        private const int _hitFrequency = 45;
        private const int _dustType = DustID.Bone;
        private const float _focusRadius = 190f;

        BoneNuts()
        {
            MaxTargets = _maxTargets;
            HitFrequency = _hitFrequency;
            DustType = _dustType;
            FocusRadius = _focusRadius;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.damage = 18;
            Item.knockBack = 5;
            Item.crit = 6;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Bone, 22)
                .AddIngredient(ModContent.ItemType<WoodNuts>())
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
