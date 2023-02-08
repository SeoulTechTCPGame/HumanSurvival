using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs; // 프리펩들을 보관할 변수. 
    List<GameObject>[] pools; // 풀 담당을 하는 리스트들 

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        // 인스펙터에서 초기화
        for (int index = 0; index < pools.Length; index++)
            pools[index] = new List<GameObject>();
    }


    public GameObject Get(int index) //게임 오브젝트 반환 함수
    {
        GameObject select = null;
        //선택한 풀의 비활성화 된 게임 오브젝트 접근.
        // 발견하면 select 변수에 할당// 생성된 적이 죽을경우 
        
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // 못찾으면 새롭게 생성후 할당// 모든 적이 죽지 않고 살아있음
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }

    public void Clear(int index)
    {
        foreach (GameObject item in pools[index])
            item.SetActive(false);
    }

    public void ClearAll()
    {
        for (int index = 0; index < pools.Length; index++)
            foreach (GameObject item in pools[index])
                item.SetActive(false);
    }
}
