using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

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
        TransWeaponIndex = Enumerable.Repeat<int>(-1, Constants.MaxWeaponNumber).ToArray<int>();
        TransAccessoryIndex = Enumerable.Repeat<int>(-1, Constants.MaxAccessoryNumber).ToArray<int>();
        MasteredWeapons = new List<int>();
        MasteredAccessories = new List<int>();

        SetNewWeapon(startingWeapon);
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
            SetNewWeapon(weaponIndex);
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
    public bool HasWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= Constants.MaxWeaponNumber)
            return false;
        return TransWeaponIndex[weaponIndex] >= 0;
    }
    public bool HasAcc(int accIndex)
    {
        if (accIndex < 0 || accIndex >= Constants.MaxAccessoryNumber)
            return false;
        return TransAccessoryIndex[accIndex] >= 0;
    }
    //ToDo: SkillFiringSystem이랑 연계 할 함수
    public void SetNewWeapon(int weaponIndex)
    {
        GameManager.instance.weaponGetTime[weaponIndex] = GameManager.instance.gameTime;
        TransWeaponIndex[weaponIndex] = Weapons.Count;

        addNewWeapon(weaponIndex);
        Weapons.Last().WeaponDefalutSetting(weaponIndex);
        GameManager.instance.WeaponSlot.GetComponent<SlotUI>().AddSlot(weaponIndex, 0);

        processWeaponSub(weaponIndex, Weapons.Last());
    }
    public Weapon GetWeapon(int weaponIndex)
    {
        if (TransWeaponIndex[weaponIndex] < 0)
            Debug.Log("없는 무기 호출");
        // TODO: 없는 무기 호출시 에러! 제대로 작성
        Debug.Log(TransWeaponIndex[weaponIndex]);
        Debug.Log(Weapons[TransWeaponIndex[weaponIndex]]);
        return Weapons[TransWeaponIndex[weaponIndex]];
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
    private void processWeaponSub(int weaponIndex, Weapon weapon)
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
    private void addNewWeapon(int weaponIndex)
    {
        switch (weaponIndex)
        {
            case 0: // Whip
                //Weapons.Add((SkillFiringSystem.instance.weaponPrefabs[weaponIndex]).GetComponent<Whip>());
                break;
            case 1: // MagicWand
                Weapons.Add((SkillFiringSystem.instance.weaponPrefabs[weaponIndex]).GetComponent<MagicWand>());
                break;
            case 2: // Knife
                Weapons.Add((SkillFiringSystem.instance.weaponPrefabs[weaponIndex]).GetComponent<Knife>());
                break;
            case 3: // Cross
                //Weapons.Add((SkillFiringSystem.instance.weaponPrefabs[weaponIndex]).GetComponent<Cross>());
                break;
            case 4: // KingBible
                //Weapons.Add((SkillFiringSystem.instance.weaponPrefabs[weaponIndex]).GetComponent<KingBible>());
                break;
            case 5: // FireWand
                //Weapons.Add((SkillFiringSystem.instance.weaponPrefabs[weaponIndex]).GetComponent<FireWand>());
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
                //Weapons.Add((SkillFiringSystem.instance.weaponPrefabs[weaponIndex]).GetComponent<LightningRing>());
                break;
        }
    }
}
