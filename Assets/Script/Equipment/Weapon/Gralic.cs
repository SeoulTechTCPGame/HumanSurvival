using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gralic : MonoBehaviour
{
    Weapon Stat;
    private GameObject player;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }

    public void SpawnGralic(GameObject objPre)
    {
        
    }
}
