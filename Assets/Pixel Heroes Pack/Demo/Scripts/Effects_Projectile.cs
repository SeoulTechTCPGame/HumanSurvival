using UnityEngine;
using System.Collections;

public class Effects_Projectile : MonoBehaviour
{

    [SerializeField]
    float speed = 5.0f;

    private Rigidbody2D body2d;

    // Use this for initialization
    void Start()
    {
        body2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        body2d.velocity = new Vector2(speed * transform.localScale.x, body2d.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            Destroy(gameObject);
    }
}
