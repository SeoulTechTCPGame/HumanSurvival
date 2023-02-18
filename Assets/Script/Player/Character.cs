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

    private int mDamage = 10;              //피해량
    private int mProjectileSpeed = 1;     //투사체 속도
    private int mDuration = 3;            //지속 시간
    private int mAttackRange = 1;         //공격범위
    private int mCooldown = 3;            //쿨타임
    private int mNumberOfProjectiles = 1;     //투사체 수

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
    public List<Accessory> Accessories;
    public List<int> MasteredWeapons;
    public List<int> MasteredAccessories;

    private int[] mTransWeaponIndex; // 해당 index의 weapon이 현재 보유중인 Weapons의 몇 번째 index에 있는지 반환하는 배열, 없다면 -1 반환
    private int[] mTransAccessoryIndex; // 위와 같으나 Accessory에 해당
    private void Awake()
    {
        mWeaponRarity = new int[13] { 100, 100, 100, 100, 80, 80, 80, 70, 100, 50, 50, 80, 80 };
        mAccessoryRarity = new int[21] { 100, 100, 100, 90, 90, 90, 80, 80, 80, 70, 70, 70, 60, 60, 60, 50, 50, 50, 40, 40, 40 }; // 임시
    }
    void Start()
    {
        mLevel = 1;
        mExp = 0;
        mMaxExp = 100;
        mdExp = 10;

        // TODO: user가 메인 화면에서 강화해놓은 스탯들을 기본값으로 받아오기
        CharacterStats = new float[21] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 70, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

        Weapons = new List<Weapon>();
        Accessories = new List<Accessory>();

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
    //버튼 지우면 삭제 예정
    public void TempLoad()
    {
        LevelUp();
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
        LevepUpUI.GetComponent<LevelUpUIManager>().LoadLevelUpUI(CharacterStats, pickUps, Weapons, Accessories);
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
        if (Accessories.Count < Constants.MaxAccessoryCount)
        {
            for (int i = 0; i < mAccessoryRarity.Length; i++)
            {
                mAccessoryPicker.Add(i, (mAccessoryRarity[i] + luck) / (double)mAccessoryRarity[i]);
            }
        }
        else
        {
            for (int i = 0; i < Accessories.Count; i++)
            {
                if (Accessories[i].AccessoryLevel == Accessories[i].AccessoryMaxLevel)
                    continue;
                int nowIdx = Accessories[i].AccessoryIndex;
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
        if (Accessories.Count == Constants.MaxAccessoryCount)
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
    //ToDo: SkillFiringSystem이랑 연계 할 함수
    public void GetWeapon(int weaponIndex)
    {
        mTransWeaponIndex[weaponIndex] = Weapons.Count;
        Weapon newWeapon = this.gameObject.AddComponent<Weapon>();
        newWeapon.WeaponSetting(weaponIndex);
        Weapons.Add(newWeapon);
        UpdateWeaponPickUpList(CharacterStats[(int)Enums.Stat.Luck]);
    }
    public void UpgradeWeapon(int weaponIndex)
    {
        Weapons[mTransWeaponIndex[weaponIndex]].Upgrade();
        if (Weapons[mTransWeaponIndex[weaponIndex]].IsMaster())
        {
            MasteredWeapons.Add(weaponIndex);
            UpdateWeaponPickUpList(CharacterStats[(int)Enums.Stat.Luck]);
        }
    }
    public void GetAccessory(int accessoryIndex)
    {
        mTransAccessoryIndex[accessoryIndex] = Accessories.Count;
        Accessories.Add(new Accessory(accessoryIndex));
        UpgradeAccessory(accessoryIndex);
        UpdateAccessoryPickUpList(CharacterStats[(int)Enums.Stat.Luck]);
    }
    public void UpgradeAccessory(int accessoryIndex)
    {
        Accessories[mTransAccessoryIndex[accessoryIndex]].Upgrade(this);
        if (Accessories[mTransAccessoryIndex[accessoryIndex]].IsMaster())
        {
            MasteredAccessories.Add(accessoryIndex);
            UpdateAccessoryPickUpList(CharacterStats[(int)Enums.Stat.Luck]);
        }
    }

    //Get,Set함수 자동 구현
    public int Damage
    {
        get { return mDamage; }
        set { mDamage = value; }
    }
    public int ProjectileSpeed
    {
        get { return mProjectileSpeed; }
        set { mProjectileSpeed = value; }
    }
    public int Duration
    {
        get { return mDuration; }
        set { mDuration = value; }
    }
    public int AttackRange
    {
        get { return mAttackRange; }
        set { mAttackRange = value; }
    }
    public int Cooldown
    {
        get { return mCooldown; }
        set { mCooldown = value; }
    }
    public int NumberOfProjectiles
    {
        get { return mNumberOfProjectiles; }
        set { mNumberOfProjectiles = value; }
    }
}
