using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChargePowerUp : MonoBehaviour
{
    [SerializeField] GameObject[] mAccessory;
    [SerializeField] GameObject mChargeObject;
    public int nowAccessoryIndex;
    private float[] upgradeStat = new float[16] { 0.05f, 1, 0.1f, 0.1f, -0.025f, 0.05f, 0.1f, 0.15f, 1, 0.05f, 0.25f, 0.1f, 0.03f, 0.1f, 0.1f, 1 };

    public void Charge()
    {
        if(UserInfo.instance.UserDataSet.Gold > UserInfo.instance.UserDataSet.nowPowerUpCash[nowAccessoryIndex] && UserInfo.instance.UserDataSet.PowerUpLevel[nowAccessoryIndex] != mAccessory[nowAccessoryIndex].GetComponent<PowerUpInfo>().accessoryLevel)
        {
            UserInfo.instance.getGold(-UserInfo.instance.UserDataSet.nowPowerUpCash[nowAccessoryIndex]);
            for (int i = 0; i < mAccessory[nowAccessoryIndex].GetComponent<PowerUpInfo>().accessoryLevel; i++)
            {
                if (UserInfo.instance.UserDataSet.PowerUpLevel[nowAccessoryIndex] == i)
                {
                    mAccessory[nowAccessoryIndex].GetComponent<PowerUpInfo>().accessoryToggle[i].isOn = true;
                    UserInfo.instance.UpdatePowerUpLevel(nowAccessoryIndex);
                    UserInfo.instance.UpdatePowerUpStat(nowAccessoryIndex, upgradeStat[nowAccessoryIndex]);
                    break;
                }
            }
            for (int i = 0; i < 16; i++)
            {
                UserInfo.instance.UpdatePowerUpCash(i);
            }
            mAccessory[nowAccessoryIndex].GetComponent<PowerUpInfo>().mAccessoryCash.text = UserInfo.instance.UserDataSet.nowPowerUpCash[nowAccessoryIndex].ToString();
        }
    }

    public void Refund()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < mAccessory[i].GetComponent<PowerUpInfo>().accessoryLevel; j++)
            {
                mAccessory[i].GetComponent<PowerUpInfo>().accessoryToggle[j].isOn = false;
            }
            UserInfo.instance.RefundPowerUpLevel(i);
            UserInfo.instance.RefundPowerUpStat(i);
            UserInfo.instance.RefundPowerUpCash(i);
        }
        UserInfo.instance.getGold(UserInfo.instance.UserDataSet.consumeGold);
        UserInfo.instance.RefundGold();
        mAccessory[nowAccessoryIndex].GetComponent<PowerUpInfo>().mAccessoryCash.text = UserInfo.instance.UserDataSet.nowPowerUpCash[nowAccessoryIndex].ToString();
    }
}
