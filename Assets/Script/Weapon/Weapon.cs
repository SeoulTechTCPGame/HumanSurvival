using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;
using System.Linq;

public class Weapon : MonoBehaviour
{
    //무기의 스탯 지정
    //예시를 위해 값은 무작위로 넣음
    public int WeaponIndex;
    public int WeaponLevel;
    public int WeaponMaxLevel;
    public bool Mastered = false;

    private int damage = 10;                //피해량
    private int projectileSpeed = 1;        //투사체 속도
    private int duration = 3;               //지속 시간
    private int attackRange = 1;            //공격범위
    private int cooldown = 3;               //쿨타임
    private int numberOfProjectiles = 1;    //투사체 수
    private int totalspeed;                 //총 속도

    public float[] WeaponStats;
    public static float[,] defaultWeaponStats;
    public static List<List<Tuple<int, float>>>[] WeaponUpgrade;

    static Weapon()
    {
        WeaponUpgrade = new List<List<Tuple<int, float>>>[13];
        levelOneWeaponPreprocessing();
        weaponUpgradePreprocessing();
    }
    public void WeaponSetting(int weaponIndex=0)
    {
        this.WeaponIndex = weaponIndex;
        this.WeaponStats = Enumerable.Range(0, defaultWeaponStats.GetLength(1)).Select(x => defaultWeaponStats[weaponIndex, x]).ToArray();

        WeaponLevel = 1;
        WeaponMaxLevel = (int)WeaponStats[(int)Enums.WeaponStat.MaxLevel];
    }

    private void Update()
    {
        transform.position = transform.position + Vector3.right * totalspeed * Time.deltaTime;
    }
    public void Shoot(int speed)
    {
        totalspeed = speed;
    }
    public bool Upgrade()
    {
        WeaponLevel++;
        foreach ((var statIndex, var data) in WeaponUpgrade[WeaponIndex][WeaponLevel])
        {
            WeaponStats[statIndex] += data;
        }
        if (WeaponLevel == WeaponMaxLevel)
        {
            Mastered = true;
            return true;
        }

        return false;
    }
    public bool IsMaster() // 삭제 예정
    {
        return Mastered;
    }
    //Get,Set함수 자동 구현
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public int ProjectileSpeed
    {
        get { return projectileSpeed; }
        set { projectileSpeed = value; }
    }
    public int Duration
    {
        get { return duration; }
        set { duration = value; }
    }
    public int AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }
    public int Cooldown
    {
        get { return cooldown; }
        set { cooldown = value; }
    }
    public int NumberOfProjectiles
    {
        get { return numberOfProjectiles; }
        set { numberOfProjectiles = value; }
    }

    public static void levelOneWeaponPreprocessing()
    {
        defaultWeaponStats = new float[13,9]
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
}
