using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public int prefabsIndex;
        public float dropRate;
    }
    public List<Drops> drops;
     public void OnDrop()
    {
        //로직 1. 적이 죽으면 랜덤 넘버 생성(아이템 확률)
        float randomNumber = UnityEngine.Random.Range(0f, 100f);

        //로직 3.
        List<Drops> posibleDrops = new List<Drops>();
        foreach(Drops rate in drops)
        {   
            //로직 2
            if (randomNumber <= rate.dropRate) posibleDrops.Add(rate);
        }       
        //drop possible 인지 확인
        if (posibleDrops.Count > 0)
        {   
            //로직 4.
            Drops drops = posibleDrops[UnityEngine.Random.Range(0, posibleDrops.Count)];
            //Instantiate(drops.itemPrefabs, transform.position, Quaternion.identity);
            GameObject dropObj=GameManager.instance.pool.Get(drops.name,drops.prefabsIndex);
            dropObj.transform.position = transform.position;
            dropObj.transform.parent = transform;
        }
        
    }
}
