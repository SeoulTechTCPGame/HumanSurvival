using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gralic : SkillFiringSystem
{
    private int index = 7;
    private GameObject player;
    private float enemyHealth;
    private GameObject targetEnemy;
    // Start is called before the first frame update
    void Start()
    {
        float cooltime = weaponPrefabs[index].GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Cooldown)];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }

    private void OnCollisionStay2D(Collision2D col)
    {
     
        if (col.gameObject.tag == "Monster") { 
            enemyHealth = col.gameObject.GetComponent<Enemy>().health - weaponPrefabs[index].GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Might)];
            if (enemyHealth > 0)
            {
                // 몬스터 체력 반영
            }
            else
            {
                // 몬스터 사망
            }
            Debug.Log("Hit");
        }
    }

    public void Attack()
    {
        
    }
}
