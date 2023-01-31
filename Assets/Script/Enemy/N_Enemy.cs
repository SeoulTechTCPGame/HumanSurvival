using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_Enemy : MonoBehaviour
{
    public float speed;
    public float health = 0.5f;
    public float maxHealth = 0.5f;
    public RuntimeAnimatorController[] animcon;
    public Rigidbody2D target;

    bool isLive ;

    private float damage = 1f;

    Rigidbody2D rb;
    SpriteRenderer spriter;
    Animator anim;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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

        //Ȱ��ȭ �ɶ� isLive true, health �ʱ�ȭ
        isLive = true;
        health = maxHealth;
    }
   
    public void Init(SpawnData data)  //������ ���� ������ ���� �Լ�s
    {
        anim.runtimeAnimatorController = animcon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Weapon"){
            if(health > damage){
                health -= damage;
            }
            else{
                gameObject.SetActive(false);
                Destroy(other.gameObject);
            }
        }
    }
}
