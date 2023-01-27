using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public Slider hpBar;
    private float damage = 0.001f;
    // Start is called before the first frame update
    void Start()
    {
        
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
