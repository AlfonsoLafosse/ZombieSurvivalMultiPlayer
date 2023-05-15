using UnityEngine;

public class DiagonalBouncer : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 direction = new Vector2(1, 1);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * speed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 normal = other.contacts[0].normal;
        direction = Vector2.Reflect(direction.normalized, normal);
        rb.velocity = direction.normalized * speed;
    }
}
