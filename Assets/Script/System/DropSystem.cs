using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefabs;
        public float dropRate;
    }
    public List<Drops> drops;
     void OnDestroy()
    {
        //���� 1. ���� ������ ���� �ѹ� ����(������ Ȯ��)
        float randomNumber = UnityEngine.Random.Range(0f, 100f);

        //���� 3.
        List<Drops> posibleDrops = new List<Drops>();
        foreach(Drops rate in drops)
        {   
            //���� 2
            if (randomNumber <= rate.dropRate) posibleDrops.Add(rate);
        }       
        //drop possible ���� Ȯ��
        if (posibleDrops.Count > 0)
        {   
            //���� 4.
            Drops drops = posibleDrops[UnityEngine.Random.Range(0, posibleDrops.Count)];
            Instantiate(drops.itemPrefabs, transform.position, Quaternion.identity);
        }
        
    }
}