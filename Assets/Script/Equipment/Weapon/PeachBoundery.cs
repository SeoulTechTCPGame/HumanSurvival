using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PeachBoundery : MonoBehaviour
{
    private float distance = 8f;  // 오브젝트와 타겟 사이의 거리
    private float speed = 40f;  // 공전 속도
    private float timer = 0;
    private bool isClockwise = true;
    private GameObject mPeachObj;

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        transform.position = getStartPosition(GameManager.instance.player.transform.position);
        if(isClockwise)
            transform.RotateAround(GameManager.instance.player.transform.position, Vector3.back, speed * timer);
        else
            transform.RotateAround(GameManager.instance.player.transform.position, -Vector3.back, speed * timer);
    }

    public void CreateCircle(GameObject peachPre, GameObject bounderyPre, bool isCW)
    {
        GameObject newobs = Instantiate(bounderyPre, GameObject.Find("SkillFiringSystem").transform);
        newobs.transform.position = getStartPosition(GameManager.instance.player.transform.position);

        if (!isCW)
            isClockwise = false;
    }

    private Vector3 getStartPosition(Vector3 pos)
    {
        return pos + Vector3.up * distance;
    }
}
