using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gralic : MonoBehaviour
{
    Weapon Stat;
    bool isExist;
    private GameObject player;
    float timer = 0;

    void Awake()
    {
        isExist = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GameManager.instance.player.transform.position + new Vector3(0, 0.5f, 0);
        Debug.Log("Gralic");
    }

    public void SpawnGralic(GameObject objPre)
    {
        Debug.Log(objPre.activeSelf);
        if(!isExist)
        {
            GameObject newobj = Instantiate(objPre);
            newobj.transform.position = GameManager.instance.player.transform.position + new Vector3(0, 0.5f, 0); // 1�ʵڿ� ������ �ֱ� ����.
            isExist = true;
        }
        
    }
}
