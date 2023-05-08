using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Destructible : MonoBehaviour
{
    public int lifePoints;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (lifePoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            lifePoints--;
        }
        
    }
}
