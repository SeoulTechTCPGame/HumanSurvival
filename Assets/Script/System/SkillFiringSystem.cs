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

    public GameObject character;    //캐릭터 스탯과 위치 가져오기
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

        GameObject monster = GameObject.FindWithTag("Monster");

        if (timer > timediff)   //쿨타임 넘을 시
        {
            GameObject newobs = Instantiate(weapon);  //무기 로드
            newobs.transform.position = character.transform.position;  //캐릭터 위치에 생성
            newobs.GetComponent<Weapon>().Shoot(projectileSpeed);  //오른쪽 벡터로 날아감
            if (OnTriggerEnter2D(weapon.GetComponent<Collider2D>()))    //무기가 몬스터와 부딪힘 감지
            {
                monster.GetComponent<Monster>().Health -= damage;   //딜 계산
                Destroy(monster, 0);    //몬스터 삭제
                if (monster.GetComponent<Monster>().Health <= 0)    //몬스터가 죽는다면
                {
                    GameObject obj = Resources.Load<GameObject>("Object/Capsule");  //임펙트 등장
                    Destroy(obj, 1);    //임펙트 등장 시간
                }
            }
            timer = 0;  //시간 초기화
            Destroy(newobs, duration);  //지속 시간 지나면 삭제
        }
    }
    private bool OnTriggerEnter2D(Collider2D weapon)
    //rigidBody가 무언가와 충돌할때 호출되는 함수로 Collider2D other로 부딪힌 객체를 받아옵니다.
    {
        if (weapon.gameObject.tag.Equals("Monster")) //부딪힌 객체의 태그를 비교해서 적인지 판단합니다.
        { return true; }
        else { return false; }
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
        damage = weapon.GetComponent<Weapon>().Damage * (1 + character.GetComponent<Character>().Damage / 100);
    }
    //투사체 속도 계산
    private void ProjectileSpeedCalculation()
    {
        projectileSpeed = weapon.GetComponent<Weapon>().ProjectileSpeed * (1 + character.GetComponent<Character>().ProjectileSpeed / 100);
    }
    //지속시간 계산
    private void DurationCalculation()
    {
        duration = weapon.GetComponent<Weapon>().Duration * (1 + character.GetComponent<Character>().Duration / 100);
    }
    //공격범위 계산
    private void AttackRangeCalculation()
    {
        attackRange = weapon.GetComponent<Weapon>().AttackRange * (1 + character.GetComponent<Character>().AttackRange / 100);
    }
    //쿨타임 계산
    private void CooldownCalculation()
    {
        cooldown = weapon.GetComponent<Weapon>().Cooldown * (1 + character.GetComponent<Character>().Cooldown / 100);
    }
    //투사체 수 계산
    private void CalculateNumberOfProjectiles()
    {
        numberOfProjectiles = weapon.GetComponent<Weapon>().NumberOfProjectiles + character.GetComponent<Character>().NumberOfProjectiles;
    }
}