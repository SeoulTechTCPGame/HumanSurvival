using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public float health;
   public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Kill();
    }
    public void Kill()
    {
        Destroy(gameObject);
    }
}
