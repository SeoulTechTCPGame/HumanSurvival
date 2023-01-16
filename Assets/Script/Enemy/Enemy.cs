using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Transform targetDestination;
    GameObject targetGameobject;
    //Character targetCharacter;
    [SerializeField] float speed;

    bool isLive = true;

    Rigidbody2D rb;
    SpriteRenderer spriter;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }
    public void SetTarget(GameObject target)
    {
        targetGameobject = target;
        targetDestination = target.transform;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLive) return;
        Vector2 direction = (targetDestination.position - transform.position).normalized;
        
        //�÷��̾��� Ű�Է� ���� ���� �̵�=������ ���� ���� ���� �̵�
        Vector2 nextVec = direction * speed * Time.fixedDeltaTime; ;
        rb.MovePosition(rb.position + nextVec);
       rb.velocity = Vector2.zero;
    }
    private void LateUpdate()
    {
        spriter.flipX = targetDestination.position.x < rb.position.x;
    }
    private void OnEnable()
    {
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject== targetGameobject)
        {
            Attack();
        }
    }
    private void Attack()
    {
        Debug.Log("���� ���ϰ� ����!");
    }
}