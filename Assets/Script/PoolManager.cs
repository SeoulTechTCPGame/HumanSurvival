using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // 프리펩들을 보관할 변수.
    List<GameObject>[] enemyPools; // 풀 담당을 하는 리스트들

    GameObject[] targetPrefab;
    List<GameObject>[] targetPool;

    public GameObject[] expPrefabs;
    List<GameObject>[] expPools;
    public GameObject[] coinPrefabs;
    List<GameObject>[] coinPools;
    public GameObject[] heartPrefabs;
    List<GameObject>[] heartPools;

    void Awake()
    {
        enemyPools = new List<GameObject>[enemyPrefabs.Length];
        expPools = new List<GameObject>[expPrefabs.Length];
        coinPools = new List<GameObject>[coinPrefabs.Length];
        heartPools = new List<GameObject>[heartPrefabs.Length];

        // 인스펙터에서 초기화
        for (int index = 0; index < enemyPools.Length; index++)
            enemyPools[index] = new List<GameObject>();
        for (int index = 0; index < expPools.Length; index++)
            expPools[index] = new List<GameObject>();
        for (int index = 0; index < coinPools.Length; index++)
            coinPools[index] = new List<GameObject>();
        for (int index = 0; index < heartPools.Length; index++)
            heartPools[index] = new List<GameObject>();
    }
    public GameObject Get(string type,int index) //게임 오브젝트 반환 함수
    {
        switch (type)
        {
            case "enemy":
                targetPool = enemyPools;
                targetPrefab = enemyPrefabs;
                break;
            case "exp":
                targetPool = expPools;
                targetPrefab = expPrefabs;
                break;

            case "heart":
                targetPool = heartPools;
                targetPrefab = heartPrefabs;
                Debug.Log("heart pooling");
                break;

            case "coin":
                targetPool = coinPools;
                targetPrefab = coinPrefabs;
                Debug.Log("coin pooling");
                break;
        }
        GameObject select = null;
        //선택한 풀의 비활성화 된 게임 오브젝트 접근.
        // 발견하면 select 변수에 할당// 생성된 적이 죽을경우 
        
        foreach (GameObject item in targetPool[index])
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
            select = Instantiate(targetPrefab[index], transform);
            targetPool[index].Add(select);
        }
        return select;
    }
    public void Clear(int index)
    {
        foreach (GameObject item in enemyPools[index])
            item.SetActive(false);
    }

    public void ClearAll()
    {
        for (int index = 0; index < enemyPools.Length; index++)
            foreach (GameObject item in enemyPools[index])
                item.SetActive(false);
    }
}
