using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFiringSystem : MonoBehaviour
{
    public GameObject[] weaponPrefabs; //무기 프리팹
    public GameObject[] evolutionWeaponPrefabs; //진화 무기 프리팹
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
                    GameManager.instance.player.GetComponent<Character>().Weapons[GameManager.instance.player.GetComponent<Character>().TransWeaponIndex[1]].GetComponent<MagicWand>().FireMagicWand(weaponPrefabs[1]);
                }
                else
                {
                    GameManager.instance.player.GetComponent<Character>().Weapons[GameManager.instance.player.GetComponent<Character>().TransWeaponIndex[1]].GetComponent<MagicWand>().FireMagicWand(evolutionWeaponPrefabs[1]);
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
                break;
            case 10:    // EbonyWings
                break;
            case 11:    // Runetracer
                break;
            case 12:   // LightningRing
                break;
        }
    }
    
}