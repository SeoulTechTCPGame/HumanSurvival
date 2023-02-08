using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Enums;
using Rito;
using static UnityEngine.Rendering.DebugUI.Table;
using static Constants;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

public class Character : MonoBehaviour
{
    //캐릭터의 스탯지정
    //예시를 위해 값은 무작위로 넣음
    public GameObject LevepUpUI;

    private int damage = 10;              //피해량
    private int projectileSpeed = 1;     //투사체 속도
    private int duration = 3;            //지속 시간
    private int attackRange = 1;         //공격범위
    private int cooldown = 3;            //쿨타임
    private int numberOfProjectiles = 1;     //투사체 수

    private int mLevel;
    private int mExp;
    private int mMaxExp;
    private int mdExp;
    private int mMaxWeaponNumber = 6;
    private int mMaxAccessoryNumber = 6;

    public float[] CharacterStats;
    private int[] mWeaponRarity;
    private int[] mAccessoryRarity;
    private WeightedRandomPicker<int> mWeaponPicker;
    private WeightedRandomPicker<int> mAccessoryPicker;
    public List<Weapon> Weapons;
    public List<Tuple<int, int, int>> Accessorys;  // tuple< Accessory_index, now_Accessory_level, max_Accessory_level >
    public static int[] AccessoriesMaxLevel;
    public static List<List<Tuple<int, float>>>[] AccessoryUpgrade;
    public List<int> MasteredWeapons;
    public List<int> MasteredAccessories;

    private int[] mTransWeaponIndex; // 해당 index의 weapon이 현재 보유중인 Weapons의 몇 번째 index에 있는지 반환하는 배열, 없다면 -1 반환
    private int[] mTransAccessoryIndex; // 위와 같으나 Accessory에 해당
    static Character()
    {
        AccessoriesMaxLevel = new int[21];
        AccessoryUpgrade = new List<List<Tuple<int, float>>>[21];
    }
    void Start()
    {
        mLevel = 1;
        mExp = 0;
        mMaxExp = 100;
        mdExp = 10;

        // TODO: user가 메인 화면에서 강화해놓은 스탯들을 기본값으로 받아오기
        CharacterStats = new float[21] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 70, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        AccessoryUpgradePreprocessing();

        Weapons = new List<Weapon>();
        Accessorys = new List<Tuple<int, int, int>>();

        mWeaponRarity = new int[13] { 100, 100, 100, 100, 80, 80, 80, 70, 100, 50, 50, 80, 80 };
        mAccessoryRarity = new int[21] { 100, 100, 100, 90, 90, 90, 80, 80, 80, 70, 70, 70, 60, 60, 60, 50, 50, 50, 40, 40, 40 }; // 임시

        mTransWeaponIndex = Enumerable.Repeat<int>(-1, 13).ToArray<int>();
        mTransAccessoryIndex = Enumerable.Repeat<int>(-1, 21).ToArray<int>();

        MasteredWeapons = new List<int>();
        MasteredAccessories = new List<int>();

        UpdateLuck(CharacterStats[(int)Enums.Stat.Luck]);


        // 임시
        GetWeapon(0);
        GetWeapon(1);
        GetAccessory(0);
        GetAccessory(1);

    }
    public void TempLoad()
    {
        LevelUp();
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
    public void GetExp(int exp)
    {
        // TODO: stat의 growth 적용하여 경험치 획득

        mExp += exp;
        while (mExp >= mMaxExp)
        {
            mExp -= mMaxExp;
            mMaxExp += mdExp;
            LevelUp();
        }
    }
    public void LevelUp()
    {
        mLevel++;
        // 게임 일시정지

        var pickUps = RandomPickUp();
        LevepUpUI.GetComponent<LevelUpUIManager>().LoadLevelUpUI(CharacterStats, pickUps, Weapons, Accessorys);
        // 게임 재개

    }
    public void UpdateLuck(float luck)
    {
        UpdateWeaponPickUpList(luck);
        UpdateAccessoryPickUpList(luck);
    }
    public void UpdateWeaponPickUpList(float luck)
    {
        CharacterStats[(int)Enums.Stat.Luck] = luck;
        mWeaponPicker = new WeightedRandomPicker<int>();
        if (Weapons.Count < Constants.MaxWeaponCount)
        {
            for (int i = 0; i < mWeaponRarity.Length; i++)
            {
                if (mTransWeaponIndex[i] >= 0 && Weapons[mTransWeaponIndex[i]].Mastered)
                    continue;
                mWeaponPicker.Add(i, (mWeaponRarity[i] + luck) / (double)mWeaponRarity[i]);
            }
        }
        else
        {
            for (int i = 0; i < Weapons.Count; i++)
            {
                if (Weapons[i].Mastered)
                    continue;
                int nowIdx = Weapons[i].WeaponIndex;
                mWeaponPicker.Add(nowIdx, (mWeaponRarity[nowIdx] + luck) / (double)mWeaponRarity[nowIdx]);
            }
        }
    }
    public void UpdateAccessoryPickUpList(float luck)
    {
        mAccessoryPicker = new WeightedRandomPicker<int>();
        if (Accessorys.Count < Constants.MaxAccessoryCount)
        {
            for (int i = 0; i < mAccessoryRarity.Length; i++)
            {
                mAccessoryPicker.Add(i, (mAccessoryRarity[i] + luck) / (double)mAccessoryRarity[i]);
            }
        }
        else
        {
            for (int i = 0; i < Accessorys.Count; i++)
            {
                if (Accessorys[i].Item2 == Accessorys[i].Item3)
                    continue;
                int nowIdx = Accessorys[i].Item1;
                mAccessoryPicker.Add(nowIdx, (mAccessoryRarity[nowIdx] + luck) / (double)mAccessoryRarity[nowIdx]);
            }
        }
    }
    public List<Tuple<int, int, int>> RandomPickUp()
    {
        int possibleWeaponChoice = 0, possibleAccessoryChoice = 0;
        getPossibleChoice(ref possibleWeaponChoice, ref possibleAccessoryChoice);

        int maxChoice = System.Math.Min(getChoice(), possibleWeaponChoice + possibleAccessoryChoice);

        List<Tuple<int, int, int>> pickUps = new List<Tuple<int, int, int>>();
        if (maxChoice == 0)
        {
            // TODO: 25골드 or hp 30 회복 선택지
            pickUps.Add(new Tuple<int, int, int>(2, 0, 1));
            pickUps.Add(new Tuple<int, int, int>(2, 1, 1));
        }
        else
        {
            List<int> pickedWeaponList = new List<int>();
            List<int> pickedAccessoryList = new List<int>();
            for (int i = 0; i < maxChoice; i++)
            {
                var pick = getOnePickUp(possibleWeaponChoice, possibleAccessoryChoice, pickedWeaponList, pickedAccessoryList);
                pickUps.Add(pick);
                if (pick.Item1 == 0)
                    possibleWeaponChoice--;
                else
                    possibleAccessoryChoice--;
            }
        }

        return pickUps;
    }
    private void getPossibleChoice(ref int possibleWeaponChoice, ref int possibleAccessoryChoice)
    {
        if (Weapons.Count == Constants.MaxWeaponCount)
            possibleWeaponChoice = Constants.MaxWeaponCount - MasteredWeapons.Count;
        else
            possibleWeaponChoice = mMaxWeaponNumber - MasteredWeapons.Count;
        if (Accessorys.Count == Constants.MaxAccessoryCount)
            possibleAccessoryChoice = Constants.MaxAccessoryCount - MasteredAccessories.Count;
        else
            possibleAccessoryChoice = mMaxAccessoryNumber - MasteredAccessories.Count;
    }

    private Tuple<int, int, int> getOnePickUp(int possibleWeaponNum, int possibleAccessoryNum, List<int> pickedWeaponList, List<int> pickedAccessoryList)
    {   // < 0: weapon / 1: accessory, index , 0: new / 1: old >
        int pickType = getPickType(possibleWeaponNum, possibleAccessoryNum);
        int pick = -1;
        int hasPick = 1;
        if (pickType == 0)
        {
            pick = getWeaponRandomPick(pickedWeaponList);
            if (mTransWeaponIndex[pick] < 0)
                hasPick = 0;
        }
        else
        {
            pick = getAccessoryRandomPick(pickedAccessoryList);
            if (mTransAccessoryIndex[pick] < 0)
                hasPick = 0;
        }

        return new Tuple<int, int, int>(pickType, pick, hasPick);
    }
    private int getChoice()
    {
        if (UnityEngine.Random.Range(0, 101) <= CharacterStats[(int)Enums.Stat.Luck])
        {
            return 4;
        }
        else
        {
            return 3;
        }
    }
    private int getPickType(int possibleWeaponNum, int possibleAccessoryNum)    // 0: weapon, 1: accessory
    {
        if (possibleWeaponNum == 0)
            return 1;
        else if (possibleAccessoryNum == 0)
            return 0;
        else
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
                return 0;
            else
                return 1;
        }
    }
    private int getWeaponRandomPick(List<int> pickedWeaponList)
    {
        int pick = mWeaponPicker.GetRandomPick();
        while(pickedWeaponList.Contains(pick))
        {
            pick = mWeaponPicker.GetRandomPick();
        }
        pickedWeaponList.Add(pick);
        return pick;
    }
    private int getAccessoryRandomPick(List<int> pickedAccessoryList)
    {
        int pick = mAccessoryPicker.GetRandomPick();
        while (pickedAccessoryList.Contains(pick))
        {
            pick = mAccessoryPicker.GetRandomPick();
        }
        pickedAccessoryList.Add(pick);
        return pick;
    }
    public void ApplyItem(Tuple<int, int, int> pickUp)
    {
        switch ((Enums.PickUpType)pickUp.Item1)
        {
            case Enums.PickUpType.Weapon:
                applyWeapon(pickUp.Item2, pickUp.Item3);
                break;
            case Enums.PickUpType.Accessory:
                applyAccessory(pickUp.Item2, pickUp.Item3);
                break;
            default:
                applyEtc(pickUp.Item2);
                break;
        }
    }
    private void applyWeapon(int weaponIndex, int hasWeapon)
    {
        if (hasWeapon == 0)
            GetWeapon(weaponIndex);
        else
            UpgradeWeapon(weaponIndex);
    }
    private void applyAccessory(int accessoryIndex, int hasAccessory)
    {
        if (hasAccessory == 0)
            GetAccessory(accessoryIndex);
        else
            UpgradeAccessory(accessoryIndex);
    }
    private void applyEtc(int etcIndex)
    {
        switch ((Enums.Etc)etcIndex)
        {
            case Enums.Etc.Food:
                // TODO: 체력 회복 함수와 연결
                break;
            case Enums.Etc.Money:
                // TODO: 재화 획득 함수와 연결
                break;
            default:
                break;
        }
    }
    public void GetWeapon(int weaponIndex)
    {
        mTransWeaponIndex[weaponIndex] = Weapons.Count;
        Weapons.Add(new Weapon(weaponIndex));
        UpdateWeaponPickUpList(CharacterStats[(int)Enums.Stat.Luck]);
    }
    public void UpgradeWeapon(int weaponIndex)
    {
        bool isMastered = Weapons[mTransWeaponIndex[weaponIndex]].Upgrade();
        if (isMastered)
        {
            MasteredWeapons.Add(weaponIndex);
            UpdateWeaponPickUpList(CharacterStats[(int)Enums.Stat.Luck]);
        }
    }
    public void GetAccessory(int accessoryIndex)
    {
        mTransAccessoryIndex[accessoryIndex] = Accessorys.Count;
        Accessorys.Add(new Tuple<int, int, int>(accessoryIndex, 0, AccessoriesMaxLevel[accessoryIndex]));
        UpgradeAccessory(accessoryIndex);
        UpdateAccessoryPickUpList(CharacterStats[(int)Enums.Stat.Luck]);
    }
    public void UpgradeAccessory(int accessoryIndex)
    {
        bool isMastered = Accessorys[mTransAccessoryIndex[accessoryIndex]].Item2 + 1 == AccessoriesMaxLevel[accessoryIndex];
        Accessorys[mTransAccessoryIndex[accessoryIndex]] = new Tuple<int, int, int>(Accessorys[mTransAccessoryIndex[accessoryIndex]].Item1, Accessorys[mTransAccessoryIndex[accessoryIndex]].Item2 + 1, Accessorys[mTransAccessoryIndex[accessoryIndex]].Item3);
        if (isMastered)
        {
            MasteredAccessories.Add(accessoryIndex);
            UpdateAccessoryPickUpList(CharacterStats[(int)Enums.Stat.Luck]);
        }

        foreach ((var statIndex, var data) in AccessoryUpgrade[accessoryIndex][Accessorys[mTransAccessoryIndex[accessoryIndex]].Item2])
        {
            CharacterStats[statIndex] += data;
            if(statIndex == (int)Enums.Stat.Luck)
            {
                UpdateLuck(CharacterStats[statIndex]);
            }
        }
    }
    private void AccessoryUpgradePreprocessing()
    {
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

        for (int i = 0; i < 21; i++)
        {
            AccessoriesMaxLevel[i] = AccessoryUpgrade[i].Count - 1;
        }
    }

}
