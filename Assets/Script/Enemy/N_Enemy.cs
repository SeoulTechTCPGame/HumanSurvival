using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animcon;
    public Rigidbody2D target;

    bool isLive ;

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
        //몬스터가 살아 있을 때만 움직이도록 
        if (!isLive) return;

        Vector2 direction = (target.position - rb.position).normalized;
        Vector2 nextVec = direction * speed * Time.fixedDeltaTime; ;

        //플레이어의 키입력 값을 더한 이동=몬스터의 방향 값을 더한 이동
        rb.MovePosition(rb.position + nextVec);

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

        //활성화 될때 isLive true, health 초기화
        isLive = true;
        health = maxHealth;
    }
   
    public void Init(SpawnData data)  //각각의 몬스터 데이터 설정 함수s
    {
        anim.runtimeAnimatorController = animcon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
}
