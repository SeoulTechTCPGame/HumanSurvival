﻿using System.Collections;
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

public class Character : EquipmentManagementSystem,IDamageable
{
    //캐릭터의 스탯지정
    public CharacterScriptableObject characterData;
    //예시를 위해 값은 무작위로 넣음
    public GameObject LevepUpUI;

    [SerializeField] HealthBar HpBar;
    private bool isDead;
    private float currentHp = 100;
    private float maxHp = 100;
    private float mDamage = 10;              //피해량
    private float mProjectileSpeed = 1;     //투사체 속도
    private float mDuration = 3;            //지속 시간
    private float mAttackRange = 1;         //공격범위
    private float mCooldown = 3;            //쿨타임
    private int mNumberOfProjectiles = 1;     //투사체 수

    private int mLevel;
    private float mExp;
    private int mMaxExp;

    public float[] CharacterStats;
    public RandomPickUpSystem RandomPickUpSystem;
    

    void Start()
    {   

        mLevel = 1;
        mExp = 0;
        mMaxExp = 100;

        // TODO: user가 메인 화면에서 강화해놓은 스탯들을 기본값으로 받아오기

        string resourceName = "CharacterData/";
        try
        {
            resourceName += DataManager.instance.currentCharcter;
        }
        catch (NullReferenceException ex)
        {
            resourceName += "Alchemist";
        }

        characterData = Resources.Load<CharacterScriptableObject>(resourceName);
        Debug.Log(characterData.MagnetBonus);
        CharacterStats = new float[21] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        Weapons = new List<Weapon>();
        Accessories = new List<Accessory>();
        TransWeaponIndex = Enumerable.Repeat<int>(-1, 13).ToArray<int>();
        TransAccessoryIndex = Enumerable.Repeat<int>(-1, 21).ToArray<int>();
        MasteredWeapons = new List<int>();
        MasteredAccessories = new List<int>();
        RandomPickUpSystem = new RandomPickUpSystem();
        UpdateLuck(CharacterStats[(int)Enums.Stat.Luck]);

        // 임시
        GetWeapon(2);
        GetWeapon(7);
        GetAccessory(0);
        GetAccessory(1);

    }
    public void RestoreHealth(float amount)
    {
        
        //if(currentHp< CharacterStats[(int)Enums.Stat.MaxHealth])
        if(currentHp<characterData.MaxHealth)
        { 
            currentHp += amount;
            if (currentHp > 100) currentHp = 100;
            HpBar.SetState(currentHp, maxHp);
        }
    }
    public void TakeDamage(float damage, int weaponIndex)
    {
        if (isDead == true) return;
        currentHp -= damage;
        if (currentHp <= 0)
        {
            GameManager.instance.GameOverPanelUp();
            isDead = true;
        }

        HpBar.SetState(currentHp, maxHp);
        
    }

    public void TempLoad()
    {
        LevelUp();
    }

    public void GetExp(float exp)
    {
        // TODO: stat의 growth 적용하여 경험치 획득
        mExp += exp;
        GameManager.instance.exp = mExp;
        while (mExp >= mMaxExp)
        {
            mExp -= mMaxExp;
            mMaxExp += Constants.DeltaExp;
            GameManager.instance.maxExp = mMaxExp;
            LevelUp();
        }
        Debug.Log("Exp:" + GameManager.instance.exp);
    }
    public void LevelUp()
    {
        mLevel++;
        GameManager.instance.level++;
        GameManager.instance.PauseGame();
        var pickUps = RandomPickUpSystem.RandomPickUp(this);
        LevepUpUI.GetComponent<LevelUpUIManager>().LoadLevelUpUI(CharacterStats, pickUps, Weapons, Accessories);
    }
    public void UpdateLuck(float luck)
    {
        RandomPickUpSystem.UpdateWeaponPickUpList(this);
        RandomPickUpSystem.UpdateAccessoryPickUpList(this);
    }


    //Get,Set함수 자동 구현
    public float Damage
    {
        get { return mDamage; }
        set { mDamage = value; }
    }
    public float ProjectileSpeed
    {
        get { return mProjectileSpeed; }
        set { mProjectileSpeed = value; }
    }
    public float Duration
    {
        get { return mDuration; }
        set { mDuration = value; }
    }
    public float AttackRange
    {
        get { return mAttackRange; }
        set { mAttackRange = value; }
    }
    public float Cooldown
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
