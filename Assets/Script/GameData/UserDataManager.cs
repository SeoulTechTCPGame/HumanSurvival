using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[Serializable]
public class UserData
{
    public int Gold;
    public int[] Options;
    public int[] PowerUps;
    public bool[] Achievements;
    public bool[] Collection;
    public bool[] UnlockStages;
    public bool[] UnlockCharacters;
}
public class UserDataManager : MonoBehaviour
{
    public static UserDataManager instance;
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
        SavePath = Application.persistentDataPath + "/";
    }
    void Start()
    {
        if (File.Exists(UserDataManager.instance.SavePath + "UserSaveData.json"))
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
        string data = JsonUtility.ToJson(UserInfo.instance.UserDataSet);
        File.WriteAllText(SavePath + "UserSaveData.json", data);
    }
    public void LoadData()
    {
        string data = File.ReadAllText(SavePath + "UserSaveData.json");
        UserInfo.instance.UserDataSet = JsonUtility.FromJson<UserData>(data);
    }
    public bool LoadData(string dataPath)
    {
        string data = File.ReadAllText(dataPath);
        UserData tempData;
        try
        {
            tempData = JsonUtility.FromJson<UserData>(data);
        }
        catch (ArgumentException)
        {
            return false;
        }

        UserInfo.instance.UserDataSet = tempData;
        SaveData();

        return true;
    }
    public void DataReset()
    {
        TextAsset textData = Resources.Load("GameData/DefaultUserData") as TextAsset;
        UserInfo.instance.UserDataSet = JsonUtility.FromJson<UserData>(textData.text);
        SaveData();
    }
}
