using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerandSoawnManager : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject player1prefab;
    [SerializeField] private GameObject player2prefab;
    private CharacterController player1Controller;
    private CharacterController player2Controller;
    private Vector3 player1Position;
    private Vector3 player2Position;
    private int player1Score;
    private int player2Score;
    public TextMeshProUGUI score1;
    public TextMeshProUGUI score2;
    public PlayerCamera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        player1Position = player1.transform.position;
        player2Position = player2.transform.position;
        player1Controller = player1.GetComponent<CharacterController>();
        player2Controller = player2.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player1 == null)
        {
            player1 = Instantiate(player1prefab, player1Position, Quaternion.identity);
            playerCamera.FindTargets();
            player1Controller = player1.GetComponent<CharacterController>();
            player2Score += 1;
        }
        if (player2 == null)
        {
            player2 =Instantiate(player2prefab, player2Position, Quaternion.identity);
            playerCamera.FindTargets();
            player2Controller = player2.GetComponent<CharacterController>();
            player1Score += 1;
        }
        score1.text = player1Score.ToString();
        score2.text = player2Score.ToString();
    }
    public void PlayersCollided()
    {
        if (player1Controller.moveDirection == -player2Controller.moveDirection)
        {
            Debug.Log("Check");
            StartCoroutine(player1Controller.ClashDelay());
            StartCoroutine(player2Controller.ClashDelay());
        }
        else
        {
            if (player1Controller.moveDirection == new Vector2(0,1) && player1.transform.position.x > player2Controller.transform.position.x - 50 && player1.transform.position.x < player2Controller.transform.position.x + 50 && player1.transform.position.y < player2.transform.position.y)
            {
                Destroy(player2);
            }
            if (player1Controller.moveDirection == new Vector2(0, -1) && player1.transform.position.x > player2Controller.transform.position.x - 50 && player1.transform.position.x < player2Controller.transform.position.x + 50 && player1.transform.position.y > player2.transform.position.y)
            {
                Destroy(player2);
            }
            if (player1Controller.moveDirection == new Vector2(1, 0) && player1.transform.position.y > player2Controller.transform.position.y - 50 && player1.transform.position.y < player2Controller.transform.position.y + 50 && player1.transform.position.x < player2.transform.position.x)
            {
                Destroy(player2);
            }
            if (player1Controller.moveDirection == new Vector2(-1, 0) && player1.transform.position.y > player2Controller.transform.position.y - 50 && player1.transform.position.y < player2Controller.transform.position.y + 50 && player1.transform.position.x > player2.transform.position.x)
            {
                Destroy(player2);
            }

            if (player2Controller.moveDirection == new Vector2(0, 1) && player2.transform.position.x > player1Controller.transform.position.x - 50 && player2.transform.position.x < player1Controller.transform.position.x + 50 && player2.transform.position.y < player1.transform.position.y)
            {
                Destroy(player1);
            }
            if (player2Controller.moveDirection == new Vector2(0, -1) && player2.transform.position.x > player1Controller.transform.position.x - 50 && player2.transform.position.x < player1Controller.transform.position.x + 50 && player2.transform.position.y > player1.transform.position.y)
            {
                Destroy(player1);
            }
            if (player2Controller.moveDirection == new Vector2(1, 0) && player2.transform.position.y > player1Controller.transform.position.y - 50 && player2.transform.position.y < player1Controller.transform.position.y + 50 && player2.transform.position.x < player1.transform.position.x)
            {
                Destroy(player1);
            }
            if (player2Controller.moveDirection == new Vector2(-1, 0) && player2.transform.position.y > player1Controller.transform.position.y - 50 && player2.transform.position.y < player1Controller.transform.position.y + 50 && player2.transform.position.x > player1.transform.position.x)
            {
                Destroy(player1);
            }
        }
    }
}
