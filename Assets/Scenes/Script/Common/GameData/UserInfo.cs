using System;
using System.Linq;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public static UserInfo instance;
    public UserData UserDataSet;

    private const int JUMP_ACCESSORY = 58;
    private const int JUMP_STAGE = 0;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public void UpdateAccumulatedTime(float time)
    {
        UserDataSet.AccumulatedTime += time;
        UserDataManager.instance.SaveData();
    }
    public void UpdateAccumulatedKill(int kill)
    {
        UserDataSet.AccumulatedKill += kill;
        UserDataManager.instance.SaveData();
    }   
    public void UpdateGold(int gold)
    {
        UserDataSet.Gold += gold;
        UserDataManager.instance.SaveData();
    }
    public void ConsumeGold(int gold)
    {
        UserDataSet.Gold += gold;
        UserDataSet.ConsumedGold -= gold;
        UserDataManager.instance.SaveData();
    }
    public void RefundGold()
    {
        UserDataSet.ConsumedGold = 0;
        UserDataManager.instance.SaveData();
    }
    public void UpdateColldection(int collectionIndex)
    {
        UserDataSet.BCollection[collectionIndex] = true;
        UserDataManager.instance.SaveData();
    }
    public void UpdateAchievement(int achievementIndexes)
    {
        UserDataSet.BAchievements[achievementIndexes] = true;
        UserDataManager.instance.SaveData();
    }
    public void UpdatePowerUpLevel(int powerUpIndex)
    {
        UserDataSet.PowerUpLevel[powerUpIndex]++;
        UserDataManager.instance.SaveData();
    }
    public void RefundPowerUpLevel(int powerUpIndex)
    {
        UserDataSet.PowerUpLevel[powerUpIndex] = 0;
        UserDataManager.instance.SaveData();
    }
    public void UpdatePowerUpStat(int powerUpIndex, float powerUpStat)
    {
        UserDataSet.PowerUpStat[powerUpIndex] += powerUpStat;
        UserDataManager.instance.SaveData();
    }
    public void RefundPowerUpStat(int powerUpIndex)
    {
        UserDataSet.PowerUpStat[powerUpIndex] = 0;
        UserDataManager.instance.SaveData();
    }
    public void UpdatePowerUpCash(int powerUpIndex)
    {
        UserDataSet.NowPowerUpCash[powerUpIndex] = (int)(UserDataSet.PowerUpCash[powerUpIndex] * (1 + UserDataSet.PowerUpLevel[powerUpIndex]) + 20 * Math.Pow(1.1, UserDataSet.PowerUpLevel.Sum() - 1));
        UserDataManager.instance.SaveData();
    }
    public void RefundPowerUpCash(int powerUpIndex)
    {
        UserDataSet.NowPowerUpCash[powerUpIndex] = UserDataSet.PowerUpCash[powerUpIndex];
        UserDataManager.instance.SaveData();
    }
    public void UpdatePowerUpActive(int powerUpIndex, bool btemp)
    {
        UserDataSet.BPowerUpActive[powerUpIndex] = btemp;
        UserDataManager.instance.SaveData();
    }
    public void RefundPowerUpActive(int powerUpIndex)
    {
        UserDataSet.BPowerUpActive[powerUpIndex] = true;
        UserDataManager.instance.SaveData();
    }
    private void UpdateOption()
    {
        // TODO: Option기능 완성되면 추가
        UserDataManager.instance.SaveData();
    }
    private void UnlockCharacter(int characterIndex)
    {
        UserDataSet.BUnlockCharacters[characterIndex] = true;
        UserDataManager.instance.SaveData();
    }
    private void UnlockWeapon(int weaponIndex)
    {
        UserDataSet.BCollection[(weaponIndex << 1) | 1] = true;
        UserDataManager.instance.SaveData();
    }
    private void UnlockAccessory(int accessoryIndex)
    {
        UserDataSet.BCollection[accessoryIndex + JUMP_ACCESSORY] = true;
        UserDataManager.instance.SaveData();
    }
    private bool IsWeaponUnlock(int weaponIndex)
    {
        return UserDataSet.BCollection[(weaponIndex << 1) | 1];
    }
    private bool IsAccessoryUnlock(int accessoryIndex)
    {
        return UserDataSet.BCollection[accessoryIndex + JUMP_ACCESSORY];
    }
}