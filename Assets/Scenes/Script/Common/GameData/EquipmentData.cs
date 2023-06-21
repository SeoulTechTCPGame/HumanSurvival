using System;
using System.Collections.Generic;

public class EquipmentData
{
    public static List<List<Tuple<int, float>>>[] WeaponUpgrade;
    public static float[,] DefaultWeaponStats;
    public static List<List<Tuple<int, float>>>[] AccessoryUpgrade;
    public static int[] AccessoriesMaxLevel;
    public static int[] EvoWeaponNeedAccIndex;   // Whip,MagicWand,Knife,Cross,KingBible,FireWand,Garlic,Peachone,EbonyWings,LightningRing,SantaWater
    public static int[] EvoWeaponNeedWeaponIndex;   // Whip,MagicWand,Knife,Cross,KingBible,FireWand,Garlic,Peachone,EbonyWings,LightningRing,SantaWater
    public static int[] EvoAccNeedWeaponIndex;   // Spinach,Armor,HollowHeart,Pummarola,EmptyTome,Candelabrador,Bracer,Spellbinder,Duplicator,Wings,Attractorb,Clover,Crown,StoneMask,Skull
    
    static EquipmentData()
    {
        LevelOneWeaponPreprocessing();
        WeaponUpgradePreprocessing();
        AccessoryUpgradePreprocessing();
        EvolutionPairData();
    }

    public static void EvolutionPairData()
    {
        // 짝이 없는 무기, 악세서리의 경우는 -1로 초기화
        EvoWeaponNeedAccIndex       = new int[Constants.MAX_WEAPON_NUMBER] { 2, 4, 6, 11, 7, 0, 3, -1, -1, 8 };
        EvoWeaponNeedWeaponIndex    = new int[Constants.MAX_WEAPON_NUMBER] { -1, -1, -1, -1, -1, -1, -1, 10, 9, -1 };
        EvoAccNeedWeaponIndex       = new int[Constants.MAX_ACCESSORY_NUMBER] { 5, -1, 0, 6, 1, -1, 2, 4, 9, -1, -1, 3, -1, -1, -1 };
    }
    public static void LevelOneWeaponPreprocessing()
    {
        DefaultWeaponStats = new float[Constants.MAX_WEAPON_NUMBER, 9]
        {
       //   Might, Cooldown,  ProjectileSpeed,  Duration,     Amount,       AmountLimit,        Piercing,       Area, MaxLevel
            { 10,   1.35f,      Constants.X,    Constants.X,    1,              30,             Constants.INF,  1,      8   },  // Whip
            { 10,   1.20f,      1,              Constants.X,    1,              60,             1,              1,      8   },  // MagicWand
            { 6.5f, 1.00f,      1,              Constants.X,    1,              70,             1,              1,      8   },  // Knife
            { 5,    2.00f,      1,              Constants.X,    1,              30,             Constants.INF,  1,      8   },  // Cross
            { 10,   3.00f,      1,              3,              1,              50,             Constants.INF,  1,      8   },  // KingBible
            { 20,   3.00f,      1,              Constants.X,    3,              30,             1,              1,      8   },  // FireWand
            { 5,    1.00f,      Constants.X,    Constants.X,    Constants.X,    Constants.X,    Constants.INF,  1,      8   },  // Garlic
            { 10,   12.00f,     0.80f,          4,              4,              60,             Constants.INF,  1,      8   },  // Peachone
            { 10,   12.00f,     0.80f,          4,              4,              60,             Constants.INF,  1,      8   },  // EbonyWings
            { 15,   4.50f,      Constants.X,    Constants.X,    2,              50,             Constants.INF,  1,      8   }   // LightningRing
        };
    }
    public static void WeaponUpgradePreprocessing()
    {
        WeaponUpgrade = new List<List<Tuple<int, float>>>[Constants.MAX_WEAPON_NUMBER];
        // Whip
        WeaponUpgrade[0] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 5)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 5),
            new Tuple<int, float>((int)Enums.EWeaponStat.Area, 0.10f)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 5)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 5),
            new Tuple<int, float>((int)Enums.EWeaponStat.Area, 0.10f)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 5)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 5)
        });

        // MagicWand
        WeaponUpgrade[1] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Cooldown, -0.2f)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 10)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Piercing, 1)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 10)
        });

        // Knife
        WeaponUpgrade[2] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 5)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Piercing, 1)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 5)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Piercing, 1)
        });

        // Cross
        WeaponUpgrade[3] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 10)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.ProjectileSpeed, 0.25f),
            new Tuple<int, float>((int)Enums.EWeaponStat.Area, 0.10f)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 10)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.ProjectileSpeed, 0.25f),
            new Tuple<int, float>((int)Enums.EWeaponStat.Area, 0.10f)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 10)
        });

        //KingBible
        WeaponUpgrade[4] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.ProjectileSpeed, 0.30f),
            new Tuple<int, float>((int)Enums.EWeaponStat.Area, 0.25f)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Duration, 0.5f),
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 10)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.ProjectileSpeed, 0.30f),
            new Tuple<int, float>((int)Enums.EWeaponStat.Area, 0.25f)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Duration, 0.5f),
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 10)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1)
        });

        // FireWand
        WeaponUpgrade[5] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 10)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 10),
            new Tuple<int, float>((int)Enums.EWeaponStat.ProjectileSpeed, 0.20f)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 10)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 10),
            new Tuple<int, float>((int)Enums.EWeaponStat.ProjectileSpeed, 0.20f)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 10)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 10),
            new Tuple<int, float>((int)Enums.EWeaponStat.ProjectileSpeed, 0.20f)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 10)
        });

        // Garlic
        WeaponUpgrade[6] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Area, 0.40f),
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 2)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Cooldown, -0.1f),
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 1)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Area, 0.20f),
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 1)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Cooldown, -0.1f),
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 2)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Area, 0.20f),
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 1)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Cooldown, -0.1f),
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 1)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Area, 0.20f),
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 2)
        });

        // Peachone
        WeaponUpgrade[7] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.EWeaponStat.Area, 0.40f)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 10)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.EWeaponStat.Cooldown, -0.3f)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.EWeaponStat.Area, 0.40f)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 10)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.EWeaponStat.Cooldown, -0.3f)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.EWeaponStat.Area, 0.40f)
        });

        // EbonyWings
        WeaponUpgrade[8] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.EWeaponStat.Area, 0.40f)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 10)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.EWeaponStat.Cooldown, -0.3f)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.EWeaponStat.Area, 0.40f)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 10)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.EWeaponStat.Cooldown, -0.3f)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.EWeaponStat.Area, 0.40f)
        });

        // LightningRing
        WeaponUpgrade[9] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Area, 1),
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 10)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Area, 1),
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 20)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Area, 1),
            new Tuple<int, float>((int)Enums.EWeaponStat.Might, 20)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EWeaponStat.Amount, 1)
        });
    }
    private static void AccessoryUpgradePreprocessing()
    {
        AccessoryUpgrade = new List<List<Tuple<int, float>>>[Constants.MAX_ACCESSORY_NUMBER];
        // 앞의 한 개(index상 0에 해당)는 더미 데이터
        // Spinach
        AccessoryUpgrade[0] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[0].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[0].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MightPer, 0.1f)
        });
        AccessoryUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MightPer, 0.1f)
        });
        AccessoryUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MightPer, 0.1f)
        });
        AccessoryUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MightPer, 0.1f)
        });
        AccessoryUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MightPer, 0.1f)
        });

        // Armor
        AccessoryUpgrade[1] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[1].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.Armor, 1),
            new Tuple<int, float>((int)Enums.EAccessoryStat.ReflectionPer, 0.1f)
        });
        AccessoryUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.Armor, 1),
            new Tuple<int, float>((int)Enums.EAccessoryStat.ReflectionPer, 0.1f)
        });
        AccessoryUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.Armor, 1),
            new Tuple<int, float>((int)Enums.EAccessoryStat.ReflectionPer, 0.1f)
        });
        AccessoryUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.Armor, 1),
            new Tuple<int, float>((int)Enums.EAccessoryStat.ReflectionPer, 0.1f)
        });
        AccessoryUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.Armor, 1),
            new Tuple<int, float>((int)Enums.EAccessoryStat.ReflectionPer, 0.1f)
        });

        // HollowHeart
        AccessoryUpgrade[2] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[2].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MaxHealthPer, 0.2f)
        });
        AccessoryUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MaxHealthPer, 0.2f)
        });
        AccessoryUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MaxHealthPer, 0.2f)
        });
        AccessoryUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MaxHealthPer, 0.2f)
        });
        AccessoryUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MaxHealthPer, 0.2f)
        });

        // Pummarola
        AccessoryUpgrade[3] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[3].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.Recovery, 0.1f)
        });
        AccessoryUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.Recovery, 0.1f)
        });
        AccessoryUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.Recovery, 0.1f)
        });
        AccessoryUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.Recovery, 0.1f)
        });
        AccessoryUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.Recovery, 0.1f)
        });

        // EmptyTome
        AccessoryUpgrade[4] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[4].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.CooldownPer, -0.8f)
        });
        AccessoryUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.CooldownPer, -0.8f)
        });
        AccessoryUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.CooldownPer, -0.8f)
        });
        AccessoryUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.CooldownPer, -0.8f)
        });
        AccessoryUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.CooldownPer, -0.8f)
        });

        // Candelabrador
        AccessoryUpgrade[5] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[5].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.AreaPer, 0.10f)
        });
        AccessoryUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.AreaPer, 0.10f)
        });
        AccessoryUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.AreaPer, 0.10f)
        });
        AccessoryUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.AreaPer, 0.10f)
        });
        AccessoryUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.AreaPer, 0.10f)
        });

        // Bracer
        AccessoryUpgrade[6] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[6].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.ProjectileSpeedPer, 0.1f)
        });
        AccessoryUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.ProjectileSpeedPer, 0.1f)
        });
        AccessoryUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.ProjectileSpeedPer, 0.1f)
        });
        AccessoryUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.ProjectileSpeedPer, 0.1f)
        });
        AccessoryUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.ProjectileSpeedPer, 0.1f)
        });

        // Spellbinder
        AccessoryUpgrade[7] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[7].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.DurationPer, 0.1f)
        });
        AccessoryUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.DurationPer, 0.1f)
        });
        AccessoryUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.DurationPer, 0.1f)
        });
        AccessoryUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.DurationPer, 0.1f)
        });
        AccessoryUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.DurationPer, 0.1f)
        });

        // Duplicator
        AccessoryUpgrade[8] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[8].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.Amount, 1)
        });
        AccessoryUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.Amount, 1)
        });

        // Wings
        AccessoryUpgrade[9] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[9].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MoveSpeedPer, 0.1f)
        });
        AccessoryUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MoveSpeedPer, 0.1f)
        });
        AccessoryUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MoveSpeedPer, 0.1f)
        });
        AccessoryUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MoveSpeedPer, 0.1f)
        });
        AccessoryUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MoveSpeedPer, 0.1f)
        });

        // Attractorb
        AccessoryUpgrade[10] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[10].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MagnetPer, 0.33f)
        });
        AccessoryUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MagnetPer, 0.33f)
        });
        AccessoryUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MagnetPer, 0.25f)
        });
        AccessoryUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MagnetPer, 0.20f)
        });
        AccessoryUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MagnetPer, 0.33f)
        });

        // Clover
        AccessoryUpgrade[11] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[11].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.LuckPer, 0.1f)
        });
        AccessoryUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.LuckPer, 0.1f)
        });
        AccessoryUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.LuckPer, 0.1f)
        });
        AccessoryUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.LuckPer, 0.1f)
        });
        AccessoryUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.LuckPer, 0.1f)
        });

        // Crown
        AccessoryUpgrade[12] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[12].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.GrowthPer, 0.08f)
        });
        AccessoryUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.GrowthPer, 0.08f)
        });
        AccessoryUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.GrowthPer, 0.08f)
        });
        AccessoryUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.GrowthPer, 0.08f)
        });
        AccessoryUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.GrowthPer, 0.08f)
        });

        // StoneMask
        AccessoryUpgrade[13] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[13].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[13].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.GreedPer, 0.1f)
        });
        AccessoryUpgrade[13].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.GreedPer, 0.1f)
        });
        AccessoryUpgrade[13].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.GreedPer, 0.1f)
        });
        AccessoryUpgrade[13].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.GreedPer, 0.1f)
        });
        AccessoryUpgrade[13].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.GreedPer, 0.1f)
        });

        // Skull
        AccessoryUpgrade[14] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[14].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[14].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.CursePer, 0.1f)
        });
        AccessoryUpgrade[14].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.CursePer, 0.1f)
        });
        AccessoryUpgrade[14].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.CursePer, 0.1f)
        });
        AccessoryUpgrade[14].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.CursePer, 0.1f)
        });
        AccessoryUpgrade[14].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.EAccessoryStat.CursePer, 0.1f)
        });

        AccessoriesMaxLevel = new int[15];
        for (int i = 0; i < AccessoriesMaxLevel.Length; i++)
        {
            AccessoriesMaxLevel[i] = AccessoryUpgrade[i].Count - 1;
        }
    }
}