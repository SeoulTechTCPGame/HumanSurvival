using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour,IDamageable
{
    public EnemyScriptableObject EnemyData;
    public Rigidbody2D Target;
    public bool BDropTB = false;

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
    private float mSpawnTime;

    private void Awake()
    {
        mRb = GetComponent<Rigidbody2D>();
        mSpriter = GetComponent<SpriteRenderer>();
        mAnim = GetComponent<Animator>();
        mColl = GetComponent<Collider2D>();
        mWait = new WaitForFixedUpdate();
    }
    private void Start()
    {
        mSpawnTime = GameManager.instance.GameTime;

    }
    private void FixedUpdate()
    {
        //몬스터가 살아 있을 때만 움직이도록 
        if (!mbLive||mAnim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        if (EnemyData.SpriteType == 4)
        {
            mDirection = (mFixedTargetDirection - mRb.position).normalized;
            Vector2 nextVec = 0.01f * EnemyData.Speed * GameManager.instance.CharacterStats[(int)Enums.EStat.Curse] * Time.fixedDeltaTime * mDirection * GameManager.instance.EnemyTimeScale;
            mRb.position += nextVec;
        }
        else
        {
            mDirection = (Target.position - mRb.position).normalized;
            Vector2 nextVec = 0.01f * EnemyData.Speed * GameManager.instance.CharacterStats[(int)Enums.EStat.Curse] * Time.fixedDeltaTime * mDirection * GameManager.instance.EnemyTimeScale;

            //플레이어의 키입력 값을 더한 이동=몬스터의 방향 값을 더한 이동
            mRb.MovePosition(mRb.position + nextVec);
        }
        //물리 속도가 이동에 영향을 주지 않도록 속도 제거
        mRb.velocity = Vector2.zero;
        //플레이어와 일정 거리 이상 떨어지면 없애기
        if (Vector3.Distance(Target.position, mRb.position) > 50)
        {
            if(EnemyData.name == "EliteBat")
            {
                mRb.position = Target.position + mDirection * 20;
            }
            else
            {
                if (gameObject.TryGetComponent(out DropTB dt))
                {
                    BDropTB = true;
                }
                mbLive = false;
                mColl.enabled = false;
                mRb.simulated = false;
                mSpriter.sortingOrder = 1;
                mAnim.SetBool("Dead", true);
                gameObject.SetActive(false);
            }            
        }
        /* Todo: 바로 없어짐 왜?
         * if (EnemyData.EnemyName == "Flower" && GameManager.instance.GameTime > mSpawnTime + 14)
        {
            Debug.Log(mSpawnTime);
            mbLive = false;     
            mColl.enabled = false;
            mRb.simulated = false;
            mSpriter.sortingOrder = 1;
            mAnim.SetBool("Dead", true);
            gameObject.SetActive(false);
        }*/
    }
    private void LateUpdate()
    {
        //타겟의 x축과 비교하여 sprite flip  
        mSpriter.flipX = Target.position.x > mRb.position.x;
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
        mAnim.SetBool("Dead", false);
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
        if (weaponIndex >= 0 && weaponIndex < (int)Enums.EWeapon.WeaponCount)
        {
            GameManager.instance.WeaponDamage[weaponIndex] += damage;
        }
        if (mHealth > 0)
        {
            StartCoroutine(KnockBack());
            if(weaponIndex == (int)Enums.EWeapon.Garlic)
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
            if (weaponIndex >= 0 && weaponIndex < (int)Enums.EWeapon.WeaponCount)
            {
                GameManager.instance.KillCount[weaponIndex]++;
            }
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
        BDropTB = false;
        gameObject.GetComponent<DropSystem>().OnDrop(mRb.transform.position);
        gameObject.SetActive(false);
    }
}