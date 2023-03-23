using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MagicWand : MonoBehaviour
{
    public Vector3 closestEnemyPosition;
    float closestDistance = Mathf.Infinity;
    EquipmentManagementSystem data;

    float timer = 0;
    bool useWand = false;
    int touch = 0;
    int touchLimit;

    private void Update()
    {
        UpdateTarget();
    }

    private void FixedUpdate()
    {
        if (!useWand) return;  //MagicWand 사용 안할 때 Update를 안 함
        if (touchLimit <= touch)
        {
            Destroy(this.gameObject);
        }
    }
    public void FireMagicWand(GameObject objPre)
    {
        timer += Time.deltaTime;
        if (timer > objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Cooldown)])
        {
             for (int i = 0; i < objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Amount)]; i++)
             {
                GameObject newobs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);
                newobs.transform.position = GameManager.instance.player.transform.position;
                newobs.GetComponent<MagicWand>().useWand = true;
                newobs.GetComponent<MagicWand>().touchLimit = (int)objPre.GetComponent<Weapon>().WeaponTotalStats[(int)Enums.WeaponStat.Piercing];
                Rigidbody2D rb = newobs.GetComponent<Rigidbody2D>();
                Debug.Log(UpdateTarget());
                rb.velocity = UpdateTarget().normalized * newobs.GetComponent<Weapon>().WeaponTotalStats[(int)Enums.WeaponStat.ProjectileSpeed];
             }
             timer = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            touch++;
        }
    }
    private Vector3 UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster");

        //적 탐색
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(GameManager.instance.player.transform.position, enemy.transform.position);

            if (distance < closestDistance) //몬스터와 주인공 사이가 가장 짧은 거리 업데이트
            {
                closestDistance = distance;
                closestEnemyPosition = enemy.transform.position;
            }
            else if (distance > closestDistance)    //가장 짧은 거리이내에 적이 없으면 다시 초기화
            {
                closestDistance = distance;
            }
        }
        return closestEnemyPosition;
    }
}