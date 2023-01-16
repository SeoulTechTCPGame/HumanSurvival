using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 8f;
    private Vector2 clickTarget;
    private Vector2 relativePos;
    private Vector2 movement;
    private float horizontal;
    private float vertical;
    bool moving;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriter;
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        clickTarget = transform.position;
    }
    void Update()
    {
        //키 input 코드

        movement.x = Input.GetAxisRaw("Horizontal"); //키 입력
        movement.y = Input.GetAxisRaw("Vertical"); 
        //animator.SetFloat("Horizontal", movement.x);
        //animator.SetFloat("Vertical", movement.y);
        //animator.SetFloat("Speed", movement.sqrMagnitude); //성능 체크용
        if (horizontal != 0||vertical !=0||moving) animator.SetBool("Moving", true);
        else animator.SetBool("Moving", false);
        // 마우스 click 코드
        if (Input.GetMouseButtonDown(0))
        {
            clickTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;
        }
        
        relativePos = new Vector2(
             clickTarget.x - rb.position.x,
             clickTarget.y - rb.position.y);
    }
    private void FixedUpdate()//물리 계산 할 때 사용
    {
        //movement 코드
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);//이전 한 프레임 수행 시간

        //click 시 movement 코드
        if (moving && (Vector2)rb.position != clickTarget)
        {
            animator.SetBool("Moving", true);
            float step = moveSpeed * Time.fixedDeltaTime;
            rb.position = Vector2.MoveTowards(rb.position, clickTarget, step);
        }
        else
        {
            animator.SetBool("Moving", false);
            moving = false;
        }
        
       
    }
    private void LateUpdate()
    {
        
        if (horizontal != 0||relativePos.x<0)
        {
            spriter.flipX = horizontal < 0;
        }
    }
}

