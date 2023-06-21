using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("EnemyName")]
    [SerializeField] string mEnemyName;
    public string EnemyName{get=>mEnemyName; private set => mEnemyName=value;}
    [SerializeField] int mSpriteType;
    public int SpriteType{get=>mSpriteType; private set => mSpriteType=value;}

    [Header("EnemyStat")]
    [SerializeField] int mSpeed; //몬스터의 이동 속도
    public int Speed { get => mSpeed; private set => mSpeed = value; }
    [SerializeField] float mPower; //몬스터의 공격력
    public float Power { get => mPower; private set => mPower = value; }
    [SerializeField] float mKnockback; //몬스터 피격 시 넉백(밀리는) 정도에 대한 수치
    public float Knockback { get => mKnockback; private set => mKnockback = value; }
    [SerializeField] float mMaxKnockback; //몬스터 넉백 정도는 증가할 수 있는데 그 정도의 상한
    public float MaxKnockback { get => mMaxKnockback; private set => mMaxKnockback = value; }
    [SerializeField] float mDeathKB; //몬스터 사망 시 넉백(밀리는) 정도에 대한 수치
    public float DeathKB { get => mDeathKB; private set => mDeathKB = value; }
    [SerializeField] int mXp; //드랍되는 경험치의 양(수치) 이다.
    public int Xp { get => mXp; private set => mXp = value; }
    [SerializeField] int mEnd; //레벨 업 상한선
    public int End { get => mEnd; private set => mEnd = value; }
    [SerializeField] int mLevel; //초기 레벨 수치
    public int Level { get => mLevel; private set => mLevel = value; }
    [SerializeField] float mMaxHP;
    public float MaxHP { get => mMaxHP; private set => mMaxHP = value; }
}