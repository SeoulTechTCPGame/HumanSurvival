using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionTile : MonoBehaviour
{
    public int x;   //타일 가로 크기
    public int y;   //타일 세로 크기
    // 태크 Area에서 충돌나서 벗어났을 때만 불러오는 함수
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) { return; }  //필터 역할

        Vector3 playerPos = GameManager.instance.player.transform.position; //주인공 위치
        Vector3 myPos = transform.position; //현재 Tilemap 위치
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.movement;  //주인공 이동방향 저장
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)  //x축으로 많이 이동시
                {
                    transform.Translate(Vector3.right * dirX * x * 2); //주인공 이동 방향 앞에 tilemap을 놓기 위해 x*2 만큼 이동
                }
                else if (diffX < diffY) //y축으로 많이 이동시
                {
                    transform.Translate(Vector3.up * dirY * y * 2); //주인공 이동 방향 앞에 tilemap을 놓기 위해 y*2 만큼 이동
                }
                break;
        }
    }
}

