using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    public enum ControlType { WASD, ArrowKeys }
    public ControlType controlType = ControlType.WASD;
    public float moveSpeed = 5f;
    public float stopSpeed = 10f;
    public float slipperyFactor = 0.5f;
    public float collisionForce = 5f;
    public float iFrameDuration = 1f;

    public enum Direction { Up, Down, Left, Right }
    public Direction currentDirection = Direction.Up;

    private string horizontalAxis = "Horizontal";
    private string verticalAxis = "Vertical";

    private Rigidbody2D rb;
    private Vector2 moveDirection = Vector2.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = slipperyFactor;
    }

    private void FixedUpdate()
    {
        if (controlType == ControlType.WASD)
        {
            horizontalAxis = "Horizontal1";
            verticalAxis = "Vertical1";
        }
        else
        {
            horizontalAxis = "Horizontal";
            verticalAxis = "Vertical";
        }

        float horizontalInput = Input.GetAxisRaw(horizontalAxis);
        float verticalInput = Input.GetAxisRaw(verticalAxis);

        if (horizontalInput != 0f)
        {
            StopMovement();
            moveDirection = new Vector2(Mathf.Sign(horizontalInput), 0f);
            currentDirection = moveDirection.x > 0 ? Direction.Right : Direction.Left;
        }
        else if (verticalInput != 0f)
        {
            StopMovement();
            moveDirection = new Vector2(0f, Mathf.Sign(verticalInput));
            currentDirection = moveDirection.y > 0 ? Direction.Up : Direction.Down;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterController otherPlayer = collision.gameObject.GetComponent<CharacterController>();
            if (otherPlayer.currentDirection == OppositeDirection())
            {
                StartCoroutine(TakeIFrames());
                StartCoroutine(otherPlayer.TakeIFrames());
                PushAwayFromCollision(collision);
            }
            else
            {
                if (currentDirection != OppositeDirection())
                {
                    Destroy(collision.gameObject);
                }
            }
        }
        else
        {
            StopMovement();
            PushAwayFromCollision(collision);
        }
    }

    private void PushAwayFromCollision(Collision2D collision)
    {
        // Calculate the direction of the collision and apply a force in the opposite direction
        Vector2 collisionDirection = -(collision.contacts[0].point - (Vector2)transform.position).normalized;
        rb.AddForce(collisionDirection * collisionForce, ForceMode2D.Impulse);
    }

    private Direction OppositeDirection()
    {
        // Returns the opposite direction of the current direction
        switch (currentDirection)
        {
            case Direction.Up:
                return Direction.Down;
            case Direction.Down:
                return Direction.Up;
            case Direction.Left:
                return Direction.Right;
            case Direction.Right:
                return Direction.Left;
            default:
                return Direction.Up;
        }
    }

    private IEnumerator TakeIFrames()
    {
        gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        yield return new WaitForSeconds(iFrameDuration);
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
