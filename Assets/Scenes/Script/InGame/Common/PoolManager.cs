using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [Header("EnemyPool")]
    public GameObject[] EnemyPrefabs; // 프리펩들을 보관할 변수.
    private List<GameObject>[] mEnemyPools; // 풀 담당을 하는 리스트들

    [Header("TargetPool")]
    private GameObject[] mTargetPrefab;
    private List<GameObject>[] mTargetPool;

    [Header("ExpPool")]
    public GameObject[] ExpPrefabs;
    private List<GameObject>[] mExpPools;

    [Header("CoinPool")]
    public GameObject[] CoinPrefabs;
    private List<GameObject>[] mCoinPools;

    [Header("HeartPool")]
    public GameObject[] HeartPrefabs;
    private List<GameObject>[] mHeartPools;

    [Header("ItemPool")]
    public GameObject[] ItemPrefabs;
    private List<GameObject>[] mItemPools;

    private void Awake()
    {
        mEnemyPools = new List<GameObject>[EnemyPrefabs.Length];
        mExpPools = new List<GameObject>[ExpPrefabs.Length];
        mCoinPools = new List<GameObject>[CoinPrefabs.Length];
        mHeartPools = new List<GameObject>[HeartPrefabs.Length];
        mItemPools = new List<GameObject>[ItemPrefabs.Length];

        // 인스펙터에서 초기화
        for (int index = 0; index < mEnemyPools.Length; index++)
            mEnemyPools[index] = new List<GameObject>();
        for (int index = 0; index < mExpPools.Length; index++)
            mExpPools[index] = new List<GameObject>();
        for (int index = 0; index < mCoinPools.Length; index++)
            mCoinPools[index] = new List<GameObject>();
        for (int index = 0; index < mHeartPools.Length; index++)
            mHeartPools[index] = new List<GameObject>();
        for (int index = 0; index < mItemPools.Length; index++)
            mItemPools[index] = new List<GameObject>();
    }
    public GameObject Get(string type,int index) //게임 오브젝트 반환 함수
    {
        switch (type)
        {
            case "enemy":
                mTargetPool = mEnemyPools;
                mTargetPrefab = EnemyPrefabs;
                break;
            case "exp":
                mTargetPool = mExpPools;
                mTargetPrefab = ExpPrefabs;
                break;

            case "heart":
                mTargetPool = mHeartPools;
                mTargetPrefab = HeartPrefabs;
                Debug.Log("heart pooling");
                break;

            case "coin":
                mTargetPool = mCoinPools;
                mTargetPrefab = CoinPrefabs;
                Debug.Log("coin pooling");
                break;
            case "item":
                mTargetPool = mItemPools;
                mTargetPrefab = ItemPrefabs;
                break;
        }
        GameObject select = null;
        // 선택한 풀의 비활성화 된 게임 오브젝트 접근.
        // 발견하면 select 변수에 할당
        // 생성된 적이 죽을경우
        
        foreach (GameObject item in mTargetPool[index])
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
            select = Instantiate(mTargetPrefab[index], transform);
            mTargetPool[index].Add(select);
        }
        return select;
    }
    public void Clear(int index)
    {
        foreach (GameObject item in mEnemyPools[index])
            item.SetActive(false);
    }
    public void ClearAll()
    {
        for (int index = 0; index < mEnemyPools.Length; index++)
            foreach (GameObject item in mEnemyPools[index])
                item.SetActive(false);
    }
}