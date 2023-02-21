using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Enums;
using Rito;
using static UnityEngine.Rendering.DebugUI.Table;
using static Constants;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

public class Character : EquipmentManagementSystem
{
    //Ä³¸¯ÅÍÀÇ ½ºÅÈÁöÁ¤
    //¿¹½Ã¸¦ À§ÇØ °ªÀº ¹«ÀÛÀ§·Î ³ÖÀ½
    public GameObject LevepUpUI;

    private float currentHealth = 100;
    private int mDamage = 10;              //ÇÇÇØ·®
    private int mProjectileSpeed = 1;     //Åõ»çÃ¼ ¼Óµµ
    private int mDuration = 3;            //Áö¼Ó ½Ã°£
    private int mAttackRange = 1;         //°ø°Ý¹üÀ§
    private int mCooldown = 3;            //ÄðÅ¸ÀÓ
    private int mNumberOfProjectiles = 1;     //Åõ»çÃ¼ ¼ö

    private int mLevel;
    private int mExp;
    private int mMaxExp;

    public float[] CharacterStats;
    public RandomPickUpSystem RandomPickUpSystem;

    void Start()
    {
        mLevel = 1;
        mExp = 0;
        mMaxExp = 100;

        // TODO: user°¡ ¸ÞÀÎ È­¸é¿¡¼­ °­È­ÇØ³õÀº ½ºÅÈµéÀ» ±âº»°ªÀ¸·Î ¹Þ¾Æ¿À±â
        CharacterStats = new float[21] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 70, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        Weapons = new List<Weapon>();
        Accessories = new List<Accessory>();
        TransWeaponIndex = Enumerable.Repeat<int>(-1, 13).ToArray<int>();
        TransAccessoryIndex = Enumerable.Repeat<int>(-1, 21).ToArray<int>();
        MasteredWeapons = new List<int>();
        MasteredAccessories = new List<int>();
        RandomPickUpSystem = new RandomPickUpSystem();
        UpdateLuck(CharacterStats[(int)Enums.Stat.Luck]);

        // ÀÓ½Ã
        GetWeapon(0);
        GetWeapon(1);
        GetAccessory(0);
        GetAccessory(1);

    }
    public void RestoreHealth(float amount)
    {
        if(currentHealth< (int)Enums.Stat.MaxHealth)
        { 
            currentHealth += amount;
            if (currentHealth > (int)Enums.Stat.MaxHealth) currentHealth = (int)Enums.Stat.MaxHealth;

        }
       
    }
    //ë²„íŠ¼ ì§€?°ë©´ ?? œ ?ˆì •
    public void TempLoad()
    {
        LevelUp();
    }

    public void GetExp(int exp)
    {
        // TODO: statÀÇ growth Àû¿ëÇÏ¿© °æÇèÄ¡ È¹µæ

        mExp += exp;
        while (mExp >= mMaxExp)
        {
            mExp -= mMaxExp;
            mMaxExp += Constants.DeltaExp;
            LevelUp();
        }
        Debug.Log(mExp);
    }
    public void LevelUp()
    {
        mLevel++;
        GameObject.Find("GameManager").GetComponent<GameManager>().PauseGame();
        var pickUps = RandomPickUpSystem.RandomPickUp(this);
        LevepUpUI.GetComponent<LevelUpUIManager>().LoadLevelUpUI(CharacterStats, pickUps, Weapons, Accessories);
    }
    public void UpdateLuck(float luck)
    {
        RandomPickUpSystem.UpdateWeaponPickUpList(this);
        RandomPickUpSystem.UpdateAccessoryPickUpList(this);
    }
    //Get,SetÇÔ¼ö ÀÚµ¿ ±¸Çö
    public int Damage
    {
        get { return mDamage; }
        set { mDamage = value; }
    }
    public int ProjectileSpeed
    {
        get { return mProjectileSpeed; }
        set { mProjectileSpeed = value; }
    }
    public int Duration
    {
        get { return mDuration; }
        set { mDuration = value; }
    }
    public int AttackRange
    {
        get { return mAttackRange; }
        set { mAttackRange = value; }
    }
    public int Cooldown
    {
        get { return mCooldown; }
        set { mCooldown = value; }
    }
    public int NumberOfProjectiles
    {
        get { return mNumberOfProjectiles; }
        set { mNumberOfProjectiles = value; }
    }
}
