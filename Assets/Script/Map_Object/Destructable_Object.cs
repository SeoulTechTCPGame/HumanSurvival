using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable_Object : MonoBehaviour,IDamageable
{
   public void TakeDamage(int damage)
    {
        Destroy(gameObject);
    }
}
