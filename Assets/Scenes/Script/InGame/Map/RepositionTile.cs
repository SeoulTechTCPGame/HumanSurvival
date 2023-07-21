using UnityEngine;

public class RepositionTile : MonoBehaviour
{
    public float X;   //타일 가로 크기
    public float Y;   //타일 세로 크기
    public int Probability;
    public GameObject Prefab;   //불러올 프리팹
    public GameObject Respawn;   //현재 프리팹

    // 태크 Area에서 충돌나서 벗어났을 때만 불러오는 함수
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) { return; }  //Area 영역에서 벗어난 것만 아래 코드가 실행됨

        Vector3 playerPos = GameManager.instance.Player.transform.position; //주인공 위치
        Vector3 myPos = transform.position; //현재 Tilemap 위치
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.Player.Movement;  //주인공 이동방향 저장
        float dirX = playerDir.x > 0 ? 1 : -1;
        float dirY = playerDir.y > 0 ? 1 : -1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)  //x축으로 많이 이동시
                {
                    RemoveObject();
                    transform.Translate(Vector3.right * dirX * X * 2); //주인공 이동 방향 앞에 tilemap을 놓기 위해 x*2 만큼 이동
                    ObjectRespown(transform.position);
                }
                else if (diffX < diffY) //y축으로 많이 이동시
                {
                    RemoveObject();
                    transform.Translate(Vector3.up * dirY * Y * 2); //주인공 이동 방향 앞에 tilemap을 놓기 위해 y*2 만큼 이동
                    ObjectRespown(transform.position);
                }
                break;
            case "Monster":
                //몬스터 위치 옮기기
                break;
        }
    }
    private void ObjectRespown(Vector3 myPos) 
    {  //프리팹 생성
        if (Respawn == null & (Random.Range(0,101) <= Probability * GameManager.instance.CharacterStats[((int)Enums.EStat.Luck)]))   //probability 확률로 생성
        {
            float randomX = Random.Range(-X/2f, X/2f); //랜덤 X좌표
            float randomY = Random.Range(-Y/2f, Y/2f); //랜덤 Y좌표
            Respawn = Instantiate(Prefab,new Vector3(myPos.x+randomX,myPos.y+randomY,0f) , Quaternion.identity);
        }
    }
    private void RemoveObject() 
    {
        Destroy(Respawn);
    }
}