using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class BadBallSpawner : MonoBehaviour
{
    public int pointsInterval = 10; // Points interval between item spawns
    public int initialPointsInterval = 10;
    public GameObject itemPrefab; // The item prefab to spawn

    private OddBallScoring scoringScript; // Reference to the scoring script
    private bool isItemSpawned = false; // Flag to track if an item is already spawned
    private int totalPoints = 0; // Total accumulated points

    private void Start()
    {
        scoringScript = FindObjectOfType<OddBallScoring>(); // Find the OddBallScoring script in the scene
    }

    private void Update()
    {
        if (scoringScript != null)
        {
            float points = CalculatePoints();

            // Check if points interval is reached and an item is not already spawned
            if (points >= pointsInterval && !isItemSpawned)
            {
                int spawnCount = Mathf.FloorToInt(points / (float)pointsInterval); // Calculate the number of items to spawn
                for (int i = 0; i < spawnCount; i++)
                {
                    if (!IsItemPresentInScene())
                    {
                        SpawnItem();
                        pointsInterval += initialPointsInterval;
                        isItemSpawned = true;
                    }
                }
            }
        }
    }

    private float CalculatePoints()
    {
        // Calculate the total points based on your scoring mechanism
        float team1Points = scoringScript.team1Score;
        float team2Points = scoringScript.team2Score;

        // Accumulate the points from both teams
        totalPoints = (int)(team1Points + team2Points);

        return totalPoints;
    }

    private bool IsItemPresentInScene()
    {
        // Check if any instances of the item prefab are still present in the scene
        GameObject[] items = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in items)
        {
            if (obj == itemPrefab)
            {
                return true;
            }
        }
        return false;
    }

    private void SpawnItem()
    {
        // Instantiate the item prefab at the spawner's position and rotation
        Instantiate(itemPrefab, transform.position, transform.rotation);
    }
}*/


 