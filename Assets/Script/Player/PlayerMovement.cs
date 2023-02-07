using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public Vector2 movement;    //입력값
    private Vector2 clickTarget;    //마우스 클릭
    private Vector2 relativePos;

    private float moveSpeed = 8f;   //속도
    private float horizontal;
    private float vertical;
    bool moving;

    [SerializeField] Rigidbody2D rb;    //리디지바디
    [SerializeField] SpriteRenderer spriter;    //스프라이트
    [SerializeField] Animator animator;  //애니메이션

    void Start()
    {
        //animator = GetComponent<Animator>();    //필요한 가?
        clickTarget = transform.position;
    }
    void Update()
    {
        //input 설정
        //movement.x = Input.GetAxisRaw("Horizontal"); //키입력
        //movement.y = Input.GetAxisRaw("Vertical"); 
        //animator.SetFloat("Horizontal", movement.x);
        //animator.SetFloat("Vertical", movement.y);
        //animator.SetFloat("Speed", movement.sqrMagnitude); //성능 테스트
        //if (horizontal != 0||vertical !=0) animator.SetBool("Moving", true);
        //else animator.SetBool("Moving", false);

        // click 이벤트
        if (Input.GetMouseButtonDown(0))
        {
            clickTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            animator.SetBool("moving", true);
            moving = true;
        }

        //relativePos = new Vector2(clickTarget.x - rb.position.x, clickTarget.y - rb.position.y);

    }
    private void FixedUpdate()//물리 계산 할 때 사용
    {
        //movement 조정
        //horizontal = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw("Vertical");
        
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
}