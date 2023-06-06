using System.Collections;
using UnityEngine;

public class DiagonalBouncer : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    public Vector2 direction = new Vector2(1, 1);
    public PlayerCamera playerCamera;
    public bool spawnedByPlayer = false;

    void Start()
    {
        FindObjectOfType<PlayerandSoawnManager>()._CrownObject = this.gameObject;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * speed;
        playerCamera = FindObjectOfType<PlayerCamera>();
        playerCamera.FindTargets();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
            Vector2 normal = other.contacts[0].normal;
            direction = Vector2.Reflect(direction.normalized, normal);
            rb.velocity = direction.normalized * speed;
    }
    public IEnumerator SpawnDelay(GameObject Player)
    {
        Physics2D.IgnoreCollision(Player.GetComponentInChildren<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        yield return new WaitForSeconds(0.2f);
        Physics2D.IgnoreCollision(Player.GetComponentInChildren<Collider2D>(), GetComponent<Collider2D>(), false);
        Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
    }
}
