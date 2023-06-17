using UnityEngine;
using UnityEngine.UI;

public class ChargePowerUp : MonoBehaviour
{
    public int NowAccessoryIndex;

    [SerializeField] GameObject[] mAccessory;
    [SerializeField] GameObject mChargeObject;
    [SerializeField] GameObject mActiveObject;
    [SerializeField] Toggle mActiveToggle;
    
    private float[] mUpgradeStat = new float[16] { 0.05f, 1, 0.1f, 0.1f, -0.025f, 0.05f, 0.1f, 0.15f, 1, 0.05f, 0.25f, 0.1f, 0.03f, 0.1f, 0.1f, 1 };
    private float[] mTempSaveStat = new float[16];

    public void Charge()
    {
        if(UserInfo.instance.UserDataSet.Gold > UserInfo.instance.UserDataSet.nowPowerUpCash[NowAccessoryIndex] && UserInfo.instance.UserDataSet.PowerUpLevel[NowAccessoryIndex] != mAccessory[NowAccessoryIndex].GetComponent<PowerUpInfo>().AccessoryMaxLevel)
        {
            UserInfo.instance.ConsumeGold(-UserInfo.instance.UserDataSet.nowPowerUpCash[NowAccessoryIndex]);
            for (int i = 0; i < mAccessory[NowAccessoryIndex].GetComponent<PowerUpInfo>().AccessoryMaxLevel; i++)
            {
                if (UserInfo.instance.UserDataSet.PowerUpLevel[NowAccessoryIndex] == i)
                {
                    mAccessory[NowAccessoryIndex].GetComponent<PowerUpInfo>().AccessoryToggle[i].isOn = true;
                    UserInfo.instance.UpdatePowerUpLevel(NowAccessoryIndex);
                    UserInfo.instance.UpdatePowerUpStat(NowAccessoryIndex, mUpgradeStat[NowAccessoryIndex]);
                    break;
                }
            }
            for (int i = 0; i < 16; i++)
            {
                UserInfo.instance.UpdatePowerUpCash(i);
            }
            mAccessory[NowAccessoryIndex].GetComponent<PowerUpInfo>().AccessoryCash.text = UserInfo.instance.UserDataSet.nowPowerUpCash[NowAccessoryIndex].ToString();
        }
        if(UserInfo.instance.UserDataSet.PowerUpLevel[NowAccessoryIndex] == mAccessory[NowAccessoryIndex].GetComponent<PowerUpInfo>().AccessoryMaxLevel)
        {
            mTempSaveStat[NowAccessoryIndex] = UserInfo.instance.UserDataSet.PowerUpStat[NowAccessoryIndex];
            mChargeObject.SetActive(false);
            mActiveObject.SetActive(true);
        }
    }
    public void Refund()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < mAccessory[i].GetComponent<PowerUpInfo>().AccessoryMaxLevel; j++)
            {
                mAccessory[i].GetComponent<PowerUpInfo>().AccessoryToggle[j].isOn = false;
            }
            UserInfo.instance.RefundPowerUpLevel(i);
            UserInfo.instance.RefundPowerUpStat(i);
            UserInfo.instance.RefundPowerUpCash(i);
        }
        UserInfo.instance.ConsumeGold(UserInfo.instance.UserDataSet.consumedGold);
        UserInfo.instance.RefundGold();
        mAccessory[NowAccessoryIndex].GetComponent<PowerUpInfo>().AccessoryCash.text = UserInfo.instance.UserDataSet.nowPowerUpCash[NowAccessoryIndex].ToString();
        
        mChargeObject.SetActive(true);
        mActiveObject.SetActive(false);
    }
    public void StatActive()
    {
        if(mActiveToggle.isOn)
        {
            UserInfo.instance.UserDataSet.PowerUpStat[NowAccessoryIndex] = mTempSaveStat[NowAccessoryIndex];
        }
        else
        {
            UserInfo.instance.UserDataSet.PowerUpStat[NowAccessoryIndex] = 0;
        }
    }
}