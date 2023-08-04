using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int WeaponIndex;
    public int WeaponLevel = 1;
    public int WeaponMaxLevel;
    public bool BEvolution = false;
    public float[] WeaponTotalStats; // Might,Cooldown,ProjectileSpeed, Duration, Amount,AmountLimit,Piercing,Area,MaxLevel
    protected int mTouch = 0;
    [SerializeField] AudioClip mClip;
    private float[] mWeaponStats;
    private float mCriticalRate = 10;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("DestructibleObj"))
        {
            if (col.gameObject.TryGetComponent(out DestructibleObject destructible))
            {
                destructible.TakeDamage(WeaponTotalStats[(int)Enums.EWeaponStat.Might], WeaponIndex);
                SoundManager.instance.PlayOverlapSound(mClip);
            }
        }
        if (col.gameObject.tag == "Monster")
        {
            float power = WeaponTotalStats[(int)Enums.EWeaponStat.Might];
            if (WeaponIndex == 0 && BEvolution)
            {
                power = UnityEngine.Random.Range(0, 101) < mCriticalRate * GameManager.instance.CharacterStats[(int)Enums.EStat.Luck] ? power * 2 : power;
                GameManager.instance.Character.RestoreHealth(8);
            }
            else if(WeaponIndex == 3 && BEvolution)
            {
                power = UnityEngine.Random.Range(0, 101) < mCriticalRate * GameManager.instance.CharacterStats[(int)Enums.EStat.Luck] ? power * 2.5f : power;
                Debug.Log(power);
            }
            col.gameObject.GetComponent<Enemy>().TakeDamage(power, WeaponIndex);
            SoundManager.instance.PlayOverlapSound(mClip);
            if (WeaponIndex == 6 && BEvolution)
            {
                GameManager.instance.Character.RestoreHealth(1);
                GameManager.instance.EvoGralicRestoreCount++;
                if (GameManager.instance.EvoGralicRestoreCount <= 3600 && GameManager.instance.EvoGralicRestoreCount > 0)
                {
                    if (GameManager.instance.EvoGralicRestoreCount % 60 == 0)
                    {
                        SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex].GetComponent<Weapon>().mWeaponStats[(int)Enums.EWeaponStat.Might] += 1;
                        SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex].GetComponent<Weapon>().AttackCalculation();
                    }
                }
            }
        }
        if (col.gameObject.tag == "Monster")
        {
            mTouch++;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boundary")
        {
            Destroy(this.gameObject);
        }
    }
    public void WeaponDefalutSetting(int weaponIndex=0)
    {
        this.WeaponIndex = weaponIndex;
        this.mWeaponStats = Enumerable.Range(0, EquipmentData.DefaultWeaponStats.GetLength(1)).Select(x => EquipmentData.DefaultWeaponStats[weaponIndex, x]).ToArray();
        WeaponLevel = 1;
        BEvolution = false;
        WeaponMaxLevel = (int)mWeaponStats[(int)Enums.EWeaponStat.MaxLevel];
        WeaponTotalStats = Enumerable.Range(0, EquipmentData.DefaultWeaponStats.GetLength(1)).Select(x => EquipmentData.DefaultWeaponStats[weaponIndex, x]).ToArray(); ;
        AttackCalculation();
    }
    public void Upgrade()
    {
        WeaponLevel++;
        foreach ((var statIndex, var data) in EquipmentData.WeaponUpgrade[WeaponIndex][WeaponLevel])
        {
            mWeaponStats[statIndex] += data;
        }
        foreach (Weapon weapon in GameManager.instance.EquipManageSys.Weapons)
        {
            weapon.AttackCalculation();
        }
        Evolution();
    }
    public List<Tuple<int, float>> GetNextUpgradeData()
    {
        if (IsMaster())
            return null;

        int nextLevel = WeaponLevel + 1;
        List<Tuple<int, float>> ret = new List<Tuple<int, float>>();
        foreach ((var statIndex, var data) in EquipmentData.WeaponUpgrade[nextLevel][nextLevel])
        {
            ret.Add(new Tuple<int, float>(statIndex, data));
        }

        return ret;
    }
    public bool IsMaster()
    {
        return WeaponLevel == WeaponMaxLevel;
    }
    public bool IsEvoluction()
    {
        return BEvolution;
    }
    public virtual void Attack() { }
    public virtual void EvolutionProcess() 
    {
        SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex].GetComponent<Weapon>().WeaponIndex = WeaponIndex;
        SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex].GetComponent<Weapon>().WeaponLevel = WeaponLevel;
        SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex].GetComponent<Weapon>().WeaponMaxLevel = WeaponMaxLevel;
        SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex].GetComponent<Weapon>().BEvolution = BEvolution;
        SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex].GetComponent<Weapon>().WeaponTotalStats = WeaponTotalStats;
        SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex].GetComponent<Weapon>().mWeaponStats = mWeaponStats;
    }
    private void Evolution()
    {
        if (!IsMaster())
            return;
        Debug.Log("Evo");
        var equipManageSys = GameManager.instance.EquipManageSys;
        int evoPairAccIndex = EquipmentData.EvoWeaponNeedAccIndex[WeaponIndex];
        if (evoPairAccIndex < 0)    // 짝이 되는 악세서리의 index = -1 -> 짝이 무기인 경우
            EvolutionException(equipManageSys);
        else if (equipManageSys.HasAcc(evoPairAccIndex))
            BEvolution = true;

        if (BEvolution)
            EvolutionProcess();
    }
    private void EvolutionException(EquipmentManagementSystem equipManageSys)     // 진화에 필요한 짝이 악세서리가 아닌 무기인 경우(예시 - 비둘기, 흑비둘기)
    {
        var evoPairWeaponIndex = EquipmentData.EvoWeaponNeedWeaponIndex[WeaponIndex];
        if (equipManageSys.HasWeapon(evoPairWeaponIndex) && equipManageSys.Weapons[equipManageSys.TransWeaponIndex[evoPairWeaponIndex]].IsMaster())
            BEvolution = equipManageSys.Weapons[equipManageSys.TransWeaponIndex[evoPairWeaponIndex]].BEvolution = true;
    }
    //아래 계산을 한번에 하기
    //ToDo: 레벨업 할때마다 갱신하는 것으로 변경
    public void AttackCalculation()
    {
        DamageCalculation();
        ProjectileSpeedCalculation();
        DurationCalculation();
        AttackRangeCalculation();
        CooldownCalculation();
        CalculateNumberOfProjectiles();
        PiercingCalculation();
    }
    private void DamageCalculation()
    {
        WeaponTotalStats[((int)Enums.EWeaponStat.Might)] = mWeaponStats[((int)Enums.EWeaponStat.Might)] * GameManager.instance.CharacterStats[(int)Enums.EStat.Might];
    }
    private void ProjectileSpeedCalculation()
    {
        WeaponTotalStats[((int)Enums.EWeaponStat.ProjectileSpeed)] = mWeaponStats[((int)Enums.EWeaponStat.ProjectileSpeed)] * GameManager.instance.CharacterStats[(int)Enums.EStat.ProjectileSpeed];
    }
    private void DurationCalculation()
    {
        WeaponTotalStats[((int)Enums.EWeaponStat.Duration)] = mWeaponStats[((int)Enums.EWeaponStat.Duration)] * GameManager.instance.CharacterStats[(int)Enums.EStat.Duration];
    }
    private void AttackRangeCalculation()
    {
        WeaponTotalStats[((int)Enums.EWeaponStat.Area)] = mWeaponStats[((int)Enums.EWeaponStat.Area)] * GameManager.instance.CharacterStats[(int)Enums.EStat.Area];
    }
    private void CooldownCalculation()
    {
        WeaponTotalStats[((int)Enums.EWeaponStat.Cooldown)] = mWeaponStats[((int)Enums.EWeaponStat.Cooldown)] * GameManager.instance.CharacterStats[(int)Enums.EStat.Cooldown];
    }
    private void CalculateNumberOfProjectiles()
    {
        WeaponTotalStats[((int)Enums.EWeaponStat.Amount)] = ((int)mWeaponStats[((int)Enums.EWeaponStat.Amount)]) + GameManager.instance.CharacterStats[(int)Enums.EStat.Amount];
    }
    private void PiercingCalculation()
    {
        WeaponTotalStats[((int)Enums.EWeaponStat.Piercing)] = ((int)mWeaponStats[((int)Enums.EWeaponStat.Piercing)]);
    }
}