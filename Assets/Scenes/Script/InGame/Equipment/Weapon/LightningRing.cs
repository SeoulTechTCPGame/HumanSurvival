using UnityEngine;
using System;      // array의 IndexOf 함수
using System.Linq; // array의 Max, Min 함수 

public class LightningRing : Weapon
{
    [SerializeField] AudioClip mFireClip;
    private GameObject mNewObj;
    private GameObject mEvoNewObj;
    private float mTimer = 0;
    private bool mbExist = false;
    private bool mbEvoExist = false;
    private Vector3[] mLightningPosition;
    private float mEvoTimer = 0;

    public override void Attack()
    {
        GameObject objPre;
        if (IsEvoluction())
        {
            objPre = SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex];
            mEvoTimer += Time.deltaTime;
        }
        else
            objPre = SkillFiringSystem.instance.weaponPrefabs[WeaponIndex];
        mTimer += Time.deltaTime;
        if (!mbExist && mTimer > WeaponTotalStats[((int)Enums.EWeaponStat.Cooldown)])
        {
            objPre.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            mNewObj = new GameObject("Lightnings");
            mNewObj.transform.parent = GameObject.Find("SkillFiringSystem").transform;
            SoundManager.instance.PlayOverlapSound(mFireClip);

            mLightningPosition = FindDenseClusterEnemy((int)WeaponTotalStats[((int)Enums.EWeaponStat.Amount)]);
            for (int i = 0; i < WeaponTotalStats[((int)Enums.EWeaponStat.Amount)]; i++)
            {
                GameObject lightning = Instantiate(objPre, GameObject.Find("Lightnings").transform);
                lightning.GetComponent<CircleCollider2D>().radius = 0.15f * (float)Math.Sqrt(WeaponTotalStats[((int)Enums.EWeaponStat.Area)]);
                lightning.transform.position = mLightningPosition[i] + Vector3.up * 10;
            }
            mbExist = true;
            mEvoTimer = 0;
            mTimer = 0;
        }
        else if (mbExist && mTimer > 0.3f)
        {
            Destroy(mNewObj);
            mbExist = false;
            mTimer = 0;
        }

        if (!mbEvoExist && mEvoTimer > WeaponTotalStats[((int)Enums.EWeaponStat.Cooldown)] / 2)
        {
            objPre.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
            mEvoNewObj = new GameObject("EvoLightnings");
            mEvoNewObj.transform.parent = GameObject.Find("SkillFiringSystem").transform;
            SoundManager.instance.PlayOverlapSound(mFireClip);

            for (int i = 0; i < WeaponTotalStats[((int)Enums.EWeaponStat.Amount)]; i++)
            {
                GameObject lightning = Instantiate(objPre, GameObject.Find("EvoLightnings").transform);
                lightning.GetComponent<CircleCollider2D>().radius = 0.15f * (float)Math.Sqrt(WeaponTotalStats[((int)Enums.EWeaponStat.Area)]);
                lightning.transform.position = mLightningPosition[i] + Vector3.up * 10;
            }
            mbEvoExist = true;
            mEvoTimer = 0;
        }
        else if (mbEvoExist && mEvoTimer > 0.3f)
        {
            Destroy(mEvoNewObj);
            mbEvoExist = false;
            mEvoTimer = 0;
        }
    }
    private Vector3[] FindDenseClusterEnemy(int Amount)
    {
        Collider2D[] enemies = Physics2D.OverlapAreaAll(GameManager.instance.Player.transform.position + Vector3.left * 15 + Vector3.up * 8, GameManager.instance.Player.transform.position + Vector3.right * 15 + Vector3.down * 8, LayerMask.GetMask("Monster"));
        Vector3[] results = new Vector3[Amount];
        int[] enemiesNumber = new int[enemies.Length];
        int count = 0;
        foreach (Collider2D enemy in enemies)
        {
            enemiesNumber[count++] = Physics2D.OverlapCircleAll(enemy.transform.position, 0.15f * (float)Math.Sqrt(WeaponTotalStats[((int)Enums.EWeaponStat.Area)])).Length;
        }
        count = 0;
        if (enemiesNumber.Length == 0)
        {
            return results;
        }
        results[count++] = enemies[Array.IndexOf(enemiesNumber, enemiesNumber.Max())].transform.position;
        enemiesNumber[Array.IndexOf(enemiesNumber, enemiesNumber.Max())] = 0;
        for(int i = 0; i < enemies.Length; i++)
        {
            if (count == Amount) break;
            float distance = Vector3.Distance(results[count - 1], enemies[Array.IndexOf(enemiesNumber, enemiesNumber.Max())].transform.position);
            if (distance > 5f)
            {
                results[count++] = enemies[Array.IndexOf(enemiesNumber, enemiesNumber.Max())].transform.position;
                enemiesNumber[Array.IndexOf(enemiesNumber, enemiesNumber.Max())] = 0;
            }
            else
            {
                enemiesNumber[Array.IndexOf(enemiesNumber, enemiesNumber.Max())] = 0;
            }
        }
        
        return results;
    }
}