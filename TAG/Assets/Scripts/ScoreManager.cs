using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI player1ScoreText; // Reference to the TextMeshProUGUI component for displaying player 1 score
    public TextMeshProUGUI player2ScoreText; // Reference to the TextMeshProUGUI component for displaying player 2 score
    private int player1Score = 0;
    private int player2Score = 0;
    public GameObject player1;
    public GameObject player2;
    private PlayerandSoawnManager playerandSoawnManager;
    public Transform crownSpawner;
    public GameObject crownObject;
    private void Start()
    {
        playerandSoawnManager = FindObjectOfType<PlayerandSoawnManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerandSoawnManager.team1.Contains(collision.gameObject) && collision.GetComponent<CharacterController>().hasCrown && gameObject.CompareTag("GoalPlayer1"))
        {
            collision.GetComponent<CharacterController>().hasCrown = false;
            player1Score++;
            player1ScoreText.text = player1Score.ToString();
            InstantiateCrown();
        }
        if (playerandSoawnManager.team2.Contains(collision.gameObject) && collision.GetComponent<CharacterController>().hasCrown && gameObject.CompareTag("GoalPlayer2"))
        {
            collision.GetComponent<CharacterController>().hasCrown = false;
            player2Score++;
            player2ScoreText.text = player2Score.ToString();
            InstantiateCrown();
        }
    }
    private void InstantiateCrown()
    {
        Instantiate(crownObject, crownSpawner.position, Quaternion.identity);
    }
}