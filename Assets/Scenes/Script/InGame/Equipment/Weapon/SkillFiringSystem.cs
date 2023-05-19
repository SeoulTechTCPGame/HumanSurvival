using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFiringSystem : MonoBehaviour
{
    public GameObject[] weaponPrefabs; //무기 프리팹
    public GameObject[] evolutionWeaponPrefabs; //진화 무기 프리팹
    public GameObject[] Birds; // peachone, EbonyWings, 둘의 진화체에 등장하는 새
    public GameObject[] Circles;
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
                    weapon.GetComponent<MagicWand>().FireMagicWand(weaponPrefabs[weapon.WeaponIndex]);
                }
                else
                {
                    weapon.GetComponent<MagicWand>().FireMagicWand(evolutionWeaponPrefabs[weapon.WeaponIndex]);
                }
                break;
            case 2:     // Knife
                if (!weapon.isEvoluction())
                {
                    weapon.GetComponent<Knife>().FireKnife(weaponPrefabs[weapon.WeaponIndex]);
                }
                else
                {
                    weapon.GetComponent<Knife>().FireKnife(evolutionWeaponPrefabs[weapon.WeaponIndex]);
                }
                break;
            case 3:     // Cross
                break;
            case 4:     //KingBible
                break;
            case 5:     // FireWand
                break;
            case 6:     // Garlic
                weapon.GetComponent<Gralic>().SpawnGralic(weaponPrefabs[weapon.WeaponIndex]); break;
            case 7:     // Peachone
                if (!weapon.isEvoluction())
                {
                    weapon.GetComponent<Peachone>().CreateCircle(weaponPrefabs[weapon.WeaponIndex], Circles[0], weapon);
                }
                else
                {
                    weapon.GetComponent<Peachone>().CreateEvoCircle(evolutionWeaponPrefabs[weapon.WeaponIndex], Circles[1], weapon);
                }
                break;
            case 8:    // EbonyWings
                if (!weapon.isEvoluction())
                {
                    weapon.GetComponent<EbonyWings>().CreateCircle(weaponPrefabs[weapon.WeaponIndex], Circles[0], weapon);
                }
                else
                {
                    weapon.GetComponent<EbonyWings>().CreateEvoCircle(evolutionWeaponPrefabs[weapon.WeaponIndex], Circles[1], weapon);
                }
                break;
            case 9:   // LightningRing
                break;
        }
    }
    
}