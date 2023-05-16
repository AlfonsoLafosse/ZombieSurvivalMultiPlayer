using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OddBallScoring : MonoBehaviour
{
    private PlayerandSoawnManager playerandSoawnManager;
    private int team1Score;
    private int team2Score;
    [SerializeField] private TextMeshProUGUI team1ScoreText;
    [SerializeField] private TextMeshProUGUI team2ScoreText;
    [SerializeField] private GameObject playerWithCrown;
    // Start is called before the first frame update
    void Start()
    {
        playerandSoawnManager = GetComponent<PlayerandSoawnManager>();
    }

    // Update is called once per frame
    void Update()
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
                team1Score += 1;
                team1ScoreText.text = team1Score.ToString();
            }
            if (playerandSoawnManager.team2.Contains(playerWithCrown.gameObject))
            {
                team2Score += 1;
                team2ScoreText.text = team1Score.ToString();
            }
        }
    }
}
