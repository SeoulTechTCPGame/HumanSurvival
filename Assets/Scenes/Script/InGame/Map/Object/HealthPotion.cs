using UnityEngine;

public class HealthPotion : MonoBehaviour,ICollectible
{
    public int HealthToRestore;
    public Rigidbody2D Target;
    private Vector2 mDirection;
    private Rigidbody2D mRb;
    private void Awake()
    {
        mRb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        Target = GameManager.instance.Player.transform.GetChild(4).GetComponent<Rigidbody2D>();
    }

    public void Collect()
    {
        Character character = GameManager.instance.Character;
        character.RestoreHealth(HealthToRestore);
        gameObject.SetActive(false);
    }
}