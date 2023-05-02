using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEditor.Rendering;
using Unity.VisualScripting;

public class CharacterController : MonoBehaviour
{
    public enum ControlType { WASD, ArrowKeys }
    public ControlType controlType = ControlType.WASD;
    public float moveSpeed = 5f;
    public float stopSpeed = 10f;
    public float slipperyFactor = 0.5f;
    private float storeMoveSpeed;

    public enum Direction { Up, Down, Left, Right }
    public Direction currentDirection = Direction.Down;

    public Collider2D upObject;
    public Collider2D downObject;
    public Collider2D leftObject;
    public Collider2D rightObject;
    public Collider2D activeCollider;

    private string horizontalAxis = "Horizontal";
    private string verticalAxis = "Vertical";

    private Rigidbody2D rb;
    private Vector2 moveDirection = Vector2.zero;

    private bool i;
    private bool inputActive;

    private void Start()
    {
        storeMoveSpeed = 1000;
        rb = GetComponent<Rigidbody2D>();
        rb.drag = slipperyFactor;
        inputActive = true;
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

        if (inputActive)
        {
            if (horizontalInput != 0f)
            {
                StopCoroutine(movement());
                StartCoroutine(movement());
                StopMovement();
                moveDirection = new Vector2(Mathf.Sign(horizontalInput), 0f);
                if (moveDirection.x > 0f)
                {
                    currentDirection = Direction.Right;
                }
                else
                {
                    currentDirection = Direction.Left;
                }
            }
            else if (verticalInput != 0f)
            {
                StopCoroutine(movement());
                StartCoroutine(movement());
                StopMovement();
                moveDirection = new Vector2(0f, Mathf.Sign(verticalInput));
                if (moveDirection.y > 0f)
                {
                    currentDirection = Direction.Up;
                }
                else
                {
                    currentDirection = Direction.Down;
                }
            }
        }
        rb.velocity = moveDirection * moveSpeed;


        // Activate the corresponding child object based on the current direction
        switch (currentDirection)
        {
            case Direction.Up:
                activeCollider = upObject;
                break;
            case Direction.Down:
                activeCollider = downObject;
                break;
            case Direction.Left:
                activeCollider = leftObject;
                break;
            case Direction.Right:
                activeCollider = rightObject;
                break;
        }
    }
    private IEnumerator movement()
        {
        moveSpeed = storeMoveSpeed;
        yield return  new WaitForSeconds(0.1f);
        moveSpeed -= 500;
        if (moveSpeed < 0f)
        {
            moveSpeed = 0f;
        }
        }
    private void StopMovement()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.drag = stopSpeed;
    }
    private IEnumerator ClashDelay()
    {
        i = true;
        inputActive = false;
        yield return new WaitForSeconds(0.1f);
        inputActive = true;
        i = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided");
        if (other.gameObject.GetComponentInParent<CharacterController>() != null && other.gameObject.GetComponentInParent<CharacterController>().activeCollider == other)
        {
            if (other.gameObject.GetComponentInParent<CharacterController>().currentDirection == Direction.Up)
            {
                if (currentDirection == Direction.Down)
                {
                    Debug.Log("Clash");
                    moveDirection = -moveDirection;
                    StopCoroutine(movement());
                    StartCoroutine(movement());
                    StartCoroutine(ClashDelay());
                }
                else
                {
                    if (i == false)
                    Destroy(this.gameObject);
                }
            }
            if (other.gameObject.GetComponentInParent<CharacterController>().currentDirection == Direction.Down)
            {
                if (currentDirection == Direction.Up)
                {
                    Debug.Log("Clash");
                    moveDirection = -moveDirection;
                    StopCoroutine(movement());
                    StartCoroutine(movement());
                    StartCoroutine(ClashDelay());
                }
                else
                {
                    if (i == false)
                        Destroy(this.gameObject);
                }
            }
            if (other.gameObject.GetComponentInParent<CharacterController>().currentDirection == Direction.Left)
            {
                if (currentDirection == Direction.Right)
                {
                    Debug.Log("Clash");
                    moveDirection = -moveDirection;
                    StopCoroutine(movement());
                    StartCoroutine(movement());
                    StartCoroutine(ClashDelay());
                }
                else
                {
                    if (i == false)
                        Destroy(this.gameObject);
                }
            }
            if (other.gameObject.GetComponentInParent<CharacterController>().currentDirection == Direction.Right)
            {
                if (currentDirection == Direction.Left)
                {
                    Debug.Log("Clash");
                    moveDirection = -moveDirection;
                    StopCoroutine(movement());
                    StartCoroutine(movement());
                    StartCoroutine(ClashDelay());
                }
                else
                {
                    if (i == false)
                        Destroy(this.gameObject);
                }
            }
        }
    }

}
