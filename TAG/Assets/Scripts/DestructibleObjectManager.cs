using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DestructibleObjectManager : MonoBehaviour
{
    public List<GameObject> objects;
    public List<Vector3> spawnPositions;
    // Start is called before the first frame update
    void Start()
    {
        NewScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RoundStart()
    {
        foreach (GameObject gameObject in objects)
        {
            if (gameObject.GetComponent<Destructible>() != null)
            {
                gameObject.GetComponent<Destructible>().Reset();
            }
            gameObject.transform.position = spawnPositions[objects.IndexOf(gameObject)];
            gameObject.SetActive(true);
        }
    }
    public void NewScene()
    {
        foreach (GameObject taggedObj in GameObject.FindGameObjectsWithTag("Destructible"))
        {
            objects.Add(taggedObj);
        }
        foreach (GameObject obj in objects)
        {
            spawnPositions.Add(obj.transform.position);
        }
    }

}
