using Enums;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("# Game Control")]
    public EStage GameStage;
    public float GameTime;
    public float MaxGameTime = 180 * 10f;
    public bool IsGMAlive = true;
    [Header("# Player Info")]
    public Character Character;
    public int Level;
    public int Kill;
    public float Exp;
    public float MaxExp;
    public int Coin;
    public float[] WeaponGetTime = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public float[] WeaponDamage = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // Whip,MagicWand,Knife,Cross,KingBible,FireWand,Garlic,Peachone,EbonyWings,LightningRing,SantaWater
    public int[] KillCount = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };   // Whip,MagicWand,Knife,Cross,KingBible,FireWand,Garlic,Peachone,EbonyWings,LightningRing,SantaWater
    public float RestoreCount;
    public int DestroyCount = 0; // LightObject
    public int FoundChickenCount = 0; // RecoveryObject
    public int EvoGralicRestoreCount = 0;
    public bool BGetChest = false;
    public EquipmentManagementSystem EquipManageSys;
    public RandomPickUpSystem RandomPickUpSystem;
    //캐릭터의 스탯지정
    public CharacterScriptableObject CharacterData; // 초기값
    public float[] CharacterStats; // 변하는 스텟

    [Header("# Game Object")]
    public PoolManager Pool;
    public PlayerMovement Player;
    public GameObject GameOverPanel;
    public GameObject LevepUpUI;
    public GameObject WeaponSlot;
    public GameObject AccessorySlot;
    //  Singleton Instance 선언
    public static GameManager instance = null;


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        Level = 1;
        Exp = 0;
        MaxExp = 100;
        Time.timeScale = 1f;
    }
    private void Start()
    {
        CharacterStats = new float[21] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        Level = 1;
        RandomPickUpSystem = new RandomPickUpSystem();
        EquipManageSys = new EquipmentManagementSystem();
        string resourceName = "CharacterData/";
        try
        {
            resourceName += DataManager.instance.CurrentCharcter;
        }
        catch (NullReferenceException)
        {
            resourceName += "Alchemist";
        }
        CharacterData = Resources.Load<CharacterScriptableObject>(resourceName);
        CharacterStats[(int)Enums.EStat.Might] = CharacterData.Might + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.EStat.Might];
        CharacterStats[(int)Enums.EStat.Armor] = CharacterData.Armor + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.EStat.Armor];
        CharacterStats[(int)Enums.EStat.MaxHealth] = 1 + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.EStat.MaxHealth]; // characterData.MaxHealth(체력 값)랑 CharacterStats[MaxHealth](% 증가량)랑 다르다!
        CharacterStats[(int)Enums.EStat.Recovery] = CharacterData.Recovery + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.EStat.Recovery];
        CharacterStats[(int)Enums.EStat.Cooldown] = CharacterData.Cooldown + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.EStat.Cooldown];
        CharacterStats[(int)Enums.EStat.Area] = CharacterData.Area + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.EStat.Area];
        CharacterStats[(int)Enums.EStat.ProjectileSpeed] = CharacterData.ProjectileSpeed + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.EStat.ProjectileSpeed];
        CharacterStats[(int)Enums.EStat.Duration] = CharacterData.Duration + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.EStat.Duration];
        CharacterStats[(int)Enums.EStat.Amount] = CharacterData.Amount + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.EStat.Amount];
        CharacterStats[(int)Enums.EStat.MoveSpeed] = CharacterData.MoveSpeed + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.EStat.MoveSpeed];
        CharacterStats[(int)Enums.EStat.Magnet] = CharacterData.MagnetBonus *(1+ UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.EStat.Magnet]);
        CharacterStats[(int)Enums.EStat.Luck] = CharacterData.Luck + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.EStat.Luck];
        CharacterStats[(int)Enums.EStat.Growth] = CharacterData.Growth + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.EStat.Growth];
        CharacterStats[(int)Enums.EStat.Greed] = CharacterData.Greed + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.EStat.Greed];
        CharacterStats[(int)Enums.EStat.Curse] = CharacterData.Curse + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.EStat.Curse];
        CharacterStats[(int)Enums.EStat.Revival] = CharacterData.Revival + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.EStat.Revival];
        CharacterStats[(int)Enums.EStat.Reroll] = CharacterData.Reroll;
        CharacterStats[(int)Enums.EStat.Skip] = CharacterData.Skip;
        CharacterStats[(int)Enums.EStat.Banish] = CharacterData.Banish;
        CharacterStats[(int)Enums.EStat.Ommi] = CharacterData.Ommi;
        CharacterStats[(int)Enums.EStat.Reflection] = CharacterData.Reflection;

        EquipManageSys.Set(CharacterData.startingWeapon);
        UpdateLuck(CharacterStats[(int)Enums.EStat.Luck]);
    }
    private void Update()
    {
        GameTime += Time.deltaTime;
        if (GameTime > MaxGameTime)
        {
            GameTime = MaxGameTime;
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public void GameOverPanelUp()
    {
        Debug.Log("Game over");
        IsGMAlive = false;
        GameObject chest;
        chest = GameObject.Find("chest(Clone)");
        Destroy(chest);
        Player.enabled = false; // Character object 비활성화
        Pool.enabled = false;
        Time.timeScale = 0;
        GameOverPanel.SetActive(true); // 판넬 활성화
    }
    public void ProcessGameOverResults()
    {
        var userData = UserInfo.instance.UserDataSet;
        userData.Gold += Coin;
        userData.AccumulatedTime += GameTime;
        userData.AccKill += Kill;
        userData.AccRestore += RestoreCount;
        UserInfo.instance.AchiManager.UpdateAchievements();
        UserInfo.instance.CollectionManager.UpdateCollections();
        UserDataManager.instance.SaveData();
    }
    public void LevelUp()
    {
        Level++;
        PauseGame();
        var pickUps = RandomPickUpSystem.RandomPickUp();
        LevepUpUI.GetComponent<LevelUpUIManager>().LoadLevelUpUI(CharacterStats, pickUps, EquipManageSys.Weapons, EquipManageSys.Accessories);
    }
    public void UpdateLuck(float luck)
    {
        RandomPickUpSystem.UpdateWeaponPickUpList();
        RandomPickUpSystem.UpdateAccessoryPickUpList();
    }
    public void GetCoin(int amount)
    {
        amount =(int)Math.Ceiling(amount * CharacterStats[(int)Enums.EStat.Greed]);
        Coin += amount;
    }
}