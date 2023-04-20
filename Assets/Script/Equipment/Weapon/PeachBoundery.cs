using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeachBoundery : MonoBehaviour
{
    [SerializeField] private Transform target;  // 움직이는 캐릭터
    [SerializeField] private float distance = 10f;  // 오브젝트와 타겟 사이의 거리
    [SerializeField] private float speed = 20f;  // 공전 속도

    private GameObject mPeachObj;

    private Vector3 targetPos;

    private void FixedUpdate()
    {
        // 타겟 위치 계산
        targetPos = target.position + Vector3.up * distance;

        // 오브젝트 공전
        transform.RotateAround(targetPos, Vector3.up, speed * Time.deltaTime);
    }

    public void CreateCircle(GameObject peachPre, GameObject bounderyPre, bool isClockwise)
    {
        GameObject newobs = Instantiate(bounderyPre, GameObject.Find("SkillFiringSystem").transform);
        newobs.transform.position = getStartPosition(GameManager.instance.player.transform.position);
    }

    private Vector3 getStartPosition(Vector3 pos)
    {
        pos.y += 1;
        return pos;
    }
}
