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
        Debug.Log(transform.position);
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
                // 화면 밖으로 나가면 삭제 + 관통
            }
            timer = 0;
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
            else { obj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90); }
        }
        //이동 방향을 가져옴
        if (GameManager.instance.player.PreMovement == Vector2.zero){ obj.GetComponent<SpriteRenderer>().flipY = true; return Vector3.right;}
        else if (GameManager.instance.player.Movement == Vector2.zero & GameManager.instance.player.PreMovement != Vector2.zero) { return GameManager.instance.player.PreMovement;}
        else { return GameManager.instance.player.Movement; }
    }
}
