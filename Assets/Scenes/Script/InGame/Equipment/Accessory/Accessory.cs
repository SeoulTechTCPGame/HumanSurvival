using System;
using UnityEngine;

public class Accessory
{
    public int AccessoryIndex;
    public int AccessoryLevel;
    public int AccessoryMaxLevel;

    public Accessory(int accessoryIndex)
    {
        AccessoryIndex = accessoryIndex;
        AccessoryLevel = 0;
        AccessoryMaxLevel = EquipmentData.AccessoriesMaxLevel[accessoryIndex];
    }

    public bool IsMaster()
    {
        return AccessoryLevel == AccessoryMaxLevel;
    }
    public void Upgrade()
    {
        AccessoryLevel++;
        foreach ((var statIndex, var data) in EquipmentData.AccessoryUpgrade[AccessoryIndex][AccessoryLevel])
        {
            GameManager.instance.CharacterStats[statIndex] += data;
            if (statIndex == (int)Enums.Stat.Luck)
            {
                GameManager.instance.UpdateLuck(GameManager.instance.CharacterStats[statIndex]);
            }
        }
        evolution();
    }
    private void evolution()
    {
        if (AccessoryLevel != 1) // 해당 악세서리를 처음 획득했을 때만 진입
            return;

        var equipManageSys = GameManager.instance.equipManageSys;
        int evoPairWeaponIndex = EquipmentData.EvoAccNeedWeaponIndex[AccessoryIndex];
        if (evoPairWeaponIndex < 0)
            return;
        if (equipManageSys.HasWeapon(evoPairWeaponIndex))
        {
            var pairWeapon = equipManageSys.Weapons[equipManageSys.TransWeaponIndex[evoPairWeaponIndex]];
            if(pairWeapon.IsMaster())
            {
                pairWeapon.bEvolution = true;
                pairWeapon.EvolutionProcess();
            }
        }
    }
}
