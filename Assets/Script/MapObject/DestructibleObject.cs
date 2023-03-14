using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour,IDamageable
{
    public void TakeDamage(float damage, int weaponIndex)
    {
        //position 위치에 Drop 생성
        gameObject.GetComponent<DropSystem>().OnDrop(gameObject.transform.position);
        Destroy(gameObject);
    }
}
