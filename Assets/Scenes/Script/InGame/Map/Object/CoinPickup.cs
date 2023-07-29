using UnityEngine;

public class CoinPickup : MonoBehaviour, ICollectible
{
    [SerializeField] AudioClip mClip;

    public void Collect()
    {
        GameManager.instance.GetCoin(Random.Range(0, 101));
        gameObject.SetActive(false);
        SoundManager.instance.PlaySoundEffect(mClip);
    }
}