using UnityEngine;

public class TresureChestPickup : MonoBehaviour, ICollectible
{
    private GameObject mTresureChestUI;
    [SerializeField] AudioClip mClip;

    private void Start()
    {
        mTresureChestUI = GameObject.Find("TreasureChestUI");
    }
    public void Collect()
    {
        mTresureChestUI.GetComponent<TreasureChest>().LoadChestUI();
        Destroy(gameObject);
        SoundManager.instance.PlaySoundTheOther(mClip);
    }
}