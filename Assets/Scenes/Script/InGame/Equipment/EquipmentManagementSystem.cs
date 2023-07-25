using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using Enums;

public class EquipmentManagementSystem 
{
    public List<Weapon> Weapons;
    public List<Accessory> Accessories;
    public List<int> MasteredWeapons;
    public List<int> MasteredAccessories;
    public int[] TransWeaponIndex; // 해당 index의 weapon이 현재 보유중인 Weapons의 몇 번째 index에 있는지 반환하는 배열, 없다면 -1 반환
    public int[] TransAccessoryIndex; // 위와 같으나 Accessory에 해당

    public void Set(int startingWeapon)
    {
        Weapons = new List<Weapon>();
        Accessories = new List<Accessory>();
        TransWeaponIndex = Enumerable.Repeat<int>(-1, Constants.MAX_WEAPON_NUMBER).ToArray<int>();
        TransAccessoryIndex = Enumerable.Repeat<int>(-1, Constants.MAX_ACCESSORY_NUMBER).ToArray<int>();
        MasteredWeapons = new List<int>();
        MasteredAccessories = new List<int>();

        SetNewWeapon(startingWeapon);
    }
    public void ApplyItem(Tuple<int, int, int> pickUp)
    {
        switch ((Enums.EPickUpType)pickUp.Item1)
        {
            case Enums.EPickUpType.Weapon:
                ApplyWeapon(pickUp.Item2, pickUp.Item3);
                break;
            case Enums.EPickUpType.Accessory:
                ApplyAccessory(pickUp.Item2, pickUp.Item3);
                break;
            default:
                ApplyEtc(pickUp.Item2);
                break;
        }
    }
    public bool HasWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= Constants.MAX_WEAPON_NUMBER)
            return false;
        return TransWeaponIndex[weaponIndex] >= 0;
    }
    public bool HasWeapon(EWeapon weapon)
    {
        return TransWeaponIndex[(int)weapon] >= 0;
    }
    public bool HasAcc(int accIndex)
    {
        if (accIndex < 0 || accIndex >= Constants.MAX_ACCESSORY_NUMBER)
            return false;
        return TransAccessoryIndex[accIndex] >= 0;
    }
    public void SetNewWeapon(int weaponIndex)
    {
        GameManager.instance.WeaponGetTime[weaponIndex] = GameManager.instance.GameTime;

        AddNewWeapon(weaponIndex);
        TransWeaponIndex[weaponIndex] = Weapons.Count - 1;

        Weapons.Last().WeaponDefalutSetting(weaponIndex);
        GameManager.instance.WeaponSlot.GetComponent<SlotUI>().AddSlot(weaponIndex, 0);

        ProcessWeaponSub(weaponIndex, Weapons.Last());
    }
    public Weapon GetWeapon(int weaponIndex)
    {
        if (TransWeaponIndex[weaponIndex] < 0)
            Debug.Log("없는 무기 호출");
        // TODO: 없는 무기 호출시 에러! 제대로 작성
        return Weapons[TransWeaponIndex[weaponIndex]];
    }
    public Weapon GetWeapon(EWeapon weapon)
    {
        if (TransWeaponIndex[(int)weapon] < 0)
            Debug.Log("없는 무기 호출");
        // TODO: 없는 무기 호출시 에러! 제대로 작성
        return Weapons[TransWeaponIndex[(int)weapon]];
    }
    public void UpgradeWeapon(int weaponIndex)
    {
        Weapons[TransWeaponIndex[weaponIndex]].Upgrade();
        if (Weapons[TransWeaponIndex[weaponIndex]].IsMaster())
        {
            MasteredWeapons.Add(weaponIndex);
        }
    }
    public void GetAccessory(int accessoryIndex)
    {
        TransAccessoryIndex[accessoryIndex] = Accessories.Count;
        Accessories.Add(new Accessory(accessoryIndex));
        UpgradeAccessory(accessoryIndex);
        GameManager.instance.AccessorySlot.GetComponent<SlotUI>().AddSlot(accessoryIndex, 1);
    }
    public void UpgradeAccessory(int accessoryIndex)
    {
        Accessories[TransAccessoryIndex[accessoryIndex]].Upgrade();
        if (Accessories[TransAccessoryIndex[accessoryIndex]].IsMaster())
        {
            MasteredAccessories.Add(accessoryIndex);
        }
    }
    private void ProcessWeaponSub(int weaponIndex, Weapon weapon)
    {
        switch(weaponIndex) 
        {
            case 7: // peachOne
                weapon.GetComponent<Peachone>().SpawnBlueBird(SkillFiringSystem.instance.Birds[0]);
                break;
            case 8: // ebonyWings
                weapon.GetComponent<EbonyWings>().SpawnBlackBird(SkillFiringSystem.instance.Birds[1]);
                break;
        }
    }
    private void AddNewWeapon(int weaponIndex)
    {
        switch (weaponIndex)
        {
            case 0: // Whip
                Weapons.Add((SkillFiringSystem.instance.weaponPrefabs[weaponIndex]).GetComponent<Whip>());
                break;
            case 1: // MagicWand
                Weapons.Add((SkillFiringSystem.instance.weaponPrefabs[weaponIndex]).GetComponent<MagicWand>());
                break;
            case 2: // Knife
                Weapons.Add((SkillFiringSystem.instance.weaponPrefabs[weaponIndex]).GetComponent<Knife>());
                break;
            case 3: // Cross
                Weapons.Add((SkillFiringSystem.instance.weaponPrefabs[weaponIndex]).GetComponent<Cross>());
                break;
            case 4: // KingBible
                Weapons.Add((SkillFiringSystem.instance.weaponPrefabs[weaponIndex]).GetComponent<KingBible>());
                break;
            case 5: // FireWand
                Weapons.Add((SkillFiringSystem.instance.weaponPrefabs[weaponIndex]).GetComponent<FireWand>());
                break;
            case 6: // Garlic
                Weapons.Add((SkillFiringSystem.instance.weaponPrefabs[weaponIndex]).GetComponent<Gralic>());
                break;
            case 7: // peachOne
                Weapons.Add((SkillFiringSystem.instance.weaponPrefabs[weaponIndex]).GetComponent<Peachone>());
                break;
            case 8: // ebonyWings
                Weapons.Add((SkillFiringSystem.instance.weaponPrefabs[weaponIndex]).GetComponent<EbonyWings>());
                break;
            case 9: // LightningRing
                Weapons.Add((SkillFiringSystem.instance.weaponPrefabs[weaponIndex]).GetComponent<LightningRing>());
                break;
        }
    }
    private void ApplyWeapon(int weaponIndex, int hasWeapon)
    {
        if (hasWeapon == 0)
            SetNewWeapon(weaponIndex);
        else
            UpgradeWeapon(weaponIndex);
    }
    private void ApplyAccessory(int accessoryIndex, int hasAccessory)
    {
        if (hasAccessory == 0)
            GetAccessory(accessoryIndex);
        else
            UpgradeAccessory(accessoryIndex);
    }
    private void ApplyEtc(int etcIndex)
    {
        switch ((Enums.EEtc)etcIndex)
        {
            case Enums.EEtc.Food:
                // TODO: 체력 회복 함수와 연결
                break;
            case Enums.EEtc.Money:
                GameManager.instance.GetCoin(25);
                break;
            default:
                break;
        }
    }
}