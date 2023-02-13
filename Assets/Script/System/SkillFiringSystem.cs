using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFiringSystem : MonoBehaviour
{
    private int damage;              //피해량
    private int projectileSpeed;     //투사체 속도
    private int duration;            //지속 시간
    private int attackRange;         //공격범위
    private int cooldown;            //쿨타임
    private int numberOfProjectiles; //투사체 수

    public GameObject weapon;    //무기 가져오기

    float timer = 0;    //시간
    void Update()
    {
        Attack();
    }

    //공격하기
    private void Attack() 
    {
        AttackCalculation();    //공격 관련 계산
        for (int i = 0; i <= numberOfProjectiles; i++)  //투사체 수만큼 발사하기
        {
            FireWeapon();
        }
    }
    //무기 발사
    //ToDo: attackRange을 적용하기
    private void FireWeapon()
    {
        float timediff = cooldown;  //쿨타임
        timer += Time.deltaTime;    //시간 갱신
        if (timer > timediff)   //쿨타임 넘을 시
        {
            GameObject newobs = Instantiate(weapon);  //무기 로드
            newobs.transform.position = GameManager.instance.player.transform.position;  //캐릭터 위치에 생성
            newobs.transform.parent = transform;
            newobs.GetComponent<Weapon>().Shoot(projectileSpeed);  //오른쪽 벡터로 날아감
            timer = 0;  //시간 초기화
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
    //데미지 계산
    private void DamageCalculation()
    {
        damage = weapon.GetComponent<Weapon>().Damage * (1 + GameManager.instance.player.GetComponent<Character>().Damage / 100);
    }
    //투사체 속도 계산
    private void ProjectileSpeedCalculation()
    {
        projectileSpeed = weapon.GetComponent<Weapon>().ProjectileSpeed * (1 + GameManager.instance.player.GetComponent<Character>().ProjectileSpeed / 100);
    }
    //지속시간 계산
    private void DurationCalculation()
    {
        duration = weapon.GetComponent<Weapon>().Duration * (1 + GameManager.instance.player.GetComponent<Character>().Duration / 100);
    }
    //공격범위 계산
    private void AttackRangeCalculation()
    {
        attackRange = weapon.GetComponent<Weapon>().AttackRange * (1 + GameManager.instance.player.GetComponent<Character>().AttackRange / 100);
    }
    //쿨타임 계산
    private void CooldownCalculation()
    {
        cooldown = weapon.GetComponent<Weapon>().Cooldown * (1 + GameManager.instance.player.GetComponent<Character>().Cooldown / 100);
    }
    //투사체 수 계산
    private void CalculateNumberOfProjectiles()
    {
        numberOfProjectiles = weapon.GetComponent<Weapon>().NumberOfProjectiles + GameManager.instance.player.GetComponent<Character>().NumberOfProjectiles;
    }
}