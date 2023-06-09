using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,IDamageable
{
    public EnemyScriptableObject enemyData;
    public Rigidbody2D target;
    
    float health;
    bool isLive ;
    int level;
    int knockbackpower = 0;

    Character targetCharacter;
    GameObject targetGameObject;

    Vector2 fixedTargetDirection;
    Vector2 direction;
    Rigidbody2D rb;
    Collider2D coll;
    SpriteRenderer spriter;
    Animator anim;
    WaitForFixedUpdate wait;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        wait = new WaitForFixedUpdate();
    }
    void FixedUpdate()
    {
        //몬스터가 살아 있을 때만 움직이도록 
        if (!isLive||anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        if (enemyData.SpriteType == 4)
        {
            direction = (fixedTargetDirection - rb.position).normalized;
            Vector2 nextVec = 0.01f * enemyData.Speed * Time.fixedDeltaTime * direction;
            rb.position += nextVec;
        }
        else
        {
            direction = (target.position - rb.position).normalized;
            Vector2 nextVec = 0.01f * enemyData.Speed * Time.fixedDeltaTime * direction;

            //플레이어의 키입력 값을 더한 이동=몬스터의 방향 값을 더한 이동
            rb.MovePosition(rb.position + nextVec);
        }
        //물리 속도가 이동에 영향을 주지 않도록 속도 제거
        rb.velocity = Vector2.zero;
        
    }
    private void LateUpdate()
    {
        //타겟의 x축과 비교하여 sprite flip  
        spriter.flipX = target.position.x < rb.position.x;
    }
    private void OnEnable()
    {
        //prefeb은 scene의 object에 접근할 수 없다=> 생성될 때마다 변수를 초기화하기
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        targetGameObject = GameManager.instance.player.gameObject;
        //bat bevy target direction
        fixedTargetDirection = target.position;
        //활성화 될때 변수 초기화
        isLive = true;
        level = enemyData.Level;
        health = enemyData.MaxHP*level;
        coll.enabled=true;
        rb.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);  //TODO: Fix code location
    }

    public void InitEnemy(EnemyScriptableObject data)  //각각의 몬스터 데이터 설정 함수
    {
        enemyData = data;
    }
    private void OnCollisionStay2D(Collision2D col)
    {
     
        if (col.gameObject ==targetGameObject) { 
            Attack();
        }
    }
    void Attack() {
        if (targetCharacter == null)
        {
            targetCharacter = target.GetComponent<Character>();
        }
        targetCharacter.TakeDamage(enemyData.power, 0);
    }  
    void Dead()
    {
        //경험치 drop
        gameObject.GetComponent<DropSystem>().OnDrop(rb.transform.position);
        gameObject.SetActive(false);

    }
  
    public void TakeDamage(float damage, int weaponIndex)
    {
        health -= damage;
        GameManager.instance.weaponDamage[weaponIndex] += damage;

        if (health > 0)
        {
            StartCoroutine(KnockBack());
            anim.SetTrigger("Hit");
            Debug.Log("Hit");
            if(weaponIndex == 6)
            {
                if(knockbackpower < 3)
                {
                    knockbackpower++;
                }
            }
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rb.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            Dead();
            GameManager.instance.killCount[weaponIndex]++;
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rb.AddForce(dirVec.normalized * (3 + knockbackpower), ForceMode2D.Impulse);
    }
}
