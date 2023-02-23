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

    private float damage = 1; 
    private float projectileSpeed = 1; 
    private float duration = 3;
    private float attackRange = 1;
    private float cooldown = 3;
    private int numberOfProjectiles = 1;
    private float totalspeed;
    private Vector3 direction;

    public float[] WeaponStats;
    public EquipmentData EquipmentData;
    public PoolManager pool;

    private void Update()
    {
        transform.position = transform.position + direction * totalspeed * Time.deltaTime;
    }
    public void Shoot(float speed, Vector3 direct)
    {
        totalspeed = speed;
        direction = direct;
    }
    public void WeaponSetting(int weaponIndex=0)
    {
        this.WeaponIndex = weaponIndex;
        this.WeaponStats = Enumerable.Range(0, EquipmentData.defaultWeaponStats.GetLength(1)).Select(x => EquipmentData.defaultWeaponStats[weaponIndex, x]).ToArray();

        WeaponLevel = 1;
        WeaponMaxLevel = (int)WeaponStats[(int)Enums.WeaponStat.MaxLevel];
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
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("DestructibleObj"))
        {
            if (col.gameObject.TryGetComponent(out DestructibleObject destructible))
            {
                Debug.Log("사물 hit");
                destructible.TakeDamage(damage);

            }
        }
    }
    public void WhichWeapon()
    {
        switch (WeaponIndex)
        {
            case 0:     // Whip
                break;
            case 1:     // MagicWand
                break;
            case 2:     // Knife
                this.damage = EquipmentData.defaultWeaponStats[2, 0];
                this.cooldown = EquipmentData.defaultWeaponStats[2, 1];
                this.projectileSpeed = EquipmentData.defaultWeaponStats[2, 2];
                this.duration = EquipmentData.defaultWeaponStats[2, 3];
                this.numberOfProjectiles = ((int)EquipmentData.defaultWeaponStats[2, 4]);
                //투사체수 최대치
                //관통
                this.attackRange = EquipmentData.defaultWeaponStats[2, 7];  //임시
                break;
            case 3:     // Axe
                break;
            case 4:     // Cross
                break;
            case 5:     //KingBible
                break;
            case 6:     // FireWand
                break;
            case 7:     // Garlic
                break;
            case 8:     // SantaWater
                break;
            case 9:     // Peachone
                break;
            case 10:    // EbonyWings
                break;
            case 11:    // Runetracer
                break;
            case 12:   // LightningRing
                break;
        }
    }
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
        set { projectileSpeed = value; }
    }
    public float Duration
    {
        get { return duration; }
        set { duration = value; }
    }
    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }
    public float Cooldown
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
