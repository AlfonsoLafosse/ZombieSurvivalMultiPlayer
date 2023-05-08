using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject[] Exits;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Transform collidingTransform = collision.gameObject.transform;
        if(collidingTransform != null)
        {
            collidingTransform.position = GetRandomExit().transform.position;
        }


    }

    public GameObject GetRandomExit()
    {
        int randomIndex = Random.Range(0, Exits.Length);
        return Exits[randomIndex];
    }

}
