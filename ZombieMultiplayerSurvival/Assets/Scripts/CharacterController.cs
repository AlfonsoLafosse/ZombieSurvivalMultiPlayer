using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public enum ControlType { WASD, ArrowKeys }
    public ControlType controlType = ControlType.WASD;
    public float moveSpeed = 5f;
    public float stopSpeed = 10f;
    public float slipperyFactor = 0.5f;
    public float collisionForce = 5f;

    [Header("Input Axes")]
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";

    private Rigidbody2D rb;
    private Vector2 moveDirection = Vector2.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = slipperyFactor;
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw(horizontalAxis);
        float verticalInput = Input.GetAxisRaw(verticalAxis);

        if (horizontalInput != 0f)
        {
            StopMovement();
            moveDirection = new Vector2(Mathf.Sign(horizontalInput), 0f);
            
        }
        else if (verticalInput != 0f)
        {
            StopMovement();
            moveDirection = new Vector2(0f, Mathf.Sign(verticalInput));
            
        }

        rb.velocity = moveDirection * moveSpeed;
    }

    private void StopMovement()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.drag = stopSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Stop the player's movement
        StopMovement();

        // Calculate the direction of the collision and apply a force in the opposite direction
        Vector2 collisionDirection = -(collision.contacts[0].point - (Vector2)transform.position).normalized;
        rb.AddForce(collisionDirection * collisionForce, ForceMode2D.Impulse);
    }
}
