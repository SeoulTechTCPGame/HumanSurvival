using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 180 * 10f;

    [Header("# Player Info")]
    public Character character;
    public int level;
    public int kill;
    public float exp;
    public float maxExp;
    public int coin;
    public float[] weaponGetTime = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public float[] weaponDamage = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // Whip, MagicWand, Knife, Axe, Cross, KingBible, FireWand, Garlic, SantaWater, Peachone, EbonyWings, Runetracer, LightningRing
    public int[] killCount = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };   // Whip, MagicWand, Knife, Axe, Cross, KingBible, FireWand, Garlic, SantaWater, Peachone, EbonyWings, Runetracer, LightningRing
    public EquipmentManagementSystem equipManageSys;
    public RandomPickUpSystem RandomPickUpSystem;
    //캐릭터의 스탯지정
    public CharacterScriptableObject characterData; // 초기값
    public float[] CharacterStats; // 변하는 스텟

    [Header("# Game Object")]
    public PoolManager pool;
    public PlayerMovement player;
    public GameObject gameoverPanel;
    public GameObject LevepUpUI;
    public GameObject WeaponSlot;
    public GameObject AccessarySlot;
    //  Singleton Instance 선언
    public static GameManager instance = null;

    private void Awake()
    {
        // Scene에 이미 인스턴스가 존재 하는지 확인 후 처리
        /*if (instance)
        {
            Destroy(this.gameObject);
            return;
        }*/
        // instance를 유일 오브젝트로 만든다
        instance = this;
        // Scene 이동 시 삭제 되지 않도록 처리
        DontDestroyOnLoad(this.gameObject);
        level = 1;
        exp = 0;
        maxExp = 100;
        //Time.timeScale = 1;
    }
    private void Start()
    {    
        //ToDo: 임시 초기화
        CharacterStats = new float[21] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        level = 1;
        RandomPickUpSystem = new RandomPickUpSystem();
        equipManageSys = new EquipmentManagementSystem();
        // TODO: user가 메인 화면에서 강화해놓은 스탯들을 기본값으로 받아오기
        string resourceName = "CharacterData/";
        try
        {
            resourceName += DataManager.instance.currentCharcter;
        }
        catch (NullReferenceException)
        {
            resourceName += "Alchemist";
        }
        characterData = Resources.Load<CharacterScriptableObject>(resourceName);
        CharacterStats[(int)Enums.Stat.Might] = characterData.Might + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.Stat.Might];
        CharacterStats[(int)Enums.Stat.Armor] = characterData.Armor + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.Stat.Armor];
        CharacterStats[(int)Enums.Stat.MaxHealth] = 1 + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.Stat.MaxHealth]; // characterData.MaxHealth(체력 값)랑 CharacterStats[MaxHealth](% 증가량)랑 다르다!
        CharacterStats[(int)Enums.Stat.Recovery] = characterData.Recovery + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.Stat.Recovery];
        CharacterStats[(int)Enums.Stat.Cooldown] = characterData.Cooldown + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.Stat.Cooldown];
        CharacterStats[(int)Enums.Stat.Area] = characterData.Area + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.Stat.Area];
        CharacterStats[(int)Enums.Stat.ProjectileSpeed] = characterData.ProjectileSpeed + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.Stat.ProjectileSpeed];
        CharacterStats[(int)Enums.Stat.Duration] = characterData.Duration + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.Stat.Duration];
        CharacterStats[(int)Enums.Stat.Amount] = characterData.Amount + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.Stat.Amount];
        CharacterStats[(int)Enums.Stat.MoveSpeed] = characterData.MoveSpeed + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.Stat.MoveSpeed];
        CharacterStats[(int)Enums.Stat.Magnet] = characterData.MagnetBonus + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.Stat.Magnet];
        CharacterStats[(int)Enums.Stat.Luck] = characterData.Luck + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.Stat.Luck];
        CharacterStats[(int)Enums.Stat.Growth] = characterData.Growth + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.Stat.Growth];
        CharacterStats[(int)Enums.Stat.Greed] = characterData.Greed + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.Stat.Greed];
        CharacterStats[(int)Enums.Stat.Curse] = characterData.Curse + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.Stat.Curse];
        CharacterStats[(int)Enums.Stat.Revival] = characterData.Revival + UserInfo.instance.UserDataSet.PowerUpStat[(int)Enums.Stat.Revival];
        CharacterStats[(int)Enums.Stat.Reroll] = characterData.Reroll;
        CharacterStats[(int)Enums.Stat.Skip] = characterData.Skip;
        CharacterStats[(int)Enums.Stat.Banish] = characterData.Banish;
        CharacterStats[(int)Enums.Stat.Ommi] = characterData.Ommi;
        CharacterStats[(int)Enums.Stat.Reflection] = characterData.Reflection;
        equipManageSys.Set(characterData.startingWeapon);
        UpdateLuck(CharacterStats[(int)Enums.Stat.Luck]);
    }
    private void Update()
    {
        gameTime += Time.deltaTime;
        if (gameTime >maxGameTime)
        {
            gameTime = maxGameTime;
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
        player.enabled = false; // Character object 비활성화
        pool.enabled = false;
        Time.timeScale = 0;
        gameoverPanel.SetActive(true); // 판넬 활성화
    }
    public void LevelUp()
    {
        level++;
        PauseGame();
        var pickUps = RandomPickUpSystem.RandomPickUp(equipManageSys);
        Debug.Log(pickUps.Count);
        LevepUpUI.GetComponent<LevelUpUIManager>().LoadLevelUpUI(CharacterStats, pickUps, equipManageSys.Weapons, equipManageSys.Accessories);
    }
    public void UpdateLuck(float luck)
    {
        RandomPickUpSystem.UpdateWeaponPickUpList();
        RandomPickUpSystem.UpdateAccessoryPickUpList();
    }
    public void GetCoin(int amount)
    {
        coin += amount;
        Debug.Log("coin: "+coin);
    }
}
