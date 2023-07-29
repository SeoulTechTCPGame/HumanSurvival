using UnityEngine;

public class GoToPlayer : MonoBehaviour
{
    private Transform mPlayer;
    private const float mCollectDistacne = 0.5f;
    private const float mSpeed = 7f;
    private float mPickupDistance = 2.5f;
    private void Awake()
    {
        mPlayer = GameManager.instance.Player.transform;
    }
    private void OnEnable()
    {
        mPlayer = GameManager.instance.Player.transform;
    }
    private void Update()
    {
        mPickupDistance *= GameManager.instance.CharacterStats[(int)Enums.EStat.Magnet];
        float distance = Vector3.Distance(transform.position, mPlayer.position);
        if (distance > mPickupDistance) return;
        transform.position = Vector3.MoveTowards(transform.position, mPlayer.position, mSpeed * Time.deltaTime);
        if (distance < mCollectDistacne) {
            if(gameObject.TryGetComponent(out ICollectible collectible))
                collectible.Collect();
        }
    }
}