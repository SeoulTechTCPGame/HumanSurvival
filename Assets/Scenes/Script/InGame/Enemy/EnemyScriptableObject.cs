using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [SerializeField]
    int spriteType;
    public int SpriteType{get=>spriteType; private set => spriteType=value;}
    [SerializeField]
    float spawnTime;
    public float SpawnTime { get => spawnTime; private set => spawnTime = value; }
    [SerializeField]
    public int speed; //몬스터의 이동 속도
    public int Speed { get => speed; private set => speed = value; }
    [SerializeField]
    public float power; //몬스터의 공격력
    public float Power { get => power; private set => power = value; }
    [SerializeField]
    public float knockback; //몬스터 피격 시 넉백(밀리는) 정도에 대한 수치
    public float Knockback { get => knockback; private set => knockback = value; }
    [SerializeField]
    public float maxKnockback; //몬스터 넉백 정도는 증가할 수 있는데 그 정도의 상한
    public float MaxKnockback { get => maxKnockback; private set => maxKnockback = value; }
    [SerializeField]
    public float deathKB; //몬스터 사망 시 넉백(밀리는) 정도에 대한 수치
    public float DeathKB { get => deathKB; private set => deathKB = value; }
    [SerializeField]
    public int xp; //드랍되는 경험치의 양(수치) 이다.
    public int Xp { get => xp; private set => xp = value; }
    [SerializeField]
    public int end; //레벨 업 상한선
    public int End { get => end; private set => end = value; }
    [SerializeField]
    public int level; //초기 레벨 수치
    public int Level { get => level; private set => level = value; }
    [SerializeField]
    public float maxHP;
    public float MaxHP { get => maxHP; private set => maxHP = value; }
}