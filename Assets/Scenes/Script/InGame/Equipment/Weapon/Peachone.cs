using Enums;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.U2D.Path;
using UnityEngine;

public class Peachone : MonoBehaviour
{
    [SerializeField] Animator animator;
    public Transform StartPoint = null;
    public Transform EndPoint = null;
    public Vector3 ControlPoint;
    private bool mbHolding = false;

    float Timer = 100;
    bool UsePeach = false;
    private void FixedUpdate()
    {
        if (!UsePeach || mbHolding)
        {
            return;
        }
        Timer += Time.deltaTime;

        transform.position = calculateBezierPoint();
        if (Timer > 1.1f)
        {
            mbHolding = true;
            Destroy(gameObject, 1f);
            animator.SetTrigger("Hold");
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }
    public void FirePeachone(GameObject objPre, Transform dstTransform, Vector3 p, Transform sourceP)
    {
        GameObject newobs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);   //skillFiringSystem에서 프리팹 가져오기
        var newObjPeachone = newobs.GetComponent<Peachone>();
        newObjPeachone.StartPoint = sourceP;
        newObjPeachone.ControlPoint = p;
        newObjPeachone.EndPoint = dstTransform;
        newObjPeachone.UsePeach = true;
        newobs.transform.position = sourceP.position; //시작 위치
    }
    public void CreateCircle(GameObject peachPre, GameObject bounderyPre, Weapon peachone)
    {
        Timer += Time.deltaTime;
        if (Timer > peachone.WeaponTotalStats[((int)Enums.WeaponStat.Cooldown)])
        {
            bounderyPre.GetComponent<PeachBoundery>().CreateCircle(peachPre, bounderyPre, true, peachone, StartPoint);
            Timer = 0;
            peachPre.transform.localScale = transform.localScale * peachone.WeaponTotalStats[((int)Enums.WeaponStat.Area)];
        }
    }
    private Vector3 calculateBezierPoint()
    {
        float u = 1f - Timer;
        float tt = Timer * Timer;
        float uu = u * u;
        Vector3 nowPos = uu * StartPoint.position;
        nowPos += 2f * u * Timer * ControlPoint;
        nowPos += tt * EndPoint.position;

        return nowPos;
    }
    public void SpawnBlueBird(GameObject bird)
    {
        GameObject newobs = Instantiate(bird, GameObject.Find("SkillFiringSystem").transform);
        var newObjBird = newobs.GetComponent<Bird>();
        newObjBird.PlayerTransform = GameManager.instance.player.transform;
        StartPoint = newObjBird.transform;
    }
}