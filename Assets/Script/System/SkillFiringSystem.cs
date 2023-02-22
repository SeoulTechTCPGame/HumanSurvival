using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFiringSystem : MonoBehaviour
{
    private int damage;
    private int projectileSpeed;
    private int duration;
    private int attackRange;
    private int cooldown;
    private int numberOfProjectiles;
    private Vector3 direction;

    public GameObject weapon;

    float timer = 0;
    void Update()
    {
        Attack();
    }
    private void Attack()
    {
        AttackCalculation();
        for (int i = 0; i <= numberOfProjectiles; i++)
        {
            FireWeapon();
        }
    }
    //ToDo: attackRange을 적용하기
    private void FireWeapon()
    {
        float timediff = cooldown;
        timer += Time.deltaTime;
        if (timer > timediff)
        {
            GameObject newobs = Instantiate(weapon);
            newobs.transform.position = GameManager.instance.player.transform.position;
            newobs.transform.parent = transform;
            newobs.GetComponent<Weapon>().Shoot(projectileSpeed, direction);  //오른쪽 벡터로 날아감
            timer = 0;
            Destroy(newobs, duration);  //지속 시간 지나면 삭제
        }
    }
    //아래 계산을 한번에 하기
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
        damage = weapon.GetComponent<Weapon>().Damage * (1 + GameManager.instance.player.GetComponent<Character>().Damage / 100);
    }
    private void ProjectileSpeedCalculation()
    {
        projectileSpeed = weapon.GetComponent<Weapon>().ProjectileSpeed * (1 + GameManager.instance.player.GetComponent<Character>().ProjectileSpeed / 100);
    }
    private void DurationCalculation()
    {
        duration = weapon.GetComponent<Weapon>().Duration * (1 + GameManager.instance.player.GetComponent<Character>().Duration / 100);
    }
    private void AttackRangeCalculation()
    {
        attackRange = weapon.GetComponent<Weapon>().AttackRange * (1 + GameManager.instance.player.GetComponent<Character>().AttackRange / 100);
    }
    private void CooldownCalculation()
    {
        cooldown = weapon.GetComponent<Weapon>().Cooldown * (1 + GameManager.instance.player.GetComponent<Character>().Cooldown / 100);
    }
    private void CalculateNumberOfProjectiles()
    {
        numberOfProjectiles = weapon.GetComponent<Weapon>().NumberOfProjectiles + GameManager.instance.player.GetComponent<Character>().NumberOfProjectiles;
    }
}