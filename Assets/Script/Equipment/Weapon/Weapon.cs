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
    
    private float enemyHealth;

    private float[] WeaponStats;
    private float[] weaponTotalStats;//Might,Cooldown,ProjectileSpeed, Duration, Amount,AmountLimit,Piercing,Area,MaxLevel

    private EquipmentData EquipmentData;
    private PoolManager pool;
    
    public void WeaponDefalutSetting(int weaponIndex=0)
    {
        this.WeaponIndex = weaponIndex;
        this.WeaponStats = Enumerable.Range(0, EquipmentData.defaultWeaponStats.GetLength(1)).Select(x => EquipmentData.defaultWeaponStats[weaponIndex, x]).ToArray();

        WeaponLevel = 1;
        WeaponMaxLevel = (int)WeaponStats[(int)Enums.WeaponStat.MaxLevel];
        weaponTotalStats = WeaponStats;
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
                destructible.TakeDamage(weaponTotalStats[((int)Enums.WeaponStat.Might)]);

            }
        }
        if (col.gameObject.tag == "Monster")
        {
            col.gameObject.GetComponent<Enemy>().TakeDamage(GameManager.instance.player.GetComponent<Character>().Weapons[WeaponIndex].GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Might)]);
            Debug.Log("Monster Hit");
        }
    }

    //아래 계산을 한번에 하기
    //ToDo: 레벨업 할때마다 갱신하는 것으로 변경
    private void AttackCalculation()
    {
        DamageCalculation();
        ProjectileSpeedCalculation();
        DurationCalculation();
        AttackRangeCalculation();
        CooldownCalculation();
        CalculateNumberOfProjectiles();
    }
    private void DamageCalculation()
    {
        weaponTotalStats[((int)Enums.WeaponStat.Might)] = WeaponStats[((int)Enums.WeaponStat.Might)] * (1 + GameManager.instance.player.GetComponent<Character>().Damage / 100);
    }
    private void ProjectileSpeedCalculation()
    {
        weaponTotalStats[((int)Enums.WeaponStat.ProjectileSpeed)] = WeaponStats[((int)Enums.WeaponStat.ProjectileSpeed)] * (1 + GameManager.instance.player.GetComponent<Character>().ProjectileSpeed / 100);
    }
    private void DurationCalculation()
    {
        weaponTotalStats[((int)Enums.WeaponStat.Duration)] = WeaponStats[((int)Enums.WeaponStat.Duration)] * (1 + GameManager.instance.player.GetComponent<Character>().Duration / 100);
    }
    private void AttackRangeCalculation()
    {
        weaponTotalStats[((int)Enums.WeaponStat.Area)] = WeaponStats[((int)Enums.WeaponStat.Area)] * (1 + GameManager.instance.player.GetComponent<Character>().AttackRange / 100);
    }
    private void CooldownCalculation()
    {
        weaponTotalStats[((int)Enums.WeaponStat.Cooldown)] = WeaponStats[((int)Enums.WeaponStat.Cooldown)] * (1 + GameManager.instance.player.GetComponent<Character>().Cooldown / 100);
    }
    private void CalculateNumberOfProjectiles()
    {
        weaponTotalStats[((int)Enums.WeaponStat.Amount)] = ((int)WeaponStats[((int)Enums.WeaponStat.Amount)]) + GameManager.instance.player.GetComponent<Character>().NumberOfProjectiles;
    }
    //Get, Set
    public float[] WeaponTotalStats { get { return weaponTotalStats; } }
}
