using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFiringSystem : MonoBehaviour
{
    //ToDO: 캐릭터의 스탯이 없어, 직접 변수로 가져오는 것으로 설정
    //캐릭터의 스탯을 가져오기
    public int damage = 10;              //피해량
    public int projectileSpeed = 1;     //투사체 속도
    public int duration = 3;            //지속 시간
    public int attackRange = 1;         //공격범위
    public int cooldown = 5;            //쿨타임
    public int numberOfProjectiles = 1; //투사체 수
    //몬스터 태그 가져오기
    public GameObject monster;
    //ToDo: 무기 리스트 가져오기로 바꾸기
    //무기 가져오기
    public TestWeapon weapon;
    //임팩트 효과 가져오기
    public GameObject impact;

    private void Start()
    {
        AttackCalculation();
    }
    private void Update()
    {
        Attack();
    }
    //공격하기
    private void Attack() { }
    private void Impact() {
        GameObject obj = Resources.Load<GameObject>("Object/Capsule");
    }
    //아래 계산을 한번에 하기
    private void AttackCalculation() {
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
        damage = weapon.Damage * (1 + damage / 100);
    }
    //투사체 속도 계산
    private void ProjectileSpeedCalculation()
    {
        //ToDo: 계산식 바꾸기
        projectileSpeed = weapon.ProjectileSpeed * (1 + projectileSpeed / 100);
    }
    //지속시간 계산
    private void DurationCalculation()
    {
        //ToDo: 계산식 바꾸기
        duration = weapon.Duration * (1 + duration / 100);
    }
    //공격범위 계산
    private void AttackRangeCalculation()
    {
        //ToDo: 계산식 바꾸기
        attackRange = weapon.AttackRange * (1 + attackRange / 100);
    }
    //쿨타임 계산
    private void CooldownCalculation()
    {
        //ToDo: 계산식 바꾸기
        cooldown = weapon.Cooldown * (1 + cooldown / 100);
    }
    //투사체 수 계산
    private void CalculateNumberOfProjectiles()
    {
        numberOfProjectiles = weapon.NumberOfProjectiles + numberOfProjectiles;
    }
}