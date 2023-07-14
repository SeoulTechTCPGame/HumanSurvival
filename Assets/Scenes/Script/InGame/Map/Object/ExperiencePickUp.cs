using UnityEngine;

public class ExperiencePickUp : MonoBehaviour, ICollectible
{
    public float ExpGranted;
    private Transform mPlayer;
    private const float mCollectDistacne= 0.3f;
    private const float mSpeed= 7f;
    private float pickupDistance = 2.5f;
    private void Awake()
    {
        mPlayer = GameManager.instance.Player.transform;
    }
    private void OnEnable()
    {
        mPlayer = GameManager.instance.Player.transform;
        //새로운 거리 업데이트
        // float currentR = MDEFAULT_R * (1 + stat / 100);
        // 수집 원 반지름= 기본*(1+자석스탯/100)
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, mPlayer.position);
        if (distance > pickupDistance) return;
        transform.position = Vector3.MoveTowards(transform.position, mPlayer.position, mSpeed * Time.deltaTime);
        if (distance < mCollectDistacne) Collect();
    }
    public void Collect()
    {
        //스크립트 명으로 오브젝트 찾기
        Character character = GameManager.instance.Character;
        //Todo : character grouth stat 
        character.GetExp(ExpGranted);
        gameObject.SetActive(false);
    }
}