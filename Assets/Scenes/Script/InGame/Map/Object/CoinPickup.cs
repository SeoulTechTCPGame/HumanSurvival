using UnityEngine;

public class CoinPickup : MonoBehaviour, ICollectible
{
    public int Amount;
    public Rigidbody2D Target;
    private Vector2 mDirection;
    private Rigidbody2D mRb;
    private void Awake()
    {
        mRb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        Target = GameManager.instance.Player.transform.GetChild(4).GetComponent<Rigidbody2D>();
    }
    public void Collect()
    {
        GameManager.instance.GetCoin(Amount);
        gameObject.SetActive(false);
    }
    public void GravityToPlayer()
    {
        mDirection = (Target.position - mRb.position).normalized;
        Vector2 nextVec = 0.01f * Time.fixedDeltaTime * mDirection;
        mRb.MovePosition(mRb.position + nextVec);
    }
}