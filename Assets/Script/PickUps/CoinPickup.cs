using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour, ICollectible
{
    public int count;
    public void Collect()
    {
        Character character = FindObjectOfType<Character>();
        //character.GetCoin(count);
    }
}
