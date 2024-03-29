using System;
using System.IO;
using UnityEngine;
using Enums;

[Serializable]
public class UserData
{
    public int Gold = 0;
    public int ConsumedGold = 0;
    public int AccKill = 0;
    public float AccumulatedTime = 0;
    public float AccRestore = 0;
    public int[] Options;
    public int[] PowerUpLevel = new int[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public float[] PowerUpStat = new float[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public float[] PowerUpMaxstat = new float[16] { 0.25f, 3, 0.3f, 0.5f, -0.05f, 0.1f, 0.2f, 0.3f, 1, 0.1f, 0.5f, 0.3f, 0.15f, 0.5f, 0.5f, 1 };
    public int[] PowerUpCash = new int[16] { 200, 600, 200, 200, 900, 300, 300, 300, 5000, 300, 300, 600, 900, 200, 1666, 10000 };
    public int[] NowPowerUpCash = new int[16] { 200, 600, 200, 200, 900, 300, 300, 300, 5000, 300, 300, 600, 900, 200, 1666, 10000 };
    public bool[] BPowerUpActive = new bool[16] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };
    public bool[] BAchievements;
    public bool[] BCollection;
    public bool[] BUnlockStages = new bool[Constants.MAX_STAGE_NUMBER];
    public bool[] BUnlockCharacters = new bool[Constants.MAX_CHARACTER_NUMBER];
    public bool[] BUnlockWeapons = new bool[Constants.MAX_WEAPON_NUMBER];
    public bool[] BUnlockAccessories = new bool[Constants.MAX_ACCESSORY_NUMBER];
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
    private void Start()
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