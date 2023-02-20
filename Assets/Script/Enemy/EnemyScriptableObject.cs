using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public int spriteType;
    public float spawnTime;
    public int speed; //몬스터의 이동 속도
    public float power; //몬스터의 공격력
    public float knockback; //몬스터 피격 시 넉백(밀리는) 정도에 대한 수치
    public float maxKnockback; //몬스터 넉백 정도는 증가할 수 있는데 그 정도의 상한
    public float deathKB; //몬스터 사망 시 넉백(밀리는) 정도에 대한 수치
    public int xp; //드랍되는 경험치의 양(수치) 이다.
    public int end; //레벨 업 상한선
    public int level; //초기 레벨 수치
    public float maxHP;
}