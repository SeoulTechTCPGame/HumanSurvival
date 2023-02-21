using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public float WeaponDamage;   //무기 데미지 적용 시 삭제
    public RuntimeAnimatorController[] animcon;
    public Rigidbody2D target;

    bool isLive ;

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
        // 주석 깨짐 확인
        //���Ͱ� ��� ���� ���� �����̵��� 
        if (!isLive) return;

        Vector2 direction = (target.position - rb.position).normalized;
        Vector2 nextVec = direction * speed * Time.fixedDeltaTime; ;

        //�÷��̾��� Ű�Է� ���� ���� �̵�=������ ���� ���� ���� �̵�
        rb.MovePosition(rb.position + nextVec);

        //���� �ӵ��� �̵��� ������ ���� �ʵ��� �ӵ� ����
        rb.velocity = Vector2.zero;
    }
    private void LateUpdate()
    {
        //Ÿ���� x��� ���Ͽ� sprite flip 
        spriter.flipX = target.position.x < rb.position.x;
    }
    private void OnEnable()
    {
        //prefeb�� scene�� object�� ������ �� ����=> ������ ������ ������ �ʱ�ȭ�ϱ�
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();

        //Ȱ��ȭ �ɶ� ���� �ʱ�ȭ
        isLive = true;
        health = maxHealth;
        coll.enabled=true;
        rb.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);  //TODO: Fix code location
    }

    public void Init(SpawnData data)  //������ ���� ������ ���� �Լ�
    {
        anim.runtimeAnimatorController = animcon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
    /* ToDo: 코드 재활용
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isLive = false;
        coll.enabled = false;
        rb.simulated = false;
        spriter.sortingOrder = 1;
        anim.SetBool("Dead", true);
        Debug.Log("Hit");
        Dead();
    }
    */
    void Dead()
    {
        // object ��Ȱ��ȭ
        gameObject.SetActive(false);
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Weapon"){

            StartCoroutine(KnockBack());
            if(health > WeaponDamage){
                health -= WeaponDamage;
            }
            else{
                //ToDo: 충돌 오브젝트 비활성화 제어
                gameObject.SetActive(false);
                Destroy(other.gameObject);

                anim.SetTrigger("Hit");
            }
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rb.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }
}
