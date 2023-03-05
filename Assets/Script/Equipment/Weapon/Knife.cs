using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    float timer = 0;

    Weapon Stat;

    private Vector3 direction;

    private void Update()
    {
        transform.position = transform.position + direction * Stat.WeaponTotalStats[((int)Enums.WeaponStat.ProjectileSpeed)] * Time.deltaTime;
    }

    public void Shoot(Vector3 direct)
    {
        direction = direct;
    }
    
    //ToDo: totalAttackRange 적용하기
    public void FireKnife(GameObject objPre)
    {
        Debug.Log("Knife");
        float timediff = objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Cooldown)];
        timer += Time.deltaTime;
        if (timer > timediff)
        {
            for (int i = 0; i <= objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Amount)]; i++)
            {
                GameObject newobs = Instantiate(objPre);   //weapon의 index와 monsterPool의 index는 값게 설정
                newobs.transform.position = GameManager.instance.player.transform.position;
                newobs.transform.parent = transform;
                newobs.GetComponent<Knife>().Shoot(GameManager.instance.player.GetComponent<PlayerMovement>().Movement);
                timer = 0;
                Destroy(newobs, objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Duration)]);  //지속 시간 지나면 삭제
            }
        }
    }
}
