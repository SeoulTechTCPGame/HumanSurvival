using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFiringSystem : MonoBehaviour
{
    public GameObject[] weaponPrefabs; //무기 프리팹
    float timer = 0;

    void Update()
    {
        foreach (var weapon in GameManager.instance.player.GetComponent<Character>().Weapons)
        {
            Attack(weapon.WeaponIndex);
        }
    }
    private void Attack(int index)
    {
        switch (index)
        {
            case 0:     // Whip
                break;
            case 1:     // MagicWand
                break;
            case 2:     // Knife
                FireKnife(index);
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
                break;
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
    //ToDo: totalAttackRange 적용하기
    private void FireKnife(int index)
    {
        float timediff = weaponPrefabs[index].GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Cooldown)];
        timer += Time.deltaTime;
        if (timer > timediff)
        {
            for (int i=0; i<= weaponPrefabs[index].GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Amount)]; i++)
            {
                GameObject newobs = Instantiate(weaponPrefabs[index]);   //weapon의 index와 monsterPool의 index는 값게 설정
                newobs.transform.position = GameManager.instance.player.transform.position;
                newobs.transform.parent = transform;
                newobs.GetComponent<Weapon>().Shoot(weaponPrefabs[index].GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.ProjectileSpeed)], GameManager.instance.player.GetComponent<PlayerMovement>().Movement);
                timer = 0;
                Destroy(newobs, weaponPrefabs[index].GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Duration)]);  //지속 시간 지나면 삭제
            }
        }
    }
}