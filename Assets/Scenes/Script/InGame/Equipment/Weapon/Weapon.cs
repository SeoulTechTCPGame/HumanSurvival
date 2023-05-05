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
    public int WeaponLevel = 1;
    public int WeaponMaxLevel;
    public bool Mastered = false;

    public bool bEvolution = false;
    private float enemyHealth;

    private float[] WeaponStats;
    public float[] weaponTotalStats;//Might,Cooldown,ProjectileSpeed, Duration, Amount,AmountLimit,Piercing,Area,MaxLevel

    private PoolManager pool;
    public void WeaponDefalutSetting(int weaponIndex=0)
    {
        this.WeaponIndex = weaponIndex;
        this.WeaponStats = Enumerable.Range(0, EquipmentData.defaultWeaponStats.GetLength(1)).Select(x => EquipmentData.defaultWeaponStats[weaponIndex, x]).ToArray();
        WeaponLevel = 1;
        WeaponMaxLevel = (int)WeaponStats[(int)Enums.WeaponStat.MaxLevel];
        weaponTotalStats = WeaponStats;
        AttackCalculation();
    }
    public void Upgrade()
    {
        WeaponLevel++;
        foreach ((var statIndex, var data) in EquipmentData.WeaponUpgrade[WeaponIndex][WeaponLevel])
        {
            WeaponStats[statIndex] += data;
        }
        evolution();
    }
    public bool IsMaster()
    {
        return WeaponLevel == WeaponMaxLevel;
    }
    public bool isEvoluction()
    {
        return bEvolution;
    }
    private void evolution()
    {
        if (!IsMaster())
            return;
        var equipManageSys = GameManager.instance.equipManageSys;
        int evoPairAccIndex = EquipmentData.EvoWeaponNeedAccIndex[WeaponIndex];
        if (evoPairAccIndex < 0)    // 짝이 되는 악세서리의 index = -1 -> 짝이 무기인 경우
            evolutionException(equipManageSys);
        else if (equipManageSys.HasAcc(evoPairAccIndex))
            bEvolution = true;

        if (bEvolution)
            EvolutionProcess();
    }
    private void evolutionException(EquipmentManagementSystem equipManageSys)     // 진화에 필요한 짝이 악세서리가 아닌 무기인 경우(예시 - 비둘기, 흑비둘기)
    {
        var evoPairWeaponIndex = EquipmentData.EvoWeaponNeedWeaponIndex[WeaponIndex];
        if (equipManageSys.HasWeapon(evoPairWeaponIndex) && equipManageSys.Weapons[equipManageSys.TransWeaponIndex[evoPairWeaponIndex]].IsMaster())
            bEvolution = equipManageSys.Weapons[equipManageSys.TransWeaponIndex[evoPairWeaponIndex]].bEvolution = true;
    }
    public void EvolutionProcess()
    {
        switch (WeaponIndex)
        {
            case 0:     // Whip
                break;
            case 1:     // MagicWand
                break;
            case 2:     // Knife
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
            {
                var equipManageSys = GameManager.instance.equipManageSys;
                var pairWeapon = equipManageSys.Weapons[equipManageSys.TransWeaponIndex[EquipmentData.EvoWeaponNeedWeaponIndex[WeaponIndex]]];
                GetComponent<Peachone>().EvolutionProcess(equipManageSys.skillFiringSystem.Birds[2], pairWeapon);
                pairWeapon.GetComponent<EbonyWings>().EvolutionProcess();
                break;
            }
            case 10:    // EbonyWings
            {
                var equipManageSys = GameManager.instance.equipManageSys;
                var pairWeapon = equipManageSys.Weapons[equipManageSys.TransWeaponIndex[EquipmentData.EvoWeaponNeedWeaponIndex[WeaponIndex]]];
                pairWeapon.GetComponent<Peachone>().EvolutionProcess(equipManageSys.skillFiringSystem.Birds[2], this);
                GetComponent<EbonyWings>().EvolutionProcess();
                break;
            }
            case 11:    // Runetracer
                break;
            case 12:   // LightningRing
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("DestructibleObj"))
        {
            if (col.gameObject.TryGetComponent(out DestructibleObject destructible))
            {
                destructible.TakeDamage(weaponTotalStats[((int)Enums.WeaponStat.Might)], WeaponIndex);

            }
        }
        if (col.gameObject.tag == "Monster")
        {
            col.gameObject.GetComponent<Enemy>().TakeDamage(WeaponTotalStats[((int)Enums.WeaponStat.Might)], WeaponIndex);
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
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boundary")
        {
            Destroy(this.gameObject);
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
        weaponTotalStats[((int)Enums.WeaponStat.Might)] = WeaponStats[((int)Enums.WeaponStat.Might)] * GameManager.instance.CharacterStats[(int)Enums.Stat.Might];
    }
    private void ProjectileSpeedCalculation()
    {
        weaponTotalStats[((int)Enums.WeaponStat.ProjectileSpeed)] = WeaponStats[((int)Enums.WeaponStat.ProjectileSpeed)] * GameManager.instance.CharacterStats[(int)Enums.Stat.ProjectileSpeed];
    }
    private void DurationCalculation()
    {
        weaponTotalStats[((int)Enums.WeaponStat.Duration)] = WeaponStats[((int)Enums.WeaponStat.Duration)] * GameManager.instance.CharacterStats[(int)Enums.Stat.Duration];
    }
    private void AttackRangeCalculation()
    {
        weaponTotalStats[((int)Enums.WeaponStat.Area)] = WeaponStats[((int)Enums.WeaponStat.Area)] * GameManager.instance.CharacterStats[(int)Enums.Stat.Area];
    }
    private void CooldownCalculation()
    {
        weaponTotalStats[((int)Enums.WeaponStat.Cooldown)] = WeaponStats[((int)Enums.WeaponStat.Cooldown)] * GameManager.instance.CharacterStats[(int)Enums.Stat.Cooldown];
    }
    private void CalculateNumberOfProjectiles()
    {
        weaponTotalStats[((int)Enums.WeaponStat.Amount)] = ((int)WeaponStats[((int)Enums.WeaponStat.Amount)]) + GameManager.instance.CharacterStats[(int)Enums.Stat.Amount];
    }
    //Get, Set
    public float[] WeaponTotalStats { get { return weaponTotalStats; } }
}
