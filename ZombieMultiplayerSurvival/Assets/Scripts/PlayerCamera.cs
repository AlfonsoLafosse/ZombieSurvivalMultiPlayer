using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float minZoom = 5f;
    public float maxZoom = 10f;
    public float maxDistance = 15f;
    public float zoomSpeed = 1f;
    public float medianDistance = 7.5f;
    public float smoothZoom = 5f;
    public float smoothMovement = 10f;

    private Camera cam;
    private float targetZoom;
    private Vector3 targetPosition;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (player1 == null || player2 == null) return;

        float currentDistance = Vector3.Distance(player1.position, player2.position);

        float newSize;

        if (currentDistance > maxDistance)
        {
            newSize = Mathf.Lerp(maxZoom, minZoom, Mathf.InverseLerp(maxDistance, medianDistance, currentDistance));
        }
        else
        {
            newSize = Mathf.Lerp(minZoom, maxZoom, Mathf.InverseLerp(medianDistance, 0f, currentDistance));
        }

        targetZoom = Mathf.Lerp(targetZoom, newSize, smoothZoom * Time.deltaTime);

        Vector3 midpoint = (player1.position + player2.position) / 2f;
        midpoint.z = transform.position.z;

        targetPosition = Vector3.Lerp(targetPosition, midpoint, smoothMovement * Time.deltaTime);

        transform.position = targetPosition;
        cam.orthographicSize = targetZoom;
    }
}