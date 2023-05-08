using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleStick : MonoBehaviour
{
    public float rotationSpeed = 10f; // speed at which the stick rotates
    public float knockbackForce = 10f; // force with which the stick knocks back the player
    public string playerTag = "Player"; // tag of the player GameObjects

    private bool isRotating = false; // whether the stick is currently rotating

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding GameObject has the player tag
        if (collision.gameObject.CompareTag(playerTag))
        {
            // Knock back the player with a force
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

            // Start rotating the stick
            isRotating = true;
        }
    }

    void Update()
    {
        if (isRotating)
        {
            // Rotate the stick in the Z axis
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
        }
    }
}
