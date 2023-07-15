using UnityEngine;

public class CoinPickup : MonoBehaviour, ICollectible
{
    public int Amount;
    [SerializeField] AudioClip mClip;

    public void Collect()
    {
        GameManager.instance.GetCoin(Amount);
        gameObject.SetActive(false);
        SoundManager.instance.PlayBuutonSoundTheOther(mClip);
    }
}