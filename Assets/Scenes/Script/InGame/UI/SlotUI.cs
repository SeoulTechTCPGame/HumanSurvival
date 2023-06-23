using UnityEngine;

public class SlotUI : MonoBehaviour
{
    public GameObject SlotList;
    private GameObject mSlotprefab;
    
    public void AddSlot(int index,int slotType)
    {
        string resourceName;
        switch (slotType)
        {
            case 0:
                Enums.EWeapon[] enumValuesWeapon = (Enums.EWeapon[])System.Enum.GetValues(typeof(Enums.EWeapon));
                Enums.EWeapon weapon = enumValuesWeapon[index];
                resourceName = "Weapons/" + weapon.ToString()+"Slot";
                mSlotprefab = Resources.Load<GameObject>(resourceName);
                break;
            case 1:
                Enums.EAccessory[] enumValuesAccessory = (Enums.EAccessory[])System.Enum.GetValues(typeof(Enums.EAccessory));
                Enums.EAccessory accessory = enumValuesAccessory[index];
                resourceName = "Accessory/" + accessory.ToString()+"Slot";
                mSlotprefab = Resources.Load<GameObject>(resourceName);
                break;
        }
        GameObject slot=Instantiate(mSlotprefab);
        slot.transform.SetParent(SlotList.transform);
    }
}