using UnityEngine;
using System;      // array의 IndexOf 함수
using System.Linq; // array의 Max, Min 함수 

public class LightningRing : Weapon
{
    private GameObject mNewObj;
    private float mTimer = 0;
    private bool mbExist = false;

    public override void Attack()
    {
        GameObject objPre;
        if (IsEvoluction())
            objPre = SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex];
        else
            objPre = SkillFiringSystem.instance.weaponPrefabs[WeaponIndex];
        mTimer += Time.deltaTime;
        if (!mbExist && mTimer > WeaponTotalStats[((int)Enums.EWeaponStat.Cooldown)])
        {
            mNewObj = new GameObject("Lightnings");
            mNewObj.transform.parent = GameObject.Find("SkillFiringSystem").transform;

            Vector3[] lightningPosition = FindDenseClusterEnemy((int)WeaponTotalStats[((int)Enums.EWeaponStat.Amount)]);
            for (int i = 0; i < WeaponTotalStats[((int)Enums.EWeaponStat.Amount)]; i++)
            {
                GameObject lightning = Instantiate(objPre, GameObject.Find("Lightnings").transform);
                lightning.GetComponent<CircleCollider2D>().radius = 0.15f * (float)Math.Sqrt(WeaponTotalStats[((int)Enums.EWeaponStat.Area)]);
                lightning.transform.position = lightningPosition[i] + Vector3.up * 10;
            }
            mbExist = true;
            mTimer = 0;
        }
        else if (mbExist && mTimer > 0.5f)
        {
            Destroy(mNewObj);
            mbExist = false;
            mTimer = 0;
        }
    }
    public override void EvolutionProcess() // 무기 진화시 한 번 호출됨
    {

    }
    private Vector3[] FindDenseClusterEnemy(int Amount)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster");
        Vector3[] results = new Vector3[Amount];
        int[] enemiesNumber = new int[enemies.Length];
        int count = 0;
        foreach (GameObject enemy in enemies)
        {
            enemiesNumber[count++] = Physics2D.OverlapCircleAll(transform.position, 1.5f * (float)Math.Sqrt(WeaponTotalStats[((int)Enums.EWeaponStat.Area)])).Length;
            //Debug.Log(Physics2D.OverlapCircleAll(transform.position, 1.5f * (float)Math.Sqrt(WeaponTotalStats[((int)Enums.EWeaponStat.Area)])).Length);
        }
        for(int i = 0; i < Amount; i++)
        {
            results[i] = enemies[Array.IndexOf(enemiesNumber, enemiesNumber.Max())].transform.position;
            enemiesNumber[Array.IndexOf(enemiesNumber, enemiesNumber.Max())] = 0;
        }
        
        return results;
    }
}