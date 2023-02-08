using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enums;
using System;

public class PlayerHp : MonoBehaviour
{
    public Slider hpBar;
    private float damage = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        hpBar.maxValue = (int)Enums.Stat.MaxHealth;
        hpBar.value = (int)Enums.Stat.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Monster"){
            hpBar.value -= damage;
        }
    }
}
