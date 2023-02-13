using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public float enemyDamage;
    public int exp;
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
        //몬스터가 살아 있을 때만 움직이도록 
        if (!isLive||anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

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
    void Dead()
    {
        // object 비활성화
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (!collision.CompareTag("Weapon")) return;
        health -= 10;
        //health -= collision.GetComponent<Weapon>().damage;
        StartCoroutine(KnockBack());
        if (health > 0)
        {
            anim.SetTrigger("Hit");
            Debug.Log("Hit");
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rb.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.exp+=exp;
            Dead();
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
