using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animcon;
    public Rigidbody2D target;
    //GameObject targetGameobject;

    bool isLive ;

    Rigidbody2D rb;
    Collider2D coll;
    SpriteRenderer spriter;
    Animator anim;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }
    void FixedUpdate()
    {
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
        anim.SetBool("Dead", false);

    }

    public void Init(SpawnData data)  //������ ���� ������ ���� �Լ�
    {
        anim.runtimeAnimatorController = animcon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
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
    void Dead()
    {
        // object ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}