using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OddBallScoring : MonoBehaviour
{
    public PlayerandSoawnManager playerandSoawnManager;
    public float team1Score;
    public float team2Score;
    [SerializeField] public TextMeshProUGUI team1ScoreText;
    [SerializeField] public TextMeshProUGUI team2ScoreText;
    public GameObject playerWithCrown;
    // Start is called before the first frame update
    void Start()
    {
        playerandSoawnManager = GetComponent<PlayerandSoawnManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(GameObject player in playerandSoawnManager._PlayerObject)
        {
            if (player.GetComponent<CharacterController>().hasCrown)
            {
                playerWithCrown = player;
            }
            
        }
        if (playerWithCrown != null)
        {
            if (playerandSoawnManager.team1.Contains(playerWithCrown.gameObject))
            {
                team1Score += .10f;
                team1ScoreText.text = Mathf.Round(team1Score).ToString();
            }
            if (playerandSoawnManager.team2.Contains(playerWithCrown.gameObject))
            {
                team2Score += .10f;
                team2ScoreText.text = Mathf.Round(team2Score).ToString();
            }
        }
    }
}
