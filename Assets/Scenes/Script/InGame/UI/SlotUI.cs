using UnityEngine;

public class SlotUI : MonoBehaviour
{
    GameObject slotprefab;
    public GameObject list;

    public void AddSlot(int index,int slotType)
    {
        string resourceName;
        switch (slotType)
        {
            case 0:
                Enums.Weapon[] enumValuesW = (Enums.Weapon[])System.Enum.GetValues(typeof(Enums.Weapon));
                Enums.Weapon weapon = enumValuesW[index];
                resourceName = "Weapons/" + weapon.ToString()+"Slot";
                slotprefab = Resources.Load<GameObject>(resourceName);
                break;
            case 1:
                Enums.Accessory[] enumValuesA = (Enums.Accessory[])System.Enum.GetValues(typeof(Enums.Accessory));
                Enums.Accessory accessory = enumValuesA[index];
                resourceName = "Accessory/" + accessory.ToString()+"Slot";
                slotprefab = Resources.Load<GameObject>(resourceName);
                break;
        }
        GameObject slot=Instantiate(slotprefab);
        slot.transform.SetParent(list.transform);
    }
}
