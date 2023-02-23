using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFiringSystem : MonoBehaviour
{
    private float totalDamage;
    private float totalProjectileSpeed;
    private float totalDuration;
    private float totalAttackRange;
    private float totalCooldown;
    private int totalNumberOfProjectiles;
    private Vector3 direction;

    public GameObject weapon;

    float timer = 0;
    void Update()
    {
        Attack();
    }
    private void Attack()
    {
        switch (weapon.GetComponent<Weapon>().WeaponIndex)
        {
            case 0:     // Whip
                break;
            case 1:     // MagicWand
                break;
            case 2:     // Knife
                FireKnife(weapon.GetComponent<Weapon>().WeaponIndex);
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
        float timediff = totalCooldown;
        timer += Time.deltaTime;
        if (timer > timediff)
        {
            for (int i=0; i<= totalNumberOfProjectiles; i++)
            {
                GameObject newobs = Instantiate(GameManager.instance.pool.monsterPrefabs[index]);
                newobs.transform.position = GameManager.instance.player.transform.position;
                newobs.transform.parent = transform;
                newobs.GetComponent<Weapon>().Shoot(totalProjectileSpeed, GameManager.instance.player.GetComponent<PlayerMovement>().Movement);
                timer = 0;
                Destroy(newobs, totalDuration);  //지속 시간 지나면 삭제
            }
        }
    }
    //아래 계산을 한번에 하기
    //레벨업 할때마다 갱신하는 것으로 변경
    private void AttackCalculation()
    {
        DamageCalculation();
        ProjectileSpeedCalculation();
        DurationCalculation();
        AttackRangeCalculation();
        CooldownCalculation();
        CalculateNumberOfProjectiles();
    }
    private void DamageCalculation()
    {
        totalDamage = weapon.GetComponent<Weapon>().Damage * (1 + GameManager.instance.player.GetComponent<Character>().Damage / 100);
    }
    private void ProjectileSpeedCalculation()
    {
        totalProjectileSpeed = weapon.GetComponent<Weapon>().ProjectileSpeed * (1 + GameManager.instance.player.GetComponent<Character>().ProjectileSpeed / 100);
    }
    private void DurationCalculation()
    {
        totalDuration = weapon.GetComponent<Weapon>().Duration * (1 + GameManager.instance.player.GetComponent<Character>().Duration / 100);
    }
    private void AttackRangeCalculation()
    {
        totalAttackRange = weapon.GetComponent<Weapon>().AttackRange * (1 + GameManager.instance.player.GetComponent<Character>().AttackRange / 100);
    }
    private void CooldownCalculation()
    {
        totalCooldown = weapon.GetComponent<Weapon>().Cooldown * (1 + GameManager.instance.player.GetComponent<Character>().Cooldown / 100);
    }
    private void CalculateNumberOfProjectiles()
    {
        totalNumberOfProjectiles = weapon.GetComponent<Weapon>().NumberOfProjectiles + GameManager.instance.player.GetComponent<Character>().NumberOfProjectiles;
    }
}