using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MagicWand : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Vector3 magicWandTransition;

    GameObject target;
    float timer = 0;
    bool useWand = false;
    int touch = 0;
    int touchLimit;

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
        if (target != null)
        {
            if (timer > objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Cooldown)])
            {
                for (int i = 0; i < objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Amount)]; i++)
                {
                    GameObject newobs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);   //skillFiringSystem에서 프리팹 가져오기
                    newobs.transform.position = GameManager.instance.player.transform.position;
                    newobs.GetComponent<MagicWand>().useWand = true;
                    Rigidbody2D rb = newobs.GetComponent<Rigidbody2D>();
                    rb.velocity = setDirection(newobs) * newobs.GetComponent<Weapon>().WeaponTotalStats[(int)Enums.WeaponStat.ProjectileSpeed];
                }
                timer = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            touch++;
        }
    }
    private Vector3 setDirection(GameObject obj)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster"); // Find all game objects with the "Enemy" tag.
        float closestDistance = Mathf.Infinity;

        Vector3 direction = Vector3.one;

        //적 탐색
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(GameManager.instance.player.transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = enemy;
            }
        }
        //벡터  설정
        return direction;
    }
}