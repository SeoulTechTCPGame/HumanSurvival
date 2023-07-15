using UnityEngine;

public class TresureChestPickup : MonoBehaviour, ICollectible
{
    [SerializeField] GameObject mTresureChestUI;
    public void Collect()
    {
        Debug.Log("보물 상자 획득");
        mTresureChestUI.GetComponent<TreasureChest>().LoadChestUI();
        Destroy(gameObject);
    }
}