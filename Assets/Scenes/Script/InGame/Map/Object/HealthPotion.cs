using UnityEngine;

public class HealthPotion : MonoBehaviour,ICollectible
{
    public int HealthToRestore;
    public void Collect()
    {
        Character character = GameManager.instance.Character;
        character.RestoreHealth(HealthToRestore);
        gameObject.SetActive(false);
    }
}
