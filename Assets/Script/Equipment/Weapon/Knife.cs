using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    float timer = 0;
    bool useKnife = false;
    Vector3 knifeTransform;
     void Update()
    {
        if (!useKnife) return;  //knife 사용 안할 때 Update를 안 함
        transform.position += knifeTransform * GameManager.instance.character.GetComponent<Character>().Weapons[GameManager.instance.character.GetComponent<Character>().TransWeaponIndex[2]].WeaponTotalStats[((int)Enums.WeaponStat.ProjectileSpeed)] * Time.deltaTime;
    }

    //ToDo: totalAttackRange 적용하기
    public void FireKnife(GameObject objPre)
    {
        timer += Time.deltaTime;
        if (timer > objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Cooldown)])
        {
            for (int i = 0; i < objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Amount)]; i++)
            {
                GameObject newobs = Instantiate(objPre);   //skillFiringSystem에서 프리팹 가져오기
                newobs.transform.parent = GameObject.Find("SkillFiringSystem").transform;
                newobs.transform.position = GameManager.instance.player.transform.position; //시작 위치
                newobs.GetComponent<Knife>().knifeTransform = setDirection(newobs);
                newobs.GetComponent<Knife>().useKnife = true;
                //무기가 몬스터와 충돌이 일어나거나, 화면 밖으로 나가면 삭제 + 관통
            }
            timer = 0;
        }
    }
    private Vector3 setDirection(GameObject obj) {
        if (GameManager.instance.player.Movement.x > 0)
        {
            obj.GetComponent<Transform>().localScale = new Vector3(2, -2, 1);
            return Vector3.right;
        }
        else if (GameManager.instance.player.Movement.x < 0)
        {
            obj.GetComponent<Transform>().localScale = new Vector3(2, 2, 1);
            return Vector3.left;
        }
        if (GameManager.instance.player.Movement.y > 0)
        {
            obj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
            return Vector3.up;
        }
        else if (GameManager.instance.player.Movement.y < 0)
        {
            obj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180);
            return Vector3.down;
        }
        else
        {
            return Vector3.zero;    //안 움직이는 이유
        }
    }
}
