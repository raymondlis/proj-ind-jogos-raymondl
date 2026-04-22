using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        
        // Start in a random diagonal direction
        float startX = Random.value > 0.5f ? 1 : -1;
        float startY = Random.value > 0.5f ? 1 : -1;
        rb.linearVelocity = new Vector2(startX, startY).normalized * speed;
    }

    void Update()
    {
        if (GameController.isGameOver)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // Ensure constant velocity
        if (rb.linearVelocity.magnitude != speed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reflect velocity based on normal of contact
        Vector2 normal = collision.contacts[0].normal;
        rb.linearVelocity = Vector2.Reflect(rb.linearVelocity, normal);
    }
}
