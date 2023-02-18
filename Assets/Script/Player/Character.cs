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
    //캐릭터의 스탯지정
    //예시를 위해 값은 무작위로 넣음
    public GameObject LevepUpUI;

    private int mDamage = 10;              //피해량
    private int mProjectileSpeed = 1;     //투사체 속도
    private int mDuration = 3;            //지속 시간
    private int mAttackRange = 1;         //공격범위
    private int mCooldown = 3;            //쿨타임
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
        GetWeapon(0);
        GetWeapon(1);
        GetAccessory(0);
        GetAccessory(1);

    }
    //버튼 지우면 삭제 예정
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
    }
    public void LevelUp()
    {
        mLevel++;
        // 게임 일시정지

        var pickUps = RandomPickUpSystem.RandomPickUp(this);
        LevepUpUI.GetComponent<LevelUpUIManager>().LoadLevelUpUI(CharacterStats, pickUps, Weapons, Accessories);
        // 게임 재개

    }
    public void UpdateLuck(float luck)
    {
        RandomPickUpSystem.UpdateWeaponPickUpList(this);
        RandomPickUpSystem.UpdateAccessoryPickUpList(this);
    }
    //Get,Set함수 자동 구현
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
