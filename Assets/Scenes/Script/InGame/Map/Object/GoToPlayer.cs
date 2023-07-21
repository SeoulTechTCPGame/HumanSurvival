using UnityEngine;

public class GoToPlayer : MonoBehaviour
{
    private Transform mPlayer;
    private const float mCollectDistacne = 0.5f;
    private const float mSpeed = 7f;
    private const float mPickupDistance = 2.5f;
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
        if (distance > mPickupDistance) return;
        transform.position = Vector3.MoveTowards(transform.position, mPlayer.position, mSpeed * Time.deltaTime);
        if (distance < mCollectDistacne) {
            if(gameObject.TryGetComponent(out ICollectible collectible))
                collectible.Collect();
        }
    }
}