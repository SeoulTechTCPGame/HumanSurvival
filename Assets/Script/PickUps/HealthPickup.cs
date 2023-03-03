using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour,ICollectible
{
    public int healthToRestore;
    public void Collect()
    {
        Character character = FindObjectOfType<Character>();
        character.RestoreHealth(healthToRestore);
        
    }

  
}
