using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enums;
using System;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] Slider HpBar;
    private float damage = 0.1f;    //몬스터 데미지

    void Start()
    {
        //TODO: 캐릭터 고유의 최대 체력 가져오기
        HpBar.maxValue = (int)Enums.Stat.MaxHealth;
        HpBar.value = (int)Enums.Stat.MaxHealth;
    }
    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Monster"){
            damage = other.gameObject.GetComponent<Enemy>().enemyData.power;
            HpBar.value -= damage;
            Debug.Log(damage);
            
        }
    }
}
