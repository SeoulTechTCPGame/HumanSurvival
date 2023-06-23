using UnityEngine;

public class CoinPickup : MonoBehaviour, ICollectible
{
    public int Amount;

    public void Collect()
    {
        GameManager.instance.GetCoin(Amount);
        gameObject.SetActive(false);
    }
}