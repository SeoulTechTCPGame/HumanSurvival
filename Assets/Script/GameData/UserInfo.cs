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
    void UpdateColldection(List<int> weaponIndexes, List<int> accessoryIndexes)
    {
        // 임시로 작성
        //foreach (int index in weaponIndexes)
        //{
        //    UserDataSet.Collection[index] = true;
        //}
        //foreach (int index in accessoryIndexes)
        //{
        //    UserDataSet.Collection[index + Constants.MaxWeaponNumber] = true;
        //}
        UserDataManager.instance.SaveData();
    }
    void UpdateAchievement(List<int> achievementIndexes)
    {
        foreach (int index in achievementIndexes)
        {
            UserDataSet.Achievements[index] = true;
        }
        // 보상 관련 적용?
        UserDataManager.instance.SaveData();
    }
    void UnlockCharacter(int characterIndex)
    {
        //UserDataSet.UnlockCharacters[characterIndex] = true;
        UserDataManager.instance.SaveData();
    }
    void UpdatePowerUp(int powerUpIndex)
    {
        UserDataSet.PowerUps[powerUpIndex]++;
        UserDataManager.instance.SaveData();
    }
    void UnlockWeapon(int weaponIndex)
    {
        //UserDataSet.UnlockWeapons[weaponIndex] = true;
        UserDataManager.instance.SaveData();
    }
    void UpdateOption()
    {
        // TODO: Option기능 완성되면 추가
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
    bool IsCharacterUnlock(int characterIndex)
    {
        return UserDataSet.UnlockCharacters[characterIndex];
    }
    bool IsStageUnlock(int stageIndex)
    {
        return UserDataSet.UnlockStages[stageIndex];
    }
}