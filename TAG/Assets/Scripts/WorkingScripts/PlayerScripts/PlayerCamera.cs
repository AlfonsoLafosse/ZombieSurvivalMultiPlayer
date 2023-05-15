using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public List<Transform> targets = new List<Transform>(); // the players to follow
    public float smoothTime = 0.5f; // smoothing time for camera movement
    public float[] zoomLevels; // an array of zoom levels based on the distance between players
    public float maxZoom = 15f; // maximum zoom level
    public float zoomSpeed = 5f; // how fast the camera should zoom in/out
    public float minDistance = 5f; // minimum distance between players before camera starts zooming out

    private Vector3 velocity;
    private Camera cam;
    private PlayerandSoawnManager playerandSoawnManager;

    private void Awake()
    {
        if (playerandSoawnManager == null) playerandSoawnManager = FindObjectOfType<PlayerandSoawnManager>();
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = zoomLevels[0];
        FindTargets();
        
    }

    void LateUpdate()
    {
        if (targets.Count == 0)
        {
            return;
        }

        Move();
        Zoom();
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + Vector3.back * 10f; // 10 units behind the center point
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    void Zoom()
    {
        float greatestDistance = GetGreatestDistance();
        int zoomLevel = 0;
        for (int i = 0; i < zoomLevels.Length; i++)
        {
            if (greatestDistance > minDistance * (i + 1))
            {
                zoomLevel = i + 1;
            }
        }
        float newZoom = Mathf.Lerp(cam.orthographicSize, zoomLevels[zoomLevel], Time.deltaTime * zoomSpeed);
        cam.orthographicSize = Mathf.Clamp(newZoom, zoomLevels[0], maxZoom);
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }

    float GetGreatestDistance()
    {
        float greatestDistance = 0f;

        for (int i = 0; i < targets.Count; i++)
        {
            for (int j = i + 1; j < targets.Count; j++)
            {
                float distance = Vector3.Distance(targets[i].position, targets[j].position);
                if (distance > greatestDistance)
                {
                    greatestDistance = distance;
                }
            }
        }

        return greatestDistance;
    }
    public void FindTargets()
    {
        targets.Clear();
        //GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        //GameObject crownObject = GameObject.FindGameObjectWithTag("Crown");

        print(playerandSoawnManager != null);

        foreach (GameObject obj in playerandSoawnManager._PlayerObject)
        {
            targets.Add(obj.transform);
        }
        if (playerandSoawnManager._CrownObject != null)
        {
            targets.Add(playerandSoawnManager._CrownObject.transform);
        }
        /*        int index = 0;
                for (int i = 0; i < playerObjects.Length; i++)
                {
                    targets[index] = playerObjects[i].transform;
                    index++;
                }

                for (int i = 0; i < crownObject.Length; i++)
                {
                    targets[index] = crownObject[i].transform;
                    index++;
                }*/

    }
}
   /* public void FindTargets()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        targets = new Transform[playerObjects.Length];
        for (int i = 0; i < playerObjects.Length; i++)
        {
            targets[i] = playerObjects[i].transform;
        }
    }
   */
