using Enums;
using Rito;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPickUpSystem
{
    private static int[] mWeaponRarity;
    private static int[] mAccessoryRarity;
    private WeightedRandomPicker<int> mWeaponPicker;
    private WeightedRandomPicker<int> mAccessoryPicker;
    
    static RandomPickUpSystem()
    {
        mWeaponRarity = new int[10] { 100, 100, 100, 80, 80, 80, 70, 50, 50, 80 };
        mAccessoryRarity = new int[15] { 100, 100, 100, 90, 90, 90, 80, 80, 80, 70, 70, 70, 60, 60, 50 }; // 임시
    }
    public void UpdateWeaponPickUpList()
    {
        var luck = GameManager.instance.CharacterStats[(int)Enums.Stat.Luck];
        mWeaponPicker = new WeightedRandomPicker<int>();
        if (GameManager.instance.equipManageSys.Weapons.Count < Constants.MaxWeaponCount)
        {
            for (int i = 0; i < mWeaponRarity.Length; i++)
            {
                if (GameManager.instance.equipManageSys.HasWeapon(i) && GameManager.instance.equipManageSys.Weapons[GameManager.instance.equipManageSys.TransWeaponIndex[i]].IsMaster())
                    continue;
                mWeaponPicker.Add(i, (mWeaponRarity[i] + luck) / (double)mWeaponRarity[i]);
            }
        }
        else
        {
                for (int i = 0; i < GameManager.instance.equipManageSys.Weapons.Count; i++)
            {
                if (GameManager.instance.equipManageSys.Weapons[i].IsMaster())
                    continue;
                int nowIdx = GameManager.instance.equipManageSys.Weapons[i].WeaponIndex;
                mWeaponPicker.Add(nowIdx, (mWeaponRarity[nowIdx] + luck) / (double)mWeaponRarity[nowIdx]);
            }
        }
    }
    public void UpdateAccessoryPickUpList()
    {
        var luck = GameManager.instance.CharacterStats[(int)Enums.Stat.Luck];
        mAccessoryPicker = new WeightedRandomPicker<int>();
        if (GameManager.instance.equipManageSys.Accessories.Count < Constants.MaxAccessoryCount)
        {
            for (int i = 0; i < mAccessoryRarity.Length; i++)
            {
                if (GameManager.instance.equipManageSys.HasAcc(i) && GameManager.instance.equipManageSys.Accessories[GameManager.instance.equipManageSys.TransAccessoryIndex[i]].IsMaster())
                    continue;
                mAccessoryPicker.Add(i, (mAccessoryRarity[i] + luck) / (double)mAccessoryRarity[i]);
            }
        }
        else
        {
            for (int i = 0; i < GameManager.instance.equipManageSys.Accessories.Count; i++)
            {
                if (GameManager.instance.equipManageSys.Accessories[i].IsMaster())
                    continue;
                int nowIdx = GameManager.instance.equipManageSys.Accessories[i].AccessoryIndex;
                mAccessoryPicker.Add(nowIdx, (mAccessoryRarity[nowIdx] + luck) / (double)mAccessoryRarity[nowIdx]);
            }
        }
    }
    public List<Tuple<int, int, int>> RandomPickUp(int n)
    {
        var equipManageSys = GameManager.instance.equipManageSys;
        UpdateAccessoryPickUpList();
        UpdateWeaponPickUpList();
        int possibleWeaponChoice = 0, possibleAccessoryChoice = 0;
        getPossibleChoice(ref possibleWeaponChoice, ref possibleAccessoryChoice, equipManageSys);

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
                var pick = getOnePickUp(possibleWeaponChoice, possibleAccessoryChoice, pickedWeaponList, pickedAccessoryList, equipManageSys);
                pickUps.Add(pick);
                if (pick.Item1 == 0)
                    possibleWeaponChoice--;
                else
                    possibleAccessoryChoice--;
            }
        }

        return pickUps;
    }
        public List<Tuple<int, int, int>> RandomPickUp()
    {
        var equipManageSys = GameManager.instance.equipManageSys;
        UpdateAccessoryPickUpList();
        UpdateWeaponPickUpList();
        int possibleWeaponChoice = 0, possibleAccessoryChoice = 0;
        getPossibleChoice(ref possibleWeaponChoice, ref possibleAccessoryChoice, equipManageSys);

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
                var pick = getOnePickUp(possibleWeaponChoice, possibleAccessoryChoice, pickedWeaponList, pickedAccessoryList, equipManageSys);
                pickUps.Add(pick);
                if (pick.Item1 == 0)
                    possibleWeaponChoice--;
                else
                    possibleAccessoryChoice--;
            }
        }

        return pickUps;
    }
    private void getPossibleChoice(ref int possibleWeaponChoice, ref int possibleAccessoryChoice, EquipmentManagementSystem equipManageSys)
    {
        if (equipManageSys.Weapons.Count == Constants.MaxWeaponCount)
            possibleWeaponChoice = Constants.MaxWeaponCount - equipManageSys.MasteredWeapons.Count;
        else
            possibleWeaponChoice = Constants.MaxWeaponCount - equipManageSys.MasteredWeapons.Count;
        if (equipManageSys.Accessories.Count == Constants.MaxAccessoryCount)
            possibleAccessoryChoice = Constants.MaxAccessoryCount - equipManageSys.MasteredAccessories.Count;
        else
            possibleAccessoryChoice = Constants.MaxAccessoryCount - equipManageSys.MasteredAccessories.Count;
    }

    private Tuple<int, int, int> getOnePickUp(int possibleWeaponNum, int possibleAccessoryNum, List<int> pickedWeaponList, List<int> pickedAccessoryList, EquipmentManagementSystem equipManagerSys)
    {   // < 0: weapon / 1: accessory, index , 0: new / 1: old >
        int pickType = getPickType(possibleWeaponNum, possibleAccessoryNum);
        int pick = -1;
        int hasPick = 1;
        if (pickType == 0)
        {
            pick = getWeaponRandomPick(pickedWeaponList);
            if (equipManagerSys.TransWeaponIndex[pick] < 0)
                hasPick = 0;
        }
        else
        {
            pick = getAccessoryRandomPick(pickedAccessoryList);
            if (equipManagerSys.TransAccessoryIndex[pick] < 0)
                hasPick = 0;
        }

        return new Tuple<int, int, int>(pickType, pick, hasPick);
    }
    private int getChoice()
    {
        if (UnityEngine.Random.Range(0, 101) <= GameManager.instance.CharacterStats[(int)Enums.Stat.Luck])
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
        while (pickedWeaponList.Contains(pick))
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
}
