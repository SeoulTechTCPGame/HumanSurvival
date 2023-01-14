using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //캐릭터의 스탯지정
    //예시를 위해 값은 무작위로 넣음
    private int damage = 10;              //피해량
    private int projectileSpeed = 1;     //투사체 속도
    private int duration = 3;            //지속 시간
    private int attackRange = 1;         //공격범위
    private int cooldown = 5;            //쿨타임
    private int numberOfProjectiles = 1;     //투사체 수

    //캐릭터 오브젝트 가져오기
    public GameObject character;
    //Get,Set함수 자동 구현
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public int ProjectileSpeed
    {
        get { return projectileSpeed; }
        set { projectileSpeed = value; }
    }
    public int Duration
    {
        get { return duration; }
        set { duration = value; }
    }
    public int AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }
    public int Cooldown
    {
        get { return cooldown; }
        set { cooldown = value; }
    }
    public int NumberOfProjectiles
    {
        get { return numberOfProjectiles; }
        set { numberOfProjectiles = value; }
    }
}
