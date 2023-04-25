using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionTile : MonoBehaviour
{
    public float x;   //타일 가로 크기
    public float y;   //타일 세로 크기
    public int probability;
    public GameObject prefab;   //불러올 프리팹
    public GameObject respawn;   //현재 프리팹
    // 태크 Area에서 충돌나서 벗어났을 때만 불러오는 함수
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) { return; }  //Area 영역에서 벗어난 것만 아래 코드가 실행됨

        Vector3 playerPos = GameManager.instance.player.transform.position; //주인공 위치
        Vector3 myPos = transform.position; //현재 Tilemap 위치
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.Movement;  //주인공 이동방향 저장
        float dirX = playerDir.x > 0 ? 1 : -1;
        float dirY = playerDir.y > 0 ? 1 : -1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)  //x축으로 많이 이동시
                {
                    RemoveObject(collision);
                    transform.Translate(Vector3.right * dirX * x * 2); //주인공 이동 방향 앞에 tilemap을 놓기 위해 x*2 만큼 이동
                    ObjectRespown(transform.position);
                }
                else if (diffX < diffY) //y축으로 많이 이동시
                {
                    RemoveObject(collision);
                    transform.Translate(Vector3.up * dirY * y * 2); //주인공 이동 방향 앞에 tilemap을 놓기 위해 y*2 만큼 이동
                    ObjectRespown(transform.position);
                }
                break;
            case "Monster":
                //몬스터 위치 옮기기
                break;
        }
    }
    private void ObjectRespown(Vector3 myPos) {  //프리팹 생성
        if (respawn == null & (Random.Range(0,101) <= probability))   //probability 확률로 생성
        {
            float randomX = Random.Range(-x/2f, x/2f); //랜덤 X좌표
            float randomY = Random.Range(-y/2f, y/2f); //랜덤 Y좌표
            //instantiate함수 (오브젝트 이름, 오브젝트 위치, 오브젝트 회전 값)
            respawn = Instantiate(prefab,new Vector3(myPos.x+randomX,myPos.y+randomY,0f) , Quaternion.identity);
        }
    }
    private void RemoveObject(Collider2D collision) {   //프리팹 삭제
        //if (collision.gameObject.tag == "Object")
        Destroy(respawn);
    }
}