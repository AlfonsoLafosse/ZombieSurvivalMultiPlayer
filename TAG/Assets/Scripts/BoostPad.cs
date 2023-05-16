using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPad : MonoBehaviour
{
    public float boostForce = 10f;
    public bool isboosted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController playerController = collision.GetComponent<CharacterController>();
        if (playerController != null)
        {
            playerController.isBoosted = true;
        }
    }
}




