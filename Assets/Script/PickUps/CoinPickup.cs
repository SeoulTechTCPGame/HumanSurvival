using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour, ICollectible
{
    public int amount;
    public void Collect()
    {
        GameManager.instance.GetCoin(amount);
        gameObject.SetActive(false);
    }
}
