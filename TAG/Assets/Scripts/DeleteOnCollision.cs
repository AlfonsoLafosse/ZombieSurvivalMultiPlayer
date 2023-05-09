using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnCollision : MonoBehaviour
{
    public GameObject objectToDelete;
    public Vector2 forceToAdd;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the designated object
        if (collision.gameObject.tag == "Player")
        {
            // Print some debug information
            Debug.Log("Collision detected with object: " + collision.gameObject.name);
            Debug.Log("Adding force to children objects with vector: " + forceToAdd.ToString());

            // Apply force to children objects
            foreach (Transform child in transform)
            {
                Rigidbody2D childRigidbody = child.GetComponent<Rigidbody2D>();
                if (childRigidbody != null)
                {
                    childRigidbody.AddForce(forceToAdd, ForceMode2D.Impulse);
                }
            }

            // Destroy the game object
            Destroy(objectToDelete);
        }
    }
}


