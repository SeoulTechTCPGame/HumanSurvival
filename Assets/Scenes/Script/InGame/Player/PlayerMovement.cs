using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D mRb;    //리디지바디
    [SerializeField] SpriteRenderer mSpriter;    //스프라이트
    [SerializeField] Animator mAnimator;  //애니메이션

    private Vector2 mMovement;    //입력값
    private Vector2 mPreMovement; //이전 이동 방향 벡터 가져오기
    private float mMoveSpeed = 8f;   //속도

    private void Awake()
    {
        mRb = GetComponent<Rigidbody2D>();
        mSpriter = GetComponent<SpriteRenderer>();
        mAnimator = GetComponent<Animator>();
        //애니메이터 파일 이름을 설정 ex> Animator/Heroknight
        string resourceName = "Animator/";
        try
        {
            resourceName += DataManager.instance.CurrentCharcter;
        }
        catch (NullReferenceException)
        {
            resourceName += "Alchemist";
        }
        //실행중에 에니메이터 바꾸기. Resources.Load()는 path의 파일을 load한다. Asset>Resource가 root 경로
        mAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(resourceName);
    }
    private void Update()
    {
        if (mMovement != Vector2.zero)
        {
            mPreMovement = mMovement;
        }
    }
    private void FixedUpdate()//물리 계산 할 때 사용
    {
        //movement 조정
        mRb.MovePosition(mRb.position + mMovement * mMoveSpeed * Time.fixedDeltaTime);  //이전 한 프레임 수행 시간
    }
    private void LateUpdate()   //모든 Update 함수가 호출된 후, 마지막으로 호출되는 함수
    {
        //키보드로 움직임 확인
        if (mMovement.magnitude != 0)
        {
            mAnimator.SetBool("Moving", true);
        }
        else
        {
            mAnimator.SetBool("Moving", false);
        }
        mAnimator.SetFloat("Speed", mMovement.magnitude);

        if (mMovement.x != 0)    //x의 입력값이 있는 경우
        {
            mSpriter.flipX = mMovement.x < 0; //방향 뒤집기
        }
    }
    public Vector2 Movement
    {
        get { return mMovement; }
    }
    public Vector2 PreMovement
    {
        get { return mPreMovement; }
    }
    private void OnMove(InputValue value)   //InputSystem으로 키입력을 받는 함수
    {
        mMovement = value.Get<Vector2>();
    }
}