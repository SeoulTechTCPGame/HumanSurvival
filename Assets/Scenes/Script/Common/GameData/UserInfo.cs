using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UserInfo : MonoBehaviour
{
    public static UserInfo instance;
    public UserData UserDataSet;
    public const int jumpAccessory = 58;
    public const int jumpStage = 0;
    // 
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
        UserDataSet.accumulatedTime += time;
        UserDataManager.instance.SaveData();
    }
    public void UpdateAccumulatedKill(int kill)
    {
        UserDataSet.accumulatedKill += kill;
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
        UserDataSet.consumedGold -= gold;
        UserDataManager.instance.SaveData();
    }
    public void RefundGold()
    {
        UserDataSet.consumedGold = 0;
        UserDataManager.instance.SaveData();
    }
    public void UpdateColldection(int collectionIndex)
    {
        UserDataSet.Collection[collectionIndex] = true;
        UserDataManager.instance.SaveData();
    }
    public void UpdateAchievement(int achievementIndexes)
    {
        UserDataSet.Achievements[achievementIndexes] = true;
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
        UserDataSet.nowPowerUpCash[powerUpIndex] = (int)(UserDataSet.powerUpCash[powerUpIndex] * (1 + UserDataSet.PowerUpLevel[powerUpIndex]) + 20 * Math.Pow(1.1, UserDataSet.PowerUpLevel.Sum() - 1));
        UserDataManager.instance.SaveData();
    }
    public void RefundPowerUpCash(int powerUpIndex)
    {
        UserDataSet.nowPowerUpCash[powerUpIndex] = UserDataSet.powerUpCash[powerUpIndex];
        UserDataManager.instance.SaveData();
    }
    void UpdateOption()
    {
        // TODO: Option기능 완성되면 추가
        UserDataManager.instance.SaveData();
    }
    void UnlockCharacter(int characterIndex)
    {
        UserDataSet.UnlockCharacters[characterIndex] = true;
        UserDataManager.instance.SaveData();
    }
    void UnlockWeapon(int weaponIndex)
    {
        UserDataSet.Collection[(weaponIndex << 1) | 1] = true;
        UserDataManager.instance.SaveData();
    }
    void UnlockAccessory(int accessoryIndex)
    {
        UserDataSet.Collection[accessoryIndex + jumpAccessory] = true;
        UserDataManager.instance.SaveData();
    }
    bool IsWeaponUnlock(int weaponIndex)
    {
        return UserDataSet.Collection[(weaponIndex << 1) | 1];
    }
    bool IsAccessoryUnlock(int accessoryIndex)
    {
        return UserDataSet.Collection[accessoryIndex + jumpAccessory];
    }
}