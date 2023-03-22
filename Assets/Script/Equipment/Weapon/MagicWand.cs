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

    private void FixedUpdate()
    {
        GetNearest();
        if (!useWand) return;  //MagicWand 사용 안할 때 Update를 안 함
        //targets = Physics2D.CircleCastAll(GameManager.instance.player.transform.position, scanRange, Vector2.zero, 0, targetLayer); //상대 찾기 캐스팅 시작 위치,원의 반지름, 캐스팅 방향, 캐스팅 길이, 대상 레이어
        transform.position += magicWandTransition * Time.deltaTime;
        if (touch == 2) //TODO: 진화 시로 변경
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
                    GameObject newobs = Instantiate(objPre, GameManager.instance.player.transform.position, Quaternion.identity);   //skillFiringSystem에서 프리팹 가져오기
                    newobs.GetComponent<MagicWand>().useWand = true;
                    newobs.transform.parent = GameObject.Find("SkillFiringSystem").transform;
                    Vector3 direction = (target.transform.position - GameManager.instance.player.transform.position).normalized;
                    newobs.GetComponent<MagicWand>().magicWandTransition = direction * GameManager.instance.character.GetComponent<Character>().Weapons[GameManager.instance.character.GetComponent<Character>().TransWeaponIndex[1]].WeaponTotalStats[((int)Enums.WeaponStat.ProjectileSpeed)];
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
    void GetNearest()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster"); // Find all game objects with the "Enemy" tag.
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(GameManager.instance.player.transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = enemy;
            }
        }
    }
}