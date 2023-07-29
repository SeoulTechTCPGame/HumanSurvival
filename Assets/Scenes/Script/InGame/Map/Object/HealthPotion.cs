using UnityEngine;

public class HealthPotion : MonoBehaviour,ICollectible
{
    public int HealthToRestore;
    [SerializeField] AudioClip mClip;

    private void Start()
    {
        GameManager.instance.FoundChickenCount++;
    }
    public void Collect()
    {
        Character character = GameManager.instance.Character;
        character.RestoreHealth(HealthToRestore);
        gameObject.SetActive(false);
        SoundManager.instance.PlaySoundEffect(mClip);
    }
}