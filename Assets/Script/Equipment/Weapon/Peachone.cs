using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peachone : MonoBehaviour
{
    [SerializeField] Animator animator;
    public Weapon ownWeapon;

    float timer = 0;
    bool usePeach = false;
    bool projectileDirUp = true; // 위 아래 번갈아가며
    int touch = 0;
    int touchLimit= 60;
    private void Start()
    {
        ownWeapon = GetComponent<Weapon>();
        //animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        //if (!usePeach) return;

        //if (touchLimit <= touch)
        //{
        //    animator.SetBool("Hit", true);
        //    Destroy(this.gameObject);
        //}
    }
    public void FirePeachone(GameObject objPre)
    {
        timer += Time.deltaTime;
        if (timer > objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Cooldown)])
        {
            for (int i = 0; i < objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Amount)]; i++)
            {
                GameObject newobs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);   //skillFiringSystem에서 프리팹 가져오기
                Vector3 spawnPosition = GameManager.instance.player.transform.position;
                if (i > 1) //여러 개 동시 발사 시 시작 위치를 다르게 설정
                {
                    spawnPosition.y -= ((int)objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Area)] * (i - 1)) / 2;
                    spawnPosition.y += i * (int)objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Area)];
                }
                newobs.transform.position = spawnPosition; //시작 위치
                newobs.GetComponent<Peachone>().usePeach = true;
                newobs.GetComponent<Peachone>().touchLimit = (int)objPre.GetComponent<Weapon>().WeaponTotalStats[(int)Enums.WeaponStat.Piercing];
                Rigidbody2D rb = newobs.GetComponent<Rigidbody2D>();
            }
            timer = 0;
        }
    }
    public void CreateCircle(GameObject peachPre, GameObject bounderyPre)
    {
        timer += Time.deltaTime;
        if (timer > peachPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Cooldown)])
        {
            bounderyPre.GetComponent<PeachBoundery>().CreateCircle(peachPre, bounderyPre, true);
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
}