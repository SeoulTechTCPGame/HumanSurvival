using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentData
{
    public static List<List<Tuple<int, float>>>[] WeaponUpgrade;
    public static float[,] defaultWeaponStats;
    public static List<List<Tuple<int, float>>>[] AccessoryUpgrade;
    public static int[] AccessoriesMaxLevel;
    static EquipmentData()
    {
        levelOneWeaponPreprocessing();
        weaponUpgradePreprocessing();
        AccessoryUpgradePreprocessing();
    }

    public static void levelOneWeaponPreprocessing()
    {
        defaultWeaponStats = new float[13, 9] // Enums.WeaponStat의 순서대로
        {
            { 10, 1.35f, Constants.X, Constants.X, 1, 30, Constants.INF, 1, 8 },
            { 10, 1.20f, 1, Constants.X, 1, 60, 1, 1, 8 },
            { 6.5f, 1.00f, 1, Constants.X, 1, 70, 1, 1, 8 },
            { 20, 4.00f, 1, Constants.X, 1, 70, 3, 1, 8 },
            { 5, 2.00f, 1, Constants.X, 1, 30, Constants.INF, 1, 8 },
            { 10, 3.00f, 1, 3, 1, 50, Constants.INF, 1, 8 },
            { 20, 3.00f, 1, Constants.X, 3, 30, 1, 1, 8 },
            { 5, 1.00f, Constants.X, Constants.X, Constants.X, Constants.X, Constants.INF, 1, 8 },
            { 10, 4.50f, Constants.X, 2, 1, 20, Constants.INF, 1, 8 },
            { 10, 1.00f, 0.80f, 4, 4, 60, Constants.INF, 1, 8 },
            { 10, 1.00f, 0.80f, 4, 4, 60, Constants.INF, 1, 8 },
            { 10, 3.00f, 1.00f, 2.25f, 1, 25, Constants.INF, 1, 8 },
            { 15, 4.50f, Constants.X, Constants.X, 2, 50, Constants.INF, 1, 8 }
        };
    }

    public static void weaponUpgradePreprocessing()
    {
        WeaponUpgrade = new List<List<Tuple<int, float>>>[13];
        // Whip
        WeaponUpgrade[0] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.10f)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.10f)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5)
        });

        // MagicWand
        WeaponUpgrade[1] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Cooldown, -0.2f)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Piercing, 1)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });

        // Knife
        WeaponUpgrade[2] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Piercing, 1)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Piercing, 1)
        });

        // Axe
        WeaponUpgrade[3] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 20)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Piercing, 2)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 20)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Piercing, 2)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 20)
        });

        // Cross
        WeaponUpgrade[4] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.ProjectileSpeed, 0.25f),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.10f)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.ProjectileSpeed, 0.25f),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.10f)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });

        //KingBible
        WeaponUpgrade[5] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.ProjectileSpeed, 0.30f),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.25f)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Duration, 0.5f),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.ProjectileSpeed, 0.30f),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.25f)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Duration, 0.5f),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });

        // FireWand
        WeaponUpgrade[6] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10),
            new Tuple<int, float>((int)Enums.WeaponStat.ProjectileSpeed, 0.20f)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10),
            new Tuple<int, float>((int)Enums.WeaponStat.ProjectileSpeed, 0.20f)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10),
            new Tuple<int, float>((int)Enums.WeaponStat.ProjectileSpeed, 0.20f)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });

        // Garlic
        WeaponUpgrade[7] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.40f),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 2)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Cooldown, -0.1f),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 1)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.20f),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 1)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Cooldown, -0.1f),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 2)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.20f),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 1)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Cooldown, -0.1f),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 1)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.20f),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 2)
        });

        // SantaWater
        WeaponUpgrade[8] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.20f)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10),
            new Tuple<int, float>((int)Enums.WeaponStat.Duration, 0.5f)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.20f)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10),
            new Tuple<int, float>((int)Enums.WeaponStat.Duration, 0.3f)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.20f)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5),
            new Tuple<int, float>((int)Enums.WeaponStat.Duration, 0.3f)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.20f)
        });

        // Peachone
        WeaponUpgrade[9] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.40f)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Cooldown, -0.3f)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.40f)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Cooldown, -0.3f)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.40f)
        });

        // EbonyWings
        WeaponUpgrade[10] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.40f)
        });
        WeaponUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Cooldown, -0.3f)
        });
        WeaponUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.40f)
        });
        WeaponUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Cooldown, -0.3f)
        });
        WeaponUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.40f)
        });

        // Runetracer
        WeaponUpgrade[11] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5),
            new Tuple<int, float>((int)Enums.WeaponStat.ProjectileSpeed, 0.20f)
        });
        WeaponUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5),
            new Tuple<int, float>((int)Enums.WeaponStat.Duration, 0.3f)
        });
        WeaponUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5),
            new Tuple<int, float>((int)Enums.WeaponStat.ProjectileSpeed, 0.20f)
        });
        WeaponUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5),
            new Tuple<int, float>((int)Enums.WeaponStat.Duration, 0.3f)
        });
        WeaponUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Duration, 0.5f)
        });

        // LightningRing
        WeaponUpgrade[12] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 0.0f)
        });
        WeaponUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 20)
        });
        WeaponUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 20)
        });
        WeaponUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
    }
    private static void AccessoryUpgradePreprocessing()
    {
        AccessoryUpgrade = new List<List<Tuple<int, float>>>[21];
        // 앞의 한 개(index상 0에 해당)는 더미 데이터
        // Spinach
        AccessoryUpgrade[0] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[0].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[0].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.1f)
        });
        AccessoryUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.1f)
        });
        AccessoryUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.1f)
        });
        AccessoryUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.1f)
        });
        AccessoryUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.1f)
        });

        // Armor
        AccessoryUpgrade[1] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[1].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Armor, 1),
            new Tuple<int, float>((int)Enums.AccessoryStat.ReflectionPer, 0.1f)
        });
        AccessoryUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Armor, 1),
            new Tuple<int, float>((int)Enums.AccessoryStat.ReflectionPer, 0.1f)
        });
        AccessoryUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Armor, 1),
            new Tuple<int, float>((int)Enums.AccessoryStat.ReflectionPer, 0.1f)
        });
        AccessoryUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Armor, 1),
            new Tuple<int, float>((int)Enums.AccessoryStat.ReflectionPer, 0.1f)
        });
        AccessoryUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Armor, 1),
            new Tuple<int, float>((int)Enums.AccessoryStat.ReflectionPer, 0.1f)
        });

        // HollowHeart
        AccessoryUpgrade[2] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[2].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.2f)
        });
        AccessoryUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.2f)
        });
        AccessoryUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.2f)
        });
        AccessoryUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.2f)
        });
        AccessoryUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.2f)
        });

        // Pummarola
        AccessoryUpgrade[3] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[3].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f)
        });
        AccessoryUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f)
        });
        AccessoryUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f)
        });
        AccessoryUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f)
        });
        AccessoryUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f)
        });

        // EmptyTome
        AccessoryUpgrade[4] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[4].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CooldownPer, -0.8f)
        });
        AccessoryUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CooldownPer, -0.8f)
        });
        AccessoryUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CooldownPer, -0.8f)
        });
        AccessoryUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CooldownPer, -0.8f)
        });
        AccessoryUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CooldownPer, -0.8f)
        });

        // Candelabrador
        AccessoryUpgrade[5] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[5].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.10f)
        });
        AccessoryUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.10f)
        });
        AccessoryUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.10f)
        });
        AccessoryUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.10f)
        });
        AccessoryUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.10f)
        });

        // Bracer
        AccessoryUpgrade[6] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[6].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.1f)
        });
        AccessoryUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.1f)
        });
        AccessoryUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.1f)
        });
        AccessoryUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.1f)
        });
        AccessoryUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.1f)
        });

        // Spellbinder
        AccessoryUpgrade[7] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[7].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.1f)
        });
        AccessoryUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.1f)
        });
        AccessoryUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.1f)
        });
        AccessoryUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.1f)
        });
        AccessoryUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.1f)
        });

        // Duplicator
        AccessoryUpgrade[8] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[8].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Amount, 1)
        });
        AccessoryUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Amount, 1)
        });

        // Wings
        AccessoryUpgrade[9] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[9].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MoveSpeedPer, 10)
        });
        AccessoryUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MoveSpeedPer, 10)
        });
        AccessoryUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MoveSpeedPer, 10)
        });
        AccessoryUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MoveSpeedPer, 10)
        });
        AccessoryUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MoveSpeedPer, 10)
        });

        // Attractorb
        AccessoryUpgrade[10] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[10].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MagnetPer, 33)
        });
        AccessoryUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MagnetPer, 33)
        });
        AccessoryUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MagnetPer, 25)
        });
        AccessoryUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MagnetPer, 20)
        });
        AccessoryUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MagnetPer, 33)
        });

        // Clover
        AccessoryUpgrade[11] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[11].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.LuckPer, 10)
        });
        AccessoryUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.LuckPer, 10)
        });
        AccessoryUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.LuckPer, 10)
        });
        AccessoryUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.LuckPer, 10)
        });
        AccessoryUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.LuckPer, 10)
        });

        // Crown
        AccessoryUpgrade[12] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[12].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.GrowthPer, 8)
        });
        AccessoryUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.GrowthPer, 8)
        });
        AccessoryUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.GrowthPer, 8)
        });
        AccessoryUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.GrowthPer, 8)
        });
        AccessoryUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.GrowthPer, 8)
        });

        // StoneMask
        AccessoryUpgrade[13] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[13].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[13].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.GreedPer, 10)
        });
        AccessoryUpgrade[13].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.GreedPer, 10)
        });
        AccessoryUpgrade[13].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.GreedPer, 10)
        });
        AccessoryUpgrade[13].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.GreedPer, 10)
        });
        AccessoryUpgrade[13].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.GreedPer, 10)
        });

        // Tiragisu
        AccessoryUpgrade[14] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[14].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[14].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Revival, 1)
        });
        AccessoryUpgrade[14].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Revival, 1)
        });

        // Skull
        AccessoryUpgrade[15] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[15].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[15].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 10)
        });
        AccessoryUpgrade[15].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 10)
        });
        AccessoryUpgrade[15].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 10)
        });
        AccessoryUpgrade[15].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 10)
        });
        AccessoryUpgrade[15].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 10)
        });

        // SilverRing
        AccessoryUpgrade[16] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[16].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[16].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.05f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.05f)
        });
        AccessoryUpgrade[16].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.05f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.05f)
        });
        AccessoryUpgrade[16].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.05f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.05f)
        });
        AccessoryUpgrade[16].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.05f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.05f)
        });
        AccessoryUpgrade[16].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.05f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.05f)
        });
        AccessoryUpgrade[16].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.05f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.05f)
        });
        AccessoryUpgrade[16].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.05f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.05f)
        });
        AccessoryUpgrade[16].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.05f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.05f)
        });
        AccessoryUpgrade[16].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.05f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.05f)
        });

        // GoldRing
        AccessoryUpgrade[17] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[17].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[17].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[17].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[17].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[17].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[17].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[17].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[17].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[17].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[17].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });

        // MetaglioLeft
        AccessoryUpgrade[18] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[18].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[18].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f),
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.05f)
        });
        AccessoryUpgrade[18].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f),
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.05f)
        });
        AccessoryUpgrade[18].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f),
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.05f)
        });
        AccessoryUpgrade[18].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f),
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.05f)
        });
        AccessoryUpgrade[18].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f),
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.05f)
        });
        AccessoryUpgrade[18].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f),
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.05f)
        });
        AccessoryUpgrade[18].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f),
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.05f)
        });
        AccessoryUpgrade[18].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f),
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.05f)
        });
        AccessoryUpgrade[18].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f),
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.05f)
        });

        // MetaglioRight
        AccessoryUpgrade[19] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[19].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[19].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[19].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[19].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[19].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[19].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[19].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[19].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[19].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[19].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });

        // TorronaBox
        AccessoryUpgrade[20] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[20].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.0f)
        });
        AccessoryUpgrade[20].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.03f)
        });
        AccessoryUpgrade[20].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.03f)
        });
        AccessoryUpgrade[20].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.03f)
        });
        AccessoryUpgrade[20].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.03f)
        });
        AccessoryUpgrade[20].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.03f)
        });
        AccessoryUpgrade[20].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.03f)
        });
        AccessoryUpgrade[20].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.03f)
        });
        AccessoryUpgrade[20].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.03f)
        });
        AccessoryUpgrade[20].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 100)
        });

        AccessoriesMaxLevel = new int[21];
        for (int i = 0; i < 21; i++)
        {
            AccessoriesMaxLevel[i] = AccessoryUpgrade[i].Count - 1;
        }
    }
}
