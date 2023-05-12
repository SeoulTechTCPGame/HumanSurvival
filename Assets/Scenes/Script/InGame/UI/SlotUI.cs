using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlotUI : MonoBehaviour
{
    GridLayoutGroup grid;
    GameObject slotprefab;
    public GameObject list;
    private void Awake()
    {
        grid = gameObject.GetComponent<GridLayoutGroup>();
    }
    public void AddSlot(int index,int slotType)
    {
        string resourceName;
        switch (slotType)
        {
            case 0:
                resourceName = "Weapons/"+index;
                slotprefab = Resources.Load<GameObject>(resourceName);
                break;
            case 1:
                resourceName = "Accessory/" + index;
                slotprefab = Resources.Load<GameObject>(resourceName);
                break;
        }
        GameObject slot=Instantiate(slotprefab);
        slot.transform.SetParent(list.transform);
    }
}
