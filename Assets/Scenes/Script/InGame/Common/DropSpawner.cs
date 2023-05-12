using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    public void Spawn(Vector2 pos,string name, int index)
    {
        GameObject newDrops = GameManager.instance.pool.Get(name, index);
        newDrops.transform.position = pos;
        newDrops.transform.parent = transform;
    }
}
