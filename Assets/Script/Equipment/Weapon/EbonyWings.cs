using Enums;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.U2D.Path;
using UnityEngine;

public class EbonyWings : MonoBehaviour
{
    [SerializeField] Animator animator;
    public Weapon ownWeapon;
    public Transform StartPoint = null;
    public Transform EndPoint = null;
    public Vector3 ControlPoint;
    private Vector3 mDefaultScale = new Vector3(2, 2, 1);

    float Timer = 0;
    bool UseEbony = false;
    private void Start()
    {
        ownWeapon = GetComponent<Weapon>();
        //animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (!UseEbony)
        {
            return;
        }
        Timer += Time.deltaTime;

        transform.position = calculateBezierPoint();
        if (Timer > 1.1f)
            Destroy(gameObject);
    }
    public void FireEbonyWings(GameObject objPre, Transform dstTransform, Vector3 p)
    {
        GameObject newobs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);   //skillFiringSystem에서 프리팹 가져오기
        var newObjEbonyWings = newobs.GetComponent<EbonyWings>();
        newObjEbonyWings.StartPoint = GameManager.instance.player.transform;
        newObjEbonyWings.ControlPoint = p;
        newObjEbonyWings.EndPoint = dstTransform;
        newObjEbonyWings.UseEbony = true;
        newobs.transform.position = GameManager.instance.player.transform.position; //시작 위치
    }
    public void CreateCircle(GameObject peachPre, GameObject bounderyPre, Weapon EbonyWings)
    {
        Timer += Time.deltaTime;
        if (Timer > EbonyWings.WeaponTotalStats[((int)Enums.WeaponStat.Cooldown)])
        {
            bounderyPre.GetComponent<PeachBoundery>().CreateCircle(peachPre, bounderyPre, false, EbonyWings);
            Timer = 0;
            peachPre.transform.localScale = mDefaultScale * EbonyWings.WeaponTotalStats[((int)Enums.WeaponStat.Area)];
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
}