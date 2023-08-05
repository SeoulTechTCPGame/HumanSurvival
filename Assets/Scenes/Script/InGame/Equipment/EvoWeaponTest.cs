using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoWeaponTest : MonoBehaviour
{
    public void LevelUpButton()
    {
        foreach(Weapon wea in GameManager.instance.EquipManageSys.Weapons)
        {
            wea.Upgrade();
        }
    }
    public void SetWeaponAccessory()
    {
        // GameManager.instance.EquipManageSys.SetNewAccessory(2);

        // GameManager.instance.EquipManageSys.SetNewWeapon(1);
         GameManager.instance.EquipManageSys.SetNewAccessory(4);

        // GameManager.instance.EquipManageSys.SetNewWeapon(2);
         GameManager.instance.EquipManageSys.SetNewAccessory(6);

        // GameManager.instance.EquipManageSys.SetNewWeapon(3);
        // GameManager.instance.EquipManageSys.SetNewAccessory(11);

        // GameManager.instance.EquipManageSys.SetNewWeapon(4);
        // GameManager.instance.EquipManageSys.SetNewAccessory(7);

        // GameManager.instance.EquipManageSys.SetNewWeapon(5);
        // GameManager.instance.EquipManageSys.SetNewAccessory(0);

        // GameManager.instance.EquipManageSys.SetNewWeapon(6);
        // GameManager.instance.EquipManageSys.SetNewAccessory(3);

        //GameManager.instance.EquipManageSys.SetNewWeapon(7);
        //GameManager.instance.EquipManageSys.SetNewAccessory(-1);
        //GameManager.instance.EquipManageSys.SetNewWeapon(8);
        //GameManager.instance.EquipManageSys.SetNewAccessory(-1);

        // GameManager.instance.EquipManageSys.SetNewWeapon(9);
        // GameManager.instance.EquipManageSys.SetNewAccessory(8);
    }
}
