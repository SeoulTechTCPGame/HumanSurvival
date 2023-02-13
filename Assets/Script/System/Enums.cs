using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Enums
{
    public enum Stat
    {
        Might,
        Armor,
        MaxHealth,
        Recovery,
        Cooldown,
        Area,
        ProjectileSpeed,
        Duration,
        Amount,
        MoveSpeed,
        Magnet,
        Luck,
        Growth,
        Greed,
        Curse,
        Revival,
        Reroll,
        Skip,
        Banish,
        Ommi,
        Reflection,
    }

    public enum WeaponStat
    {
        Might,
        Cooldown,
        ProjectileSpeed,
        Duration,
        Amount,
        AmountLimit,
        Piercing,
        Area,
        MaxLevel
    }

    public enum AccessoryStat
    {
        MightPer,
        Armor,
        MaxHealthPer,
        Recovery,
        CooldownPer,
        AreaPer,
        ProjectileSpeedPer,
        DurationPer,
        Amount,
        MoveSpeedPer,
        MagnetPer,
        LuckPer,
        GrowthPer,
        GreedPer,
        CursePer,
        Revival,
        Reroll,
        Skip,
        Banish,
        Ommi,
        ReflectionPer,
    }

    public enum Weapon
    {
        Whip,
        MagicWand,
        Knife,
        Axe,
        Cross,
        KingBible,
        FireWand,
        Garlic,
        SantaWater,
        Peachone,
        EbonyWings,
        Runetracer,
        LightningRing // 13개
    }

    public enum Accessory
    {
        Spinach,
        Armor,
        HollowHeart,
        Pummarola,
        EmptyTome,
        Candelabrador,
        Bracer,
        Spellbinder,
        Duplicator,
        Wings,
        Attractorb,
        Clover,
        Crown,
        StoneMask,
        Tiragisu,
        Skull,
        SilverRing,
        GoldRing,
        MetaglioLeft,
        MetaglioRight,
        TorronaBox  // 21개
    }
    public enum PickUpType
    {
        Weapon,
        Accessory,
        Etc
    }
    public enum Etc
    {
        Money,
        Food
    }
    public enum Button
    {
        Name,
        Script,
        Property
    }
}
