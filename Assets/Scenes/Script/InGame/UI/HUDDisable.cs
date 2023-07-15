using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDDisable : MonoBehaviour
{
    [SerializeField] GameObject HUD;
    void Update()
    {
        if (Time.timeScale == 0)
        {
           HUD.SetActive(false);
        }else  HUD.SetActive(true);
    }
}
