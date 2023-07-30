using UnityEngine;

public class Eraser : MonoBehaviour, ICollectible
{
    public float Damage = 666;
    public float[] Area = {15, 8};
    [SerializeField] AudioClip mClip;

    public void Collect()
    {
        Vector3[] enemyPositions = FindAllEnemies(Area[0], Area[1]);
        foreach (Vector3 enemyPosition in enemyPositions)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(enemyPosition, 0.1f, LayerMask.GetMask("Monster"));
            foreach (Collider2D collider in colliders)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(Damage, -1);   //TODO: 두번째 인덱스가 무기 인덱스
                }
            }
        }
        gameObject.SetActive(false);
        SoundManager.instance.PlaySoundEffect(mClip);
    }
    private Vector3[] FindAllEnemies(float width, float height)
    {
        Collider2D[] enemies = Physics2D.OverlapAreaAll(
            GameManager.instance.Player.transform.position + Vector3.left * width + Vector3.up * height,
            GameManager.instance.Player.transform.position + Vector3.right * width + Vector3.down * height,
            LayerMask.GetMask("Monster")
            );
        
        Vector3[] positions = new Vector3[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            positions[i] = enemies[i].transform.position;
        }

        return positions;
    }
}