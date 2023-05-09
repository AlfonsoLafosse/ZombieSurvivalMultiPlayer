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

    private string horizontalAxis = "Horizontal";
    private string verticalAxis = "Vertical";

    private Rigidbody2D rb;
    public Vector2 moveDirection = Vector2.zero;

    private bool i;
    public bool inputActive;
    public bool dom;
    public bool check;
    public PlayerandSoawnManager playerandSoawnManager;
    public PowerUpStorage powerUpStorage;

    public string thisPlayerName;

    private void Start()
    {
        playerandSoawnManager = FindObjectOfType<PlayerandSoawnManager>();
        rb = GetComponent<Rigidbody2D>();
        rb.drag = slipperyFactor;
        inputActive = true;
        StartCoroutine(SpawnI());
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
    }

    private void FixedUpdate()
    {

        float horizontalInput = Input.GetAxisRaw(horizontalAxis);
        float verticalInput = Input.GetAxisRaw(verticalAxis);

        if (inputActive)
        {
            if (horizontalInput != 0f)
            {
                StopMovement();
                rb.velocity = moveDirection * moveSpeed;
                moveDirection = new Vector2(Mathf.Sign(horizontalInput), 0f);
            }
            else if (verticalInput != 0f)
            {
                StopMovement();
                rb.velocity = moveDirection * moveSpeed;
                moveDirection = new Vector2(0f, Mathf.Sign(verticalInput));
            }
        }
        else
        {
            StopMovement();
            rb.velocity = moveDirection * moveSpeed;
        }

        //Alfonso Code//
        if (Input.GetKey(KeyCode.Space) && powerUpStorage.PowerUpEquipped == true)
        {
            powerUpStorage.ExecuteCurrentPowerUp();
        }
    }

    
    private void StopMovement()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.drag = stopSpeed;
    }
    public IEnumerator ClashDelay()
    {
        inputActive = false;
        i = true;
        moveDirection = -moveDirection;
        Debug.Log("Stopped" + gameObject);
        yield return new WaitForSeconds(.15f);
        dom = false;
        inputActive = true;
        i = false;
    }
    private IEnumerator SpawnI()
    {
        i = true;
        yield return new WaitForSeconds(0.5f);
        i = false;
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "PowerUp" && powerUpStorage.PowerUpEquipped == false)
        {
            powerUpStorage.GetPowerUp();
            Debug.Log("Collided with a " + other.gameObject.name);
            Destroy(other.gameObject);
            thisPlayerName = this.name;
            Debug.Log(thisPlayerName);
        }
        
        if (other.gameObject.tag == "Player" && inputActive == true && other.transform.GetComponent<CharacterController>().i == false)
        {
            if (other.gameObject.GetComponent<CharacterController>().dom == false)
            {
                dom = true;
            }
            playerandSoawnManager.PlayersCollided();
        }
       /* if (other.gameObject.GetComponent<CharacterController>() != null && inputActive == true)
        {
            if (other.gameObject.GetComponent<CharacterController>().dom == false)
            {
                dom = true;
            }
            if (dom == true)
            {
                check = true;
                if (other.gameObject.GetComponent<CharacterController>().currentDirection == Direction.Down && check == true)
                {
                    if (currentDirection == Direction.Up)
                    {
                        check = false;
                        StartCoroutine(ClashDelay());
                        StartCoroutine(other.gameObject.GetComponent<CharacterController>().ClashDelay());
                        currentDirection = Direction.Down;
                        other.gameObject.GetComponent<CharacterController>().currentDirection = Direction.Up;
                    }
                    else
                    {
                        if (i == false && other.transform.position.y < this.transform.position.y && (other.transform.position.x < this.transform.position.x - 69 || other.transform.position.x > this.transform.position.x + 69))
                        {
                            check = false;
                            Destroy(other.gameObject);
                        }
                        if (i == false && other.transform.position.y > this.transform.position.y)
                        {
                            check = false;
                            Destroy(this.gameObject);
                        }
                    }
                }
                if (other.gameObject.GetComponent<CharacterController>().currentDirection == Direction.Right && check == true)
                {
                    if (currentDirection == Direction.Left)
                    {
                        check = false;
                        StartCoroutine(ClashDelay());
                        StartCoroutine(other.gameObject.GetComponent<CharacterController>().ClashDelay());
                        currentDirection = Direction.Right;
                        other.gameObject.GetComponent<CharacterController>().currentDirection = Direction.Left;
                    }
                    else
                    {
                        if (i == false && other.transform.position.x > this.transform.position.x && (other.transform.position.y < this.transform.position.x - 69 || other.transform.position.x > this.transform.position.y + 69))
                        {
                            check = false;
                            Destroy(other.gameObject);
                        }
                        if (i == false && other.transform.position.x < this.transform.position.x)
                        {
                            check = false;
                            Destroy(this.gameObject);
                        }
                    }
                }
                if (other.gameObject.GetComponent<CharacterController>().currentDirection == Direction.Up && check == true)
                {
                    if (currentDirection == Direction.Down)
                    {
                        check = false;
                        StartCoroutine(ClashDelay());
                        StartCoroutine(other.gameObject.GetComponent<CharacterController>().ClashDelay());
                        currentDirection = Direction.Up;
                        other.gameObject.GetComponent<CharacterController>().currentDirection = Direction.Down;
                    }
                    else
                    {
                        if (i == false && other.transform.position.y > this.transform.position.y && (other.transform.position.x < this.transform.position.x - 69 || other.transform.position.x > this.transform.position.x + 69))
                        {
                            check = false;
                            Destroy(other.gameObject);
                        }
                        if (i == false && other.transform.position.y < this.transform.position.y)
                        {
                            check = false;
                            Destroy(this.gameObject);
                        }
                    }
                }
                if (other.gameObject.GetComponent<CharacterController>().currentDirection == Direction.Left && check == true)
                {
                    if (currentDirection == Direction.Right)
                    {
                        check = false;
                        StartCoroutine(ClashDelay());
                        StartCoroutine(other.gameObject.GetComponent<CharacterController>().ClashDelay());
                        currentDirection = Direction.Left;
                        other.gameObject.GetComponent<CharacterController>().currentDirection = Direction.Right;
                    }
                    else
                    {
                        if (i == false && other.transform.position.x < this.transform.position.x && (other.transform.position.y < this.transform.position.y - 69 || other.transform.position.y > this.transform.position.y + 69))
                        {
                            check = false;
                            Destroy(other.gameObject);
                        }
                        if (i == false && other.transform.position.x > this.transform.position.x)
                        {
                            check = false;
                            Destroy(this.gameObject);
                        }

                    }
                }
            }
        }*/
    }

}
