using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    float timer = 0;
    bool useKnife = false;
    int touch = 0;
    int touchLimit;
    void Update()
    {
        if (!useKnife) return;  //knife 사용 안할 때 Update를 안 함
        if(touchLimit <= touch)
        {
            Destroy(this.gameObject);
        }
    }
    public void FireKnife(GameObject objPre)
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
                    spawnPosition.y -= ((int)objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Area)] * (i-1)) / 2;
                    spawnPosition.y += i * (int)objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Area)];
                }
                newobs.transform.position = spawnPosition; //시작 위치
                newobs.GetComponent<Knife>().useKnife = true;
                newobs.GetComponent<Knife>().touchLimit = (int)objPre.GetComponent<Weapon>().WeaponTotalStats[(int)Enums.WeaponStat.Piercing];
                Rigidbody2D rb = newobs.GetComponent<Rigidbody2D>();
                rb.velocity = setDirection(newobs) * newobs.GetComponent<Weapon>().WeaponTotalStats[(int)Enums.WeaponStat.ProjectileSpeed];
            }
            timer = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            touch++;
        }
    }
    private Vector3 setDirection(GameObject obj) {
        //칼날 방향
        if (GameManager.instance.player.Movement.x > 0)
        {
            obj.GetComponent<SpriteRenderer>().flipY = true;
            if (GameManager.instance.player.Movement.y > 0) { obj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 135); }
            else if (GameManager.instance.player.Movement.y < 0) { obj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 45); }
            else { obj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90); }
        }
        else if (GameManager.instance.player.Movement.x < 0)
        {
            obj.GetComponent<SpriteRenderer>().flipY = false;
            if (GameManager.instance.player.Movement.y > 0) { obj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 45); }
            else if (GameManager.instance.player.Movement.y < 0) { obj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 135); }
            else { obj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90); }
        }
        else
        {
            if (GameManager.instance.player.Movement.y > 0) { obj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0); }
            else if (GameManager.instance.player.Movement.y < 0) { obj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180); }
            else { obj.GetComponent<Transform>().rotation = Quaternion.FromToRotation(Vector3.up, GameManager.instance.player.PreMovement); }
        }
        //이동 방향을 가져옴
        if (GameManager.instance.player.PreMovement == Vector2.zero){ obj.GetComponent<SpriteRenderer>().flipY = true; return Vector3.right;}
        else if (GameManager.instance.player.Movement == Vector2.zero & GameManager.instance.player.PreMovement != Vector2.zero) { return GameManager.instance.player.PreMovement;}
        else { return GameManager.instance.player.Movement; }
    }
}
