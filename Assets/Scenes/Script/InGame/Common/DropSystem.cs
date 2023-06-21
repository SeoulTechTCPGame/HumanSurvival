using System.Collections.Generic;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string Name;
        public int PrefabsIndex;
        public float DropRate;
    }
    public List<Drops> DropList;
    private GameObject mDropsObj;
    
     public void OnDrop(Vector2 pos)
    {
        //로직 1. 적이 죽으면 랜덤 넘버 생성(아이템 확률)
        float randomNumber = UnityEngine.Random.Range(0f, 100f);
        mDropsObj = GameObject.Find("--DropObj--");
        //로직 3.
        List<Drops> posibleDrops = new List<Drops>();
        foreach(Drops rate in DropList)
        {   
            //로직 2
            if (randomNumber <= rate.DropRate) posibleDrops.Add(rate);
        }       
        //drop possible 인지 확인
        if (posibleDrops.Count > 0)
        {   
            //로직 4.
            Drops drops = posibleDrops[UnityEngine.Random.Range(0, posibleDrops.Count)];
            mDropsObj.GetComponent<DropSpawner>().Spawn(transform.position, drops.Name, drops.PrefabsIndex);
        }
    }
}