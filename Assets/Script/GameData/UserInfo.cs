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
    void getGold(int gold)
    {
        UserDataSet.Gold += gold;
        UserDataManager.instance.SaveData();
    }
    void UpdateColldection(int collectionIndex)
    {
        UserDataSet.Collection[collectionIndex] = true;
        UserDataManager.instance.SaveData();
    }
    void UpdateAchievement(int achievementIndexes)
    {
        UserDataSet.Achievements[achievementIndexes] = true;
        UserDataManager.instance.SaveData();
    }
    void UpdatePowerUp(int powerUpIndex)
    {
        UserDataSet.PowerUps[powerUpIndex]++;
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