using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlotUI : MonoBehaviour
{
    public enum slotType { Weapon,Accessory} //0 ,1
    GridLayoutGroup grid;
    GameObject slotprefab;
    public GameObject list;
    private void Awake()
    {
        grid = gameObject.GetComponent<GridLayoutGroup>();
    }
    public void AddSlot(int index,int type)
    {
        string resourceName;
        switch (type)
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
        GameObject slot=(GameObject)Instantiate(slotprefab);
        slot.transform.SetParent(list.transform);
    }
}
