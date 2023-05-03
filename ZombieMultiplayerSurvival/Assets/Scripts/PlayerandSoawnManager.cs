using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerandSoawnManager : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject player1prefab;
    [SerializeField] private GameObject player2prefab;
    private Vector3 player1Position;
    private Vector3 player2Position;
    private int player1Score;
    private int player2Score;
    public TextMeshProUGUI score1;
    public TextMeshProUGUI score2;
    // Start is called before the first frame update
    void Start()
    {
        player1Position = player1.transform.position;
        player2Position = player2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player1 == null)
        {
            player1 = Instantiate(player1prefab, player1Position, Quaternion.identity);
            player2Score += 1;
        }
        if (player2 == null)
        {
            player2 =Instantiate(player2prefab, player2Position, Quaternion.identity);
            player1Score += 1;
        }
        score1.text = player1Score.ToString();
        score2.text = player2Score.ToString();
    }
}
