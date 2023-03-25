using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MagicWand : MonoBehaviour
{
    [SerializeField] Animator animator;
    public Weapon ownWeapon;

    float timer = 0;
    bool useWand = false;
    int touch = 0;
    int touchLimit;
    private void Start()
    {
        ownWeapon = GetComponent<Weapon>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (!useWand) return;  //MagicWand 사용 안할 때 Update를 안 함
        if (touchLimit <= touch)
        {
            animator.SetBool("Hit", true);
            Destroy(this.gameObject);
        }
    }
    public void FireMagicWand(GameObject objPre)
    {
        timer += Time.deltaTime;
        if (timer > ownWeapon.WeaponTotalStats[((int)Enums.WeaponStat.Cooldown)])
        {
            for (int i = 0; i < ownWeapon.WeaponTotalStats[((int)Enums.WeaponStat.Amount)]; i++)
            {
                //무기 세팅
                GameObject newobs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);
                newobs.transform.position = GameManager.instance.player.transform.position;
                MagicWand newWand = newobs.GetComponent<MagicWand>();
                newWand.useWand = true;
                newWand.touchLimit = (int)ownWeapon.WeaponTotalStats[(int)Enums.WeaponStat.Piercing];
                Vector3 direction = FindClosestEnemyDirection();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                //무기가 바라보는 방향 조절
                newobs.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                //무기 발사
                Rigidbody2D rb = newobs.GetComponent<Rigidbody2D>();
                rb.velocity = direction * ownWeapon.WeaponTotalStats[((int)Enums.WeaponStat.ProjectileSpeed)];
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
    private GameObject FindClosestEnemy() 
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        //적 탐색
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance) //소환된 무기와 몬스터 가장 짧은 거리 업데이트
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }
        return closestEnemy;
    }
    public Vector3 FindClosestEnemyDirection()
    {
        GameObject closestEnemy = FindClosestEnemy();
        Debug.Log(closestEnemy);
        if (closestEnemy != null)
        {
            Vector3 direction = closestEnemy.transform.position - transform.position;
            return direction.normalized;
        }
        else
        {
            return Vector3.right;
        }
    }
}