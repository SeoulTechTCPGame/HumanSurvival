using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private Vector2 movement;    //입력값
    private Vector2 clickTarget;    //마우스 클릭
    private float moveSpeed = 8f;   //속도
    bool moving;

    [SerializeField] Rigidbody2D rb;    //리디지바디
    [SerializeField] SpriteRenderer spriter;    //스프라이트
    [SerializeField] Animator animator;  //애니메이션
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        clickTarget = transform.position;
    }
    void Update()
    {
        // click 이벤트
        if (Input.GetMouseButtonDown(0))
        {
            clickTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            animator.SetBool("Moving", true);
            moving = true;
        }
    }
    private void FixedUpdate()//물리 계산 할 때 사용
    {
        //movement 조정
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);  //이전 한 프레임 수행 시간

        //click 시 movement 코드
        if (moving && rb.position != clickTarget)
        {
            float step = moveSpeed * Time.fixedDeltaTime;
            rb.position = Vector2.MoveTowards(rb.position, clickTarget, step);
        }
        else
        {
            animator.SetBool("Moving", false);
            moving = false;
        }
    }

    private void LateUpdate()   //모든 Update 함수가 호출된 후, 마지막으로 호출되는 함수
    {
        //키보드로 움직임 확인
        if (movement.magnitude != 0)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
        animator.SetFloat("Speed", movement.magnitude);

        if (movement.x != 0)    //x의 입력값이 있는 경우
        {
            spriter.flipX = movement.x < 0; //방향 뒤집기
        }
    }
    private void OnMove(InputValue value)   //InputSystem으로 키입력을 받는 함수
    {
        movement = value.Get<Vector2>();
    }
    public Vector2 Movement {
        get { return movement; }
    }
}
