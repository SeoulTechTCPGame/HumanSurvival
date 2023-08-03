using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoWeaponTest : MonoBehaviour
{
    public void LevelUpButton()
    {
        GameManager.instance.LevelUp();
        foreach(Weapon wea in GameManager.instance.EquipManageSys.Weapons)
        {
            Debug.Log(wea);
        }

        

    }
}
