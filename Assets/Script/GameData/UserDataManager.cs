using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    [Serializable]
    public class UserData
    {
        public int Gold;
        public bool[] UnlockWeapons;
        public bool[] UnlockCharacters;
        public bool[] Achievements;
        public bool[] Collection;
        public int[] Options;
        public int[] PowerUps;
    }
    public static UserDataManager instance;
    public UserData UserDataSet;
    public string SavePath;
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
        SavePath = Application.persistentDataPath + "/SaveData/";
    }
    void Start()
    {
        if (File.Exists(UserDataManager.instance.SavePath + "UserSaveData"))
        {
            LoadData();
        }
        else
        {
            DataReset();
        }
    }

    void applyData()
    {
        // 게임 시작시 저장 데이터 적용?
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(instance);
        File.WriteAllText(SavePath + "UserSaveData", data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(SavePath + "UserSaveData");
        UserDataSet = JsonUtility.FromJson<UserData>(data);
    }
    public void LoadData(string file)
    {
        // TODO: 파싱 제대로 되는지 판별 후 진행되도록

        string data = File.ReadAllText(file);
        File.WriteAllText(SavePath + "UserSaveData", data);

        // TODO: 게임 재시작 시키기
    }

    public void DataReset()
    {
        string data = File.ReadAllText(SavePath + "DefaultUserData");
        UserDataSet = JsonUtility.FromJson<UserData>(data);
        SaveData();
    }
    void UpdateGold(int nowGold)
    {
        UserDataSet.Gold = nowGold;
        SaveData();
    }
    void UpdateColldection(List<int> weaponIndexes, List<int> accessoryIndexes)
    {
        // 임시로 작성
        foreach (int index in weaponIndexes)
        {
            UserDataSet.Collection[index] = true;
        }
        foreach (int index in accessoryIndexes)
        {
            UserDataSet.Collection[index + Constants.MaxWeaponNumber] = true;
        }
        SaveData();
    }
    void UpdateAchievement(List<int> achievementIndexes)
    {
        foreach (int index in achievementIndexes)
        {
            UserDataSet.Achievements[index] = true;
        }
        SaveData();
    }
    void UnlockCharacter(int characterIndex)
    {
        UserDataSet.UnlockCharacters[characterIndex] = true;
        SaveData();
    }
    void UpdatePowerUp(int powerUpIndex)
    {
        UserDataSet.PowerUps[powerUpIndex]++;
        SaveData();
    }
    void UnlockWeapon(int weaponIndex)
    {
        UserDataSet.UnlockWeapons[weaponIndex] = true;
        SaveData();
    }
    void UpdateOption()
    {
        // TODO: Option기능 완성되면 추가
        SaveData();
    }
}
