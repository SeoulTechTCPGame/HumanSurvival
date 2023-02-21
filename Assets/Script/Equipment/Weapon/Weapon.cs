using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;
using System.Linq;

public class Weapon : MonoBehaviour
{
    //무기의 스탯 지정
    //예시를 위해 값은 무작위로 넣음
    public int WeaponIndex;
    public int WeaponLevel;
    public int WeaponMaxLevel;
    public bool Mastered = false;

    private int damage = 10;                //피해량
    private int projectileSpeed = 1;        //투사체 속도
    private int duration = 3;               //지속 시간
    private int attackRange = 1;            //공격범위
    private int cooldown = 3;               //쿨타임
    private int numberOfProjectiles = 1;    //투사체 수
    private int totalspeed;                 //총 속도

    public float[] WeaponStats;
    public EquipmentData EquipmentData;

    public void WeaponSetting(int weaponIndex=0)
    {
        this.WeaponIndex = weaponIndex;
        this.WeaponStats = Enumerable.Range(0, EquipmentData.defaultWeaponStats.GetLength(1)).Select(x => EquipmentData.defaultWeaponStats[weaponIndex, x]).ToArray();

        WeaponLevel = 1;
        WeaponMaxLevel = (int)WeaponStats[(int)Enums.WeaponStat.MaxLevel];
    }

    private void Update()
    {
        transform.position = transform.position + Vector3.right * totalspeed * Time.deltaTime;
    }
    public void Shoot(int speed)
    {
        totalspeed = speed;
    }
    public void Upgrade()
    {
        WeaponLevel++;
        foreach ((var statIndex, var data) in EquipmentData.WeaponUpgrade[WeaponIndex][WeaponLevel])
        {
            WeaponStats[statIndex] += data;
        }
    }
    public bool IsMaster()
    {
        return WeaponLevel == WeaponMaxLevel;
    }
    //Get,Set함수 자동 구현
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public int ProjectileSpeed
    {
        get { return projectileSpeed; }
        set { projectileSpeed = value; }
    }
    public int Duration
    {
        get { return duration; }
        set { duration = value; }
    }
    public int AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }
    public int Cooldown
    {
        get { return cooldown; }
        set { cooldown = value; }
    }
    public int NumberOfProjectiles
    {
        get { return numberOfProjectiles; }
        set { numberOfProjectiles = value; }
    }

    
}
