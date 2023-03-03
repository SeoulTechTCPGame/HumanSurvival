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

public class Character : EquipmentManagementSystem,IDamageable
{
    //캐릭터의 스탯지정
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
    private int mExp;
    private int mMaxExp;

    public float[] CharacterStats;
    public RandomPickUpSystem RandomPickUpSystem;
    

    void Start()
    {
        mLevel = 1;
        mExp = 0;
        mMaxExp = 100;

        // TODO: user가 메인 화면에서 강화해놓은 스탯들을 기본값으로 받아오기
        CharacterStats = new float[21] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 70, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
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
        GetAccessory(0);
        GetAccessory(1);

    }
    public void RestoreHealth(float amount)
    {
        if(currentHp< (int)Enums.Stat.MaxHealth)
        { 
            currentHp += amount;
            if (currentHp > (int)Enums.Stat.MaxHealth) currentHp = (int)Enums.Stat.MaxHealth;

        }
       
    }
    public void TakeDamage(float damage)
    {
        if (isDead == true) return;
        currentHp -= damage;
        Debug.Log(currentHp);
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

    public void GetExp(int exp)
    {
        // TODO: stat의 growth 적용하여 경험치 획득

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
