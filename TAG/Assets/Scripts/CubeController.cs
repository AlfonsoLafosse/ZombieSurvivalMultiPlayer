using UnityEngine;
using UnityEngine.InputSystem;

public class CubeController : MonoBehaviour
{
    // Enum to switch between WASD and Arrow Keys controls
    public enum ControlType { WASD, ArrowKeys };
    public ControlType controlType = ControlType.WASD;

    // Input actions for movement
    private InputAction movementInput;
    private Vector2 moveDirection;

    // Cube movement variables
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    private void Awake()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Set up the movement input action
        movementInput = new InputAction("Move", InputActionType.PassThrough, "Vector2");
        movementInput.performed += OnMovePerformed;
        movementInput.canceled += OnMoveCanceled;
    }

    private void OnEnable()
    {
        // Enable the movement input action
        movementInput.Enable();
    }

    private void OnDisable()
    {
        // Disable the movement input action
        movementInput.Disable();
    }

    private void FixedUpdate()
    {
        // Move the cube based on the input direction and speed
        rb.velocity = moveDirection.normalized * moveSpeed;

        // Rotate the cube to face the movement direction
        if (moveDirection.magnitude > 0)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        // Get the movement direction based on the input action and control type
        if (controlType == ControlType.WASD)
        {
            moveDirection = context.ReadValue<Vector2>();
        }
        else if (controlType == ControlType.ArrowKeys)
        {
            moveDirection = context.ReadValue<Vector2>() * new Vector2(1, -1);
        }
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // Stop moving when the movement input is released
        moveDirection = Vector2.zero;
    }
}
