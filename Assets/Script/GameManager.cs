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
    public CharacterScriptableObject characterData;
    public float[] CharacterStats;

    [Header("# Game Object")]
    public PoolManager pool;
    public PlayerMovement player;
    public GameObject gameoverPanel;
    public GameObject LevepUpUI;

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
        CharacterStats = new float[21] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 70, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        level = 1;
        RandomPickUpSystem = new RandomPickUpSystem();
        equipManageSys = new EquipmentManagementSystem();
        equipManageSys.Set();
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
