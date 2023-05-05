using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class EquipmentManagementSystem : MonoBehaviour
{
    public List<Weapon> Weapons;
    public List<Accessory> Accessories;
    public List<int> MasteredWeapons;
    public List<int> MasteredAccessories;
    public SkillFiringSystem skillFiringSystem;
    public int[] TransWeaponIndex; // 해당 index의 weapon이 현재 보유중인 Weapons의 몇 번째 index에 있는지 반환하는 배열, 없다면 -1 반환
    public int[] TransAccessoryIndex; // 위와 같으나 Accessory에 해당

    public void Set(int startingWeapon)
    {
        skillFiringSystem = GameObject.Find("SkillFiringSystem").GetComponent<SkillFiringSystem>();
        Weapons = new List<Weapon>();
        Accessories = new List<Accessory>();
        TransWeaponIndex = Enumerable.Repeat<int>(-1, 13).ToArray<int>();
        TransAccessoryIndex = Enumerable.Repeat<int>(-1, 21).ToArray<int>();
        MasteredWeapons = new List<int>();
        MasteredAccessories = new List<int>();

        GetWeapon(startingWeapon);
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
    public bool HasWeapon(int weaponIndex)
    {
        return TransWeaponIndex[weaponIndex] >= 0;
    }
    public bool HasAcc(int accIndex)
    {
        return TransAccessoryIndex[accIndex] >= 0;
    }
    //ToDo: SkillFiringSystem이랑 연계 할 함수
    public void GetWeapon(int weaponIndex)
    {
        GameManager.instance.weaponGetTime[weaponIndex] = GameManager.instance.gameTime;
        TransWeaponIndex[weaponIndex] = Weapons.Count;
        Weapon newWeapon = (skillFiringSystem.weaponPrefabs[weaponIndex]).GetComponent<Weapon>();
        newWeapon.WeaponDefalutSetting(weaponIndex);
        Weapons.Add(newWeapon);
        GameManager.instance.WeaponSlot.GetComponent<SlotUI>().AddSlot(weaponIndex, 0);

        processWeaponSub(weaponIndex, newWeapon);
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
            case 9: // peachOne
                weapon.GetComponent<Peachone>().SpawnBlueBird(skillFiringSystem.Birds[0]);
                break;
            case 10: // ebonyWings
                weapon.GetComponent<EbonyWings>().SpawnBlackBird(skillFiringSystem.Birds[1]);
                break;
        }
    }
}
