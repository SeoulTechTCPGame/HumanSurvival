using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFiringSystem : MonoBehaviour
{
    public GameObject[] weaponPrefabs; //무기 프리팹
    public GameObject[] evolutionWeaponPrefabs; //진화 무기 프리팹
    public GameObject[] WeaponSubPrefabs; // 무기 서브 프리팹 (peachone의 공전원 등)
    void Update()
    {
        foreach (var weapon in GameManager.instance.equipManageSys.Weapons)
        {
            Attack(weapon);
        }
    }
    private void Attack(Weapon weapon)
    {
        switch (weapon.WeaponIndex)
        {
            case 0:     // Whip
                break;
            case 1:     // MagicWand
                if (!weapon.isEvoluction())
                {
                    weapon.GetComponent<MagicWand>().FireMagicWand(weaponPrefabs[1]);
                }
                else
                {
                    weapon.GetComponent<MagicWand>().FireMagicWand(evolutionWeaponPrefabs[1]);
                }
                break;
            case 2:     // Knife
                if (!weapon.isEvoluction())
                {
                    weapon.GetComponent<Knife>().FireKnife(weaponPrefabs[2]);
                }
                else
                {
                    weapon.GetComponent<Knife>().FireKnife(evolutionWeaponPrefabs[2]);
                }
                break;
            case 3:     // Axe
                break;
            case 4:     // Cross
                break;
            case 5:     //KingBible
                break;
            case 6:     // FireWand
                break;
            case 7:     // Garlic
                weapon.GetComponent<Gralic>().SpawnGralic(weaponPrefabs[7]); break;
            case 8:     // SantaWater
                break;
            case 9:     // Peachone
                if (!weapon.isEvoluction())
                {
                    weapon.GetComponent<Peachone>().CreateCircle(weaponPrefabs[9], WeaponSubPrefabs[9], weapon);
                }
                else
                {
                    //weapon.GetComponent<Peachone>().CreateCircle(evolutionWeaponPrefabs[9], WeaponSubPrefabs[9]);
                }
                break;
            case 10:    // EbonyWings
                if (!weapon.isEvoluction())
                {
                    weapon.GetComponent<EbonyWings>().CreateCircle(weaponPrefabs[10], WeaponSubPrefabs[10], weapon);
                }
                else
                {
                    //weapon.GetComponent<Peachone>().CreateCircle(evolutionWeaponPrefabs[9], WeaponSubPrefabs[9]);
                }
                break;
            case 11:    // Runetracer
                break;
            case 12:   // LightningRing
                break;
        }
    }
    
}