using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour,ICollectible
{
    public int healthToRestore;
    public void Collect()
    {
        Character character = GameManager.instance.character;
        character.RestoreHealth(healthToRestore);
        gameObject.SetActive(false);
    }
}
