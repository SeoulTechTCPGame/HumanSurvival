using System;

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
        }
        foreach(Weapon weapon in GameManager.instance.EquipManageSys.Weapons)
        {
            weapon.AttackCalculation();
        }
        Evolution();
    }
    private void Evolution()
    {
        if (AccessoryLevel != 1) // 해당 악세서리를 처음 획득했을 때만 진입
            return;

        var equipManageSys = GameManager.instance.EquipManageSys;
        int evoPairWeaponIndex = EquipmentData.EvoAccNeedWeaponIndex[AccessoryIndex];
        if (equipManageSys.HasWeapon(evoPairWeaponIndex))
        {
            var pairWeapon = equipManageSys.Weapons[equipManageSys.TransWeaponIndex[evoPairWeaponIndex]];
            if(pairWeapon.IsMaster())
            {
                pairWeapon.BEvolution = true;
                pairWeapon.EvolutionProcess();
            }
        }
    }
}