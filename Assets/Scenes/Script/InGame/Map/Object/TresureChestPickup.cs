using UnityEngine;

public class TresureChestPickup : MonoBehaviour, ICollectible
{
    private GameObject mTresureChestUI;

    private void Start()
    {
        mTresureChestUI = GameObject.Find("TreasureChestUI");
    }
    public void Collect()
    {
        mTresureChestUI.GetComponent<TreasureChest>().LoadChestUI();
        Destroy(gameObject);
    }
}