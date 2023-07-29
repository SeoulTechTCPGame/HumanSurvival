using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour,IDamageable
{
    public EnemyScriptableObject EnemyData;
    public Rigidbody2D Target;
    
    private float mHealth;
    private bool mbLive;
    private int mLevel;
    private int mKnockbackpower = 0;
    private Character mTargetCharacter;
    private GameObject mTargetGameObject;
    private Vector2 mFixedTargetDirection;
    private Vector2 mDirection;
    private Rigidbody2D mRb;
    private Collider2D mColl;
    private SpriteRenderer mSpriter;
    private Animator mAnim;
    private WaitForFixedUpdate mWait;

    private void Awake()
    {
        mRb = GetComponent<Rigidbody2D>();
        mSpriter = GetComponent<SpriteRenderer>();
        mAnim = GetComponent<Animator>();
        mColl = GetComponent<Collider2D>();
        mWait = new WaitForFixedUpdate();
    }
    private void FixedUpdate()
    {
        //몬스터가 살아 있을 때만 움직이도록 
        if (!mbLive||mAnim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        if (EnemyData.SpriteType == 4)
        {
            mDirection = (mFixedTargetDirection - mRb.position).normalized;
            Vector2 nextVec = 0.01f * EnemyData.Speed * GameManager.instance.CharacterStats[(int)Enums.EStat.Curse] * Time.fixedDeltaTime * mDirection;
            mRb.position += nextVec;
        }
        else
        {
            mDirection = (Target.position - mRb.position).normalized;
            Vector2 nextVec = 0.01f * EnemyData.Speed * GameManager.instance.CharacterStats[(int)Enums.EStat.Curse] * Time.fixedDeltaTime * mDirection;

            //플레이어의 키입력 값을 더한 이동=몬스터의 방향 값을 더한 이동
            mRb.MovePosition(mRb.position + nextVec);
        }
        //물리 속도가 이동에 영향을 주지 않도록 속도 제거
        mRb.velocity = Vector2.zero;
        //플레이어와 일정 거리 이상 떨어지면 다시 리스폰하기
        if (Vector3.Distance(Target.position, mRb.position) > 30)
        {
            mRb.position = Target.position + mDirection * 20;
        }
    }
    private void LateUpdate()
    {
        //타겟의 x축과 비교하여 sprite flip  
        mSpriter.flipX = Target.position.x < mRb.position.x;
    }
    private void OnEnable()
    {
        //prefeb은 scene의 object에 접근할 수 없다=> 생성될 때마다 변수를 초기화하기
        mTargetGameObject = GameManager.instance.Player.gameObject;
        Target = mTargetGameObject.transform.GetComponent<Rigidbody2D>();

        //bat bevy target direction
        mFixedTargetDirection = Target.position;
        //활성화 될때 변수 초기화
        mbLive = true;
        mLevel = EnemyData.Level;
        mHealth = EnemyData.MaxHP*mLevel* GameManager.instance.CharacterStats[(int)Enums.EStat.Curse];
        mColl.enabled=true;
        mRb.simulated = true;
        mSpriter.sortingOrder = 2;
        mAnim.SetBool("Dead", false);  //TODO: Fix code location
    }
    private void OnCollisionStay2D(Collision2D col)
    {
     
        if (col.gameObject ==mTargetGameObject) { 
            Attack();
        }
    }
    public void InitEnemy(EnemyScriptableObject data)  //각각의 몬스터 데이터 설정 함수
    {
        EnemyData = data;
    }
    public void TakeDamage(float damage, int weaponIndex)
    {
        mHealth -= damage;
        GameManager.instance.WeaponDamage[weaponIndex] += damage;

        if (mHealth > 0)
        {
            StartCoroutine(KnockBack());
            mAnim.SetTrigger("Hit");
            Debug.Log("Hit");
            if(weaponIndex == 6)
            {
                if(mKnockbackpower < 3)
                {
                    mKnockbackpower++;
                }
            }
        }
        else
        {
            mbLive = false;
            mColl.enabled = false;
            mRb.simulated = false;
            mSpriter.sortingOrder = 1;
            mAnim.SetBool("Dead", true);
            GameManager.instance.Kill++;
            Dead();
            GameManager.instance.KillCount[weaponIndex]++;
        }
    }
    private IEnumerator KnockBack()
    {
        yield return mWait;
        Vector3 playerPos = GameManager.instance.Player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        mRb.AddForce(dirVec.normalized * (3 + mKnockbackpower), ForceMode2D.Impulse);
    }
    private void Attack()
    {
        if (mTargetCharacter == null)
        {
            mTargetCharacter = Target.GetComponent<Character>();
        }
        mTargetCharacter.TakeDamage(EnemyData.Power, 0);
    }  
    private void Dead()
    {
        //경험치 drop
        gameObject.GetComponent<DropSystem>().OnDrop(mRb.transform.position);
        gameObject.SetActive(false);

    }
}