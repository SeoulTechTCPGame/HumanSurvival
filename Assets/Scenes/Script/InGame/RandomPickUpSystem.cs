using Rito;
using System;
using System.Collections.Generic;

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
        var luck = GameManager.instance.CharacterStats[(int)Enums.EStat.Luck];
        mWeaponPicker = new WeightedRandomPicker<int>();
        if (GameManager.instance.EquipManageSys.Weapons.Count < Constants.MAX_WEAPON_COUNT)
        {
            for (int i = 0; i < mWeaponRarity.Length; i++)
            {
                if (GameManager.instance.EquipManageSys.HasWeapon(i) && GameManager.instance.EquipManageSys.Weapons[GameManager.instance.EquipManageSys.TransWeaponIndex[i]].IsMaster())
                    continue;
                mWeaponPicker.Add(i, (mWeaponRarity[i] + luck) / (double)mWeaponRarity[i]);
            }
        }
        else
        {
                for (int i = 0; i < GameManager.instance.EquipManageSys.Weapons.Count; i++)
            {
                if (GameManager.instance.EquipManageSys.Weapons[i].IsMaster())
                    continue;
                int nowIdx = GameManager.instance.EquipManageSys.Weapons[i].WeaponIndex;
                mWeaponPicker.Add(nowIdx, (mWeaponRarity[nowIdx] + luck) / (double)mWeaponRarity[nowIdx]);
            }
        }
    }
    public void UpdateAccessoryPickUpList()
    {
        var luck = GameManager.instance.CharacterStats[(int)Enums.EStat.Luck];
        mAccessoryPicker = new WeightedRandomPicker<int>();
        if (GameManager.instance.EquipManageSys.Accessories.Count < Constants.MAX_ACCESSORY_COUNT)
        {
            for (int i = 0; i < mAccessoryRarity.Length; i++)
            {
                if (GameManager.instance.EquipManageSys.HasAcc(i) && GameManager.instance.EquipManageSys.Accessories[GameManager.instance.EquipManageSys.TransAccessoryIndex[i]].IsMaster())
                    continue;
                mAccessoryPicker.Add(i, (mAccessoryRarity[i] + luck) / (double)mAccessoryRarity[i]);
            }
        }
        else
        {
            for (int i = 0; i < GameManager.instance.EquipManageSys.Accessories.Count; i++)
            {
                if (GameManager.instance.EquipManageSys.Accessories[i].IsMaster())
                    continue;
                int nowIdx = GameManager.instance.EquipManageSys.Accessories[i].AccessoryIndex;
                mAccessoryPicker.Add(nowIdx, (mAccessoryRarity[nowIdx] + luck) / (double)mAccessoryRarity[nowIdx]);
            }
        }
    }
    public List<Tuple<int, int, int>> RandomPickUp(EquipmentManagementSystem equipManageSys)
    {
        UpdateAccessoryPickUpList();
        UpdateWeaponPickUpList();
        int possibleWeaponChoice = 0, possibleAccessoryChoice = 0;
        GetPossibleChoice(ref possibleWeaponChoice, ref possibleAccessoryChoice, equipManageSys);

        int maxChoice = System.Math.Min(GetChoice(), possibleWeaponChoice + possibleAccessoryChoice);

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
                var pick = GetOnePickUp(possibleWeaponChoice, possibleAccessoryChoice, pickedWeaponList, pickedAccessoryList, equipManageSys);
                pickUps.Add(pick);
                if (pick.Item1 == 0)
                    possibleWeaponChoice--;
                else
                    possibleAccessoryChoice--;
            }
        }

        return pickUps;
    }
    private void GetPossibleChoice(ref int possibleWeaponChoice, ref int possibleAccessoryChoice, EquipmentManagementSystem equipManageSys)
    {
        if (equipManageSys.Weapons.Count == Constants.MAX_WEAPON_COUNT)
        {
            possibleWeaponChoice = Constants.MAX_WEAPON_COUNT - equipManageSys.MasteredWeapons.Count;
        }
        else
        {
            possibleWeaponChoice = Constants.MAX_WEAPON_COUNT - equipManageSys.MasteredWeapons.Count;
        }
        if (equipManageSys.Accessories.Count == Constants.MAX_ACCESSORY_COUNT)
        {
            possibleAccessoryChoice = Constants.MAX_ACCESSORY_COUNT - equipManageSys.MasteredAccessories.Count;
        }
        else
        {
            possibleAccessoryChoice = Constants.MAX_ACCESSORY_COUNT - equipManageSys.MasteredAccessories.Count;
        }
    }

    private Tuple<int, int, int> GetOnePickUp(int possibleWeaponNum, int possibleAccessoryNum, List<int> pickedWeaponList, List<int> pickedAccessoryList, EquipmentManagementSystem equipManagerSys)
    {   // < 0: weapon / 1: accessory, index , 0: new / 1: old >
        int pickType = GetPickType(possibleWeaponNum, possibleAccessoryNum);
        int pick = -1;
        int hasPick = 1;
        if (pickType == 0)
        {
            pick = GetWeaponRandomPick(pickedWeaponList);
            if (equipManagerSys.TransWeaponIndex[pick] < 0)
            {
                hasPick = 0;
            }
        }
        else
        {
            pick = GetAccessoryRandomPick(pickedAccessoryList);
            if (equipManagerSys.TransAccessoryIndex[pick] < 0)
            {
                hasPick = 0;
            }
        }

        return new Tuple<int, int, int>(pickType, pick, hasPick);
    }
    private int GetChoice()
    {
        if (UnityEngine.Random.Range(0, 101) <= GameManager.instance.CharacterStats[(int)Enums.EStat.Luck])
        {
            return 4;
        }
        else
        {
            return 3;
        }
    }
    private int GetPickType(int possibleWeaponNum, int possibleAccessoryNum)    // 0: weapon, 1: accessory
    {
        if (possibleWeaponNum == 0)
        {
            return 1;
        }
        else if (possibleAccessoryNum == 0)
        {
            return 0;
        }
        else
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
    private int GetWeaponRandomPick(List<int> pickedWeaponList)
    {
        int pick = mWeaponPicker.GetRandomPick();
        while (pickedWeaponList.Contains(pick))
        {
            pick = mWeaponPicker.GetRandomPick();
        }
        pickedWeaponList.Add(pick);
        return pick;
    }
    private int GetAccessoryRandomPick(List<int> pickedAccessoryList)
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
