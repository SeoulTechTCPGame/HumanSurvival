using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gralic : MonoBehaviour
{
    Weapon Stat;
    bool isExist = false;
    GameObject newobj;
    float timer = 0;

    // Update is called once per frame
    void Update()
    {
        transform.position = GameManager.instance.player.transform.position + new Vector3(0, 0.5f, 0);
    }

    public void SpawnGralic(GameObject objPre)
    {
        timer += Time.deltaTime;
        if (timer > objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.WeaponStat.Cooldown)])
        {
            if(!isExist)
            {
                newobj = Instantiate(objPre);
                newobj.transform.position = GameManager.instance.player.transform.position + new Vector3(0, 0.5f, 0);
                isExist = true;
            }
            else
            {
                Destroy(newobj);
                isExist = false;
            }
            timer = 0;
        }
        
    }
}
