using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveCamera : MonoBehaviour
{
    public List<Transform> targets;     // List of players to track
    public Vector2 zoomParamsLevel1;    // Zoom parameters for Level 1
    public Vector2 zoomParamsLevel2;    // Zoom parameters for Level 2

    private Transform cameraTransform;   // Transform of the camera
    private Vector3 desiredPosition;     // Desired position of the camera

    private bool readyToCamera = false;

    private void Start()
    {
        cameraTransform = transform;
        targets = new List<Transform>();

        // Find and add all Player tagged objects to the targets list
        
    }

    private void LateUpdate()
    {

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            targets.Add(player.transform);
        }

        // Calculate the desired position based on the average position of targets
        CalculateDesiredPosition();

        // Move the camera towards the desired position
        SmoothMove();
    }

    private void CalculateDesiredPosition()
    {
        Vector3 averagePosition = Vector3.zero;

        // Calculate the average position of all targets
        foreach (Transform target in targets)
        {
            averagePosition += target.position;
        }

        averagePosition /= targets.Count;

        // Set the desired position as the average position of targets
        desiredPosition = averagePosition;
    }

    private void SmoothMove()
    {
        // Move the camera towards the desired position using Lerp
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, desiredPosition, Time.deltaTime * 5f);
    }

    private void Update()
    {
        // Check the distances between players and activate different zoom levels if criteria are met
        if (targets.Count >= 2)
        {
            float xDistance = Mathf.Abs(targets[0].position.x - targets[1].position.x);
            float yDistance = Mathf.Abs(targets[0].position.y - targets[1].position.y);

            if (xDistance >= zoomParamsLevel2.x || yDistance >= zoomParamsLevel2.y)
            {
                // Activate Level 2 zoom level
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 10f, Time.deltaTime * 5f);
            }
            else if (xDistance >= zoomParamsLevel1.x || yDistance >= zoomParamsLevel1.y)
            {
                // Activate Level 1 zoom level
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 5f, Time.deltaTime * 5f);
            }
            else
            {
                // Activate default zoom level
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 3f, Time.deltaTime * 5f);
            }
        }
    }
}
