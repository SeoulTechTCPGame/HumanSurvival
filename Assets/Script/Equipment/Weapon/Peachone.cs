using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peachone : MonoBehaviour
{
    [SerializeField] Animator animator;
    public Weapon ownWeapon;

    float timer = 0;
    bool useWand = false;
    bool projectileDirUp = true; // 위 아래 번갈아가며
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
    public void FirePeachone(GameObject objPre)
    {
        timer += Time.deltaTime;
        if (timer > ownWeapon.WeaponTotalStats[((int)Enums.WeaponStat.Cooldown)])
        {
            for (int i = 0; i < ownWeapon.WeaponTotalStats[((int)Enums.WeaponStat.Amount)]; i++)
            {
                //무기 세팅
                GameObject newobs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);
                newobs.transform.position = GameManager.instance.peachone.transform.position;

                Peachone newPeachone = newobs.GetComponent<Peachone>();
                newPeachone.useWand = true;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                //무기가 바라보는 방향 조절
                newobs.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);     //180은 이 스프라이트에 맞게 보정한 값
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
}