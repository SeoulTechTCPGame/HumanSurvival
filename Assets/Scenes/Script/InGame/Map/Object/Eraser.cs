using UnityEngine;

public class Eraser : MonoBehaviour, ICollectible
{
    public float Damage = 666;
    public float[] Area = {30, 20};
    [SerializeField] AudioClip mClip;

    public void Collect()
    {
        gameObject.SetActive(false);
        SoundManager.instance.PlaySoundEffect(mClip);

        Collider2D[] enemies = FindAllEnemies(Area[0], Area[1]);
        if (enemies.Length == 0) { return; }
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(Damage, -1);
        }
    }
    private Collider2D[] FindAllEnemies(float width, float height)
    {
        Collider2D[] enemies = Physics2D.OverlapAreaAll(
            GameManager.instance.Player.transform.position + Vector3.left * width + Vector3.up * height,
            GameManager.instance.Player.transform.position + Vector3.right * width + Vector3.down * height,
            LayerMask.GetMask("Monster")
            );

        return enemies;
    }
}