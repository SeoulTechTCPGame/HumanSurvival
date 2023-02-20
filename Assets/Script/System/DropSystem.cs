using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    /*
    public static DropSystem Instance;
    [SerializeField]
    private GameObject itemPrefab;
    //private Queue<PickUpItem> dropItemPool = new Queue<PickUpItem>();
    private List<PickUpItem> itemPool = new List<PickUpItem>();
    int rand;

    private void Awake()
    {
        Instance = this;
        Init(5);
    }

    private PickUpItem Create()
    {
        var obj = Instantiate(itemPrefab).GetComponent<PickUpItem>();
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
        return obj;
    }

    private void Init(int count)
    {
        for (int i = 0; i < count; i++)
        {
            itemPool.Add(Create());
        }
    }

    public static PickUpItem GetItem()
    {
        int rand = Random.Range(0, Instance.itemPool.Count);
        var obj = Instance.itemPool[rand];
        Instance.itemPool.RemoveAt(rand);
        obj.transform.SetParent(null);
        obj.gameObject.SetActive(true);
        return obj;
    }

    public static void ReturnItem(PickUpItem item)
    {
        item.gameObject.SetActive(false);
        item.transform.SetParent(Instance.transform);
        Instance.itemPool.Add(item);
    }
    */
}
