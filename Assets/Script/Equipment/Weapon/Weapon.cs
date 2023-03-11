using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;
using System.Linq;

public class Weapon : MonoBehaviour
{
    //������ ���� ����
    //���ø� ���� ���� �������� ����
    public int WeaponIndex;
    public int WeaponLevel;
    public int WeaponMaxLevel;
    public bool Mastered = false;
    
    private float enemyHealth;

    private float[] WeaponStats;
    public float[] weaponTotalStats;//Might,Cooldown,ProjectileSpeed, Duration, Amount,AmountLimit,Piercing,Area,MaxLevel

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
    //��ȭ ���� ���� Ȯ��
    private void isEvoluction()
    {
        bool evoluction = IsMaster();
        //knife �ȸ� ��ȣ�� ��ű� ���� Ȯ��
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("DestructibleObj"))
        {
            if (col.gameObject.TryGetComponent(out DestructibleObject destructible))
            {
                destructible.TakeDamage(weaponTotalStats[((int)Enums.WeaponStat.Might)]);

            }
        }
        if (col.gameObject.tag == "Monster")
        {
            col.gameObject.GetComponent<Enemy>().TakeDamage(WeaponTotalStats[((int)Enums.WeaponStat.Might)]);
            
        }
        // for(int i = 0; i < col.Length; i++)
        // {
        //     IDamageable e = col[i].GetComponent<IDamageable>();
        //     if(e != null)
        //     {
        //         e.TakeDamage(GameManager.instance.player.GetComponent<Character>().Weapons[GameManager.instance.player.GetComponent<Character>().TransWeaponIndex[WeaponIndex]].GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Might)]);
        //     }
        // }
    }


    //�Ʒ� ����� �ѹ��� �ϱ�
    //ToDo: ������ �Ҷ����� �����ϴ� ������ ����
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
