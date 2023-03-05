using System.Collections;
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

    void Awake(){ skillFiringSystem = GameObject.Find("SkillFiringSystem").GetComponent<SkillFiringSystem>(); }

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
        TransWeaponIndex[weaponIndex] = Weapons.Count;
        Weapon newWeapon = (skillFiringSystem.weaponPrefabs[weaponIndex]).GetComponent<Weapon>();
        newWeapon.WeaponDefalutSetting(weaponIndex);
        Weapons.Add(newWeapon);
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
    }
    public void UpgradeAccessory(int accessoryIndex)
    {
        Accessories[TransAccessoryIndex[accessoryIndex]].Upgrade();
        if (Accessories[TransAccessoryIndex[accessoryIndex]].IsMaster())
        {
            MasteredAccessories.Add(accessoryIndex);
        }
    }
}
