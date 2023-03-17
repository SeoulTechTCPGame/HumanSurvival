using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeAccessory : MonoBehaviour
{
    [SerializeField] GameObject[] mAccessory;
    public int nowAccessoryIndex;

    public void Charge()
    {
        for (int i = 0; i < mAccessory[nowAccessoryIndex].GetComponent<AccessoryInfo>().accessoryLevel; i++)
        {
            if (mAccessory[nowAccessoryIndex].GetComponent<AccessoryInfo>().accessoryNowLevel == i)
            {
                mAccessory[nowAccessoryIndex].GetComponent<AccessoryInfo>().accessoryToggle[i].isOn = true;
                mAccessory[nowAccessoryIndex].GetComponent<AccessoryInfo>().accessoryNowLevel++;
                break;
            }
        }
    }

    public void Refund()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < mAccessory[i].GetComponent<AccessoryInfo>().accessoryLevel; j++)
            {
                mAccessory[i].GetComponent<AccessoryInfo>().accessoryToggle[j].isOn = false;
            }
            mAccessory[nowAccessoryIndex].GetComponent<AccessoryInfo>().accessoryNowLevel = 0;
        }
    }
}
