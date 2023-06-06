using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OddBallScoring : MonoBehaviour
{
    public PlayerandSoawnManager playerandSoawnManager;
    public float score;
    public float finalScore;
    public GameObject playerWithCrown;
    public Slider slider;
    public GameObject sliderObject;
    public GameObject team1Win;
    public GameObject team2Win;
    public MusicManager musicManager;
    // Start is called before the first frame update
    void Start()
    {
        playerandSoawnManager = GetComponent<PlayerandSoawnManager>();
        musicManager = FindObjectOfType<MusicManager>();
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
        slider.value = score;
        if (playerWithCrown != null)
        {
            if (playerandSoawnManager.team1.Contains(playerWithCrown.gameObject))
            {
                score += .10f;
            }
            if (playerandSoawnManager.team2.Contains(playerWithCrown.gameObject))
            {
                score -= .10f;
            }
        }
        if (score > finalScore)
        {
            Time.timeScale = 0;
            musicManager.audioSource.volume = .25f;
            musicManager.youWin.Play();
            sliderObject.SetActive(false);
            team1Win.SetActive(true);
        }
        if (score < 0)
        {
            Time.timeScale = 0;
            musicManager.audioSource.volume = .25f;
            musicManager.youWin.Play();
            sliderObject.SetActive(false);
            team2Win.SetActive(true);
        }
    }
}
