using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Destructible : MonoBehaviour
{
    public int lifePoints = 3;

    [SerializeField] private List<Sprite> glassSprites;
    public int listIndexValue = 0;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (listIndexValue >= lifePoints)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log(listIndexValue);

        if (collision.gameObject.tag == "Player")
        {
            listIndexValue++;
            SpriteRenderer spriteRender = GetComponentInChildren<SpriteRenderer>();
            spriteRender.sprite = glassSprites[listIndexValue];
        }      
    }
}
