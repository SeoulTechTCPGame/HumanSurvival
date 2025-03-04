namespace Enums
{
    public enum EStat
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
    public enum EWeaponStat
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
    public enum EAccessoryStat
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
    public enum EWeapon // 10개
    {
        Whip,
        MagicWand,
        Knife,
        Cross,
        KingBible,
        FireWand,
        Garlic,
        Peachone,
        EbonyWings,
        LightningRing,
        WeaponCount
    }
    public enum EAccessory // 15개
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
        Skull
    }
    public enum EPickUpType
    {
        Weapon,
        Accessory,
        Etc
    }
    public enum EEtc
    {
        Money,
        Food
    }
    public enum EButton
    {
        Name,
        Script,
        Property
    }
    public enum ECharacterType
    { 
        Rogue,//Antonio,
        StormMage,//Imelda,      
        Barbarian,//Gennaro,      
        FireMage, //Arca
        Necromancer,//Porta,
        Warlock,//Poe,
        Alchemist,//Clerici
    }
    public enum EBgm
    {
        BGM,
        Stage1
    }
    public enum ECharacterEffect
    {
        Attack,
        Die,
        LevelUp
    }
    public enum EStage
    {
        MadForest,
        InlaidLibrary
    }
    public enum ELangauge
    {
        EN,
        KR
    }
    public enum EPickScript
    {
        Weapon,
        Accessory,
        Etc, // recovery, coin
        WeaponStat
    }
}