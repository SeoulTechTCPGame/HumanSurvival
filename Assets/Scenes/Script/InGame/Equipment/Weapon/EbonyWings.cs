using Enums;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.U2D.Path;
using UnityEngine;

public class EbonyWings : MonoBehaviour
{
    [SerializeField] Animator animator;
    public GameObject Bird = null;
    public Transform StartPoint = null;
    public Transform EndPoint = null;
    public Vector3 ControlPoint;
    private bool mbHolding = false;

    float Timer = 100;
    bool UseEbony = false;
    private void FixedUpdate()
    {
        if (!UseEbony || mbHolding)
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
    public void Fire(GameObject objPre, Transform dstTransform, Vector3 secondPoint, Transform sourceP)
    {
        GameObject newobs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);   //skillFiringSystem에서 프리팹 가져오기
        var newObjEbonyWings = newobs.GetComponent<EbonyWings>();
        newObjEbonyWings.StartPoint = sourceP;
        newObjEbonyWings.ControlPoint = secondPoint;
        newObjEbonyWings.EndPoint = dstTransform;
        newObjEbonyWings.UseEbony = true;
        newobs.transform.position = sourceP.position; //시작 위치
    }
    public void CreateCircle(GameObject peachPre, GameObject bounderyPre, Weapon EbonyWings)
    {
        Timer += Time.deltaTime;
        if (Timer > EbonyWings.WeaponTotalStats[((int)Enums.WeaponStat.Cooldown)])
        {
            bounderyPre.GetComponent<PeachBoundery>().CreateCircle(peachPre, bounderyPre, false, EbonyWings, StartPoint);
            Timer = 0;
            peachPre.transform.localScale = transform.localScale * EbonyWings.WeaponTotalStats[((int)Enums.WeaponStat.Area)];
        }
    }
    public void CreateEvoCircle(GameObject peachPre, GameObject bounderyPre, Weapon peachone)
    {
        Timer += Time.deltaTime;
        if (Timer > peachone.WeaponTotalStats[((int)Enums.WeaponStat.Cooldown)])
        {
            bounderyPre.GetComponent<EvoPeachBoundery>().CreateCircle(peachPre, bounderyPre, false, peachone, StartPoint);
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
    public void SpawnBlackBird(GameObject bird)
    {
        Bird = Instantiate(bird, GameObject.Find("SkillFiringSystem").transform);
        var newObjBird = Bird.GetComponent<Bird>();
        newObjBird.PlayerTransform = GameManager.instance.player.transform;
        StartPoint = newObjBird.transform;
    }
    public void EvolutionProcess()
    {
        StartPoint = Bird.GetComponent<Bird>().transform;
    }
}