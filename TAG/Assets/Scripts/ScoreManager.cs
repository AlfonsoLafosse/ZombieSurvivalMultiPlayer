using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    private bool hasSprite = false;

    // Called when the player collides with the sprite object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Star"))
        {
            // Store the sprite on the player
            collision.gameObject.SetActive(false); // Hide or disable the sprite object
            hasSprite = true;
        }
    }

    // Called when the player enters the trigger area
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Port") && hasSprite)
        {
            // Count as a score and remove the sprite from the player
            score++;
            hasSprite = false;
        }
    }
}