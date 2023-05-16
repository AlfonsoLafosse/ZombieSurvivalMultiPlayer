using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerandSoawnManager : MonoBehaviour
{
    [SerializeField] public GameObject player1;
    [SerializeField] public GameObject player2;
    [SerializeField] private GameObject player1prefab;
    [SerializeField] private GameObject player2prefab;
    private CharacterController player1Controller;
    private CharacterController player2Controller;
    private Vector3 player1Position;
    private Vector3 player2Position;
    public PlayerCamera playerCamera;
    public GameObject crownObject;
   
    public float crownCollectDelay = 1.0f; 
    public bool canCollectCrown = true;

    public List<GameObject> _PlayerObject = new List<GameObject>();
    public GameObject _CrownObject = null;
    
    
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
            //_PlayerObject.Add(player1);
            playerCamera.FindTargets();
            player1Controller = player1.GetComponent<CharacterController>();
        }
        if (player2 == null)
        {
            player2 =Instantiate(player2prefab, player2Position, Quaternion.identity);
            playerCamera.FindTargets();
            player2Controller = player2.GetComponent<CharacterController>();
            //_PlayerObject.Add(player2);
        }
    }
    public void PlayersCollided()
    {
        if (player1Controller.moveDirection == -player2Controller.moveDirection)
        {
            StartCoroutine(player1Controller.ClashDelay());
            StartCoroutine(player2Controller.ClashDelay());
            if (player1.GetComponent<CharacterController>().hasCrown && canCollectCrown == true)
            {
                player1.GetComponent<CharacterController>().hasCrown = false;
                Instantiate(crownObject, player1.transform.position, Quaternion.identity);
                StartCoroutine(StartCrownCollectDelay());
            }

            if (player2.GetComponent<CharacterController>().hasCrown && canCollectCrown == true)
            {
                player2.GetComponent<CharacterController>().hasCrown = false;
                Instantiate(crownObject, player2.transform.position, Quaternion.identity);
                StartCoroutine(StartCrownCollectDelay());
            }
        }
        else
        {
            if (player1Controller.moveDirection == new Vector2(0,1) && player1.transform.position.x > player2Controller.transform.position.x - 50 && player1.transform.position.x < player2Controller.transform.position.x + 50 && player1.transform.position.y < player2.transform.position.y)
            {
                DestroyPlayer(player2, player1);
            }
            if (player1Controller.moveDirection == new Vector2(0, -1) && player1.transform.position.x > player2Controller.transform.position.x - 50 && player1.transform.position.x < player2Controller.transform.position.x + 50 && player1.transform.position.y > player2.transform.position.y)
            {
                DestroyPlayer(player2, player1);
            }
            if (player1Controller.moveDirection == new Vector2(1, 0) && player1.transform.position.y > player2Controller.transform.position.y - 50 && player1.transform.position.y < player2Controller.transform.position.y + 50 && player1.transform.position.x < player2.transform.position.x)
            {
                DestroyPlayer(player2, player1);
            }
            if (player1Controller.moveDirection == new Vector2(-1, 0) && player1.transform.position.y > player2Controller.transform.position.y - 50 && player1.transform.position.y < player2Controller.transform.position.y + 50 && player1.transform.position.x > player2.transform.position.x)
            {
                DestroyPlayer(player2, player1);
            }

            if (player2Controller.moveDirection == new Vector2(0, 1) && player2.transform.position.x > player1Controller.transform.position.x - 50 && player2.transform.position.x < player1Controller.transform.position.x + 50 && player2.transform.position.y < player1.transform.position.y)
            {
                DestroyPlayer(player1, player2);
            }
            if (player2Controller.moveDirection == new Vector2(0, -1) && player2.transform.position.x > player1Controller.transform.position.x - 50 && player2.transform.position.x < player1Controller.transform.position.x + 50 && player2.transform.position.y > player1.transform.position.y)
            {
                DestroyPlayer(player1, player2);
            }
            if (player2Controller.moveDirection == new Vector2(1, 0) && player2.transform.position.y > player1Controller.transform.position.y - 50 && player2.transform.position.y < player1Controller.transform.position.y + 50 && player2.transform.position.x < player1.transform.position.x)
            {
                DestroyPlayer(player1, player2);
            }
            if (player2Controller.moveDirection == new Vector2(-1, 0) && player2.transform.position.y > player1Controller.transform.position.y - 50 && player2.transform.position.y < player1Controller.transform.position.y + 50 && player2.transform.position.x > player1.transform.position.x)
            {
                DestroyPlayer(player1, player2);
            }
        }
    }
    private IEnumerator StartCrownCollectDelay()
    {
        canCollectCrown = false;
        yield return new WaitForSeconds(crownCollectDelay);
        canCollectCrown = true;
    }
    private void DestroyPlayer(GameObject player, GameObject otherPlayer)
    {
        if (player.GetComponent<CharacterController>().hasCrown == true)
        {
            player.GetComponent<CharacterController>().hasCrown = false;
            otherPlayer.GetComponent<CharacterController>().hasCrown = true;
        }
        _PlayerObject.Remove(player);
        Destroy(player);
    }
}
