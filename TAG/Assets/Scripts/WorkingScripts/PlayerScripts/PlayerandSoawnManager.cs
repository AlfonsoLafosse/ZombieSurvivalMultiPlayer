using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerandSoawnManager : MonoBehaviour
{
    [SerializeField] public Transform[] playerSpawnPositions;
    public List<GameObject> team1;
    public List<GameObject> team2;
    public PlayerCamera playerCamera;
    public GameObject crownObject;
    public bool teamCheck;
   
    public float crownCollectDelay = 1.0f; 
    public bool canCollectCrown = true;

    public List<GameObject> _PlayerObject = new List<GameObject>();
    public GameObject _CrownObject = null;
    [SerializeField] private PlayerInputManager playerInputManager;
    
    
    // Start is called before the first frame update
    private void Awake()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    // Update is called once per frame

    public void PlayersCollided(GameObject player, CharacterController playerController, GameObject otherPlayer, CharacterController otherPlayerController)
    {
        if (playerController.moveDirection == -otherPlayerController.moveDirection)
        {
            StartCoroutine(playerController.ClashDelay());
            StartCoroutine(otherPlayerController.ClashDelay());
            if (playerController.hasCrown && canCollectCrown == true)
            {
                playerController.hasCrown = false;
                Instantiate(crownObject, player.transform.position, Quaternion.identity);
                StartCoroutine(StartCrownCollectDelay());
            }

            if (otherPlayerController.hasCrown && canCollectCrown == true)
            {
                otherPlayerController.hasCrown = false;
                Instantiate(crownObject, otherPlayer.transform.position, Quaternion.identity);
                StartCoroutine(StartCrownCollectDelay());
            }
        }
        else
        {
            if (playerController.moveDirection == new Vector2(0,1) && player.transform.position.x > otherPlayerController.transform.position.x - 50 && player.transform.position.x < otherPlayerController.transform.position.x + 50 && player.transform.position.y < otherPlayer.transform.position.y)
            {
                DestroyPlayer(otherPlayer, player);
            }
            if (playerController.moveDirection == new Vector2(0, -1) && player.transform.position.x > otherPlayerController.transform.position.x - 50 && player.transform.position.x < otherPlayerController.transform.position.x + 50 && player.transform.position.y > otherPlayer.transform.position.y)
            {
                DestroyPlayer(otherPlayer, player);
            }
            if (playerController.moveDirection == new Vector2(1, 0) && player.transform.position.y > otherPlayerController.transform.position.y - 50 && player.transform.position.y < otherPlayerController.transform.position.y + 50 && player.transform.position.x < otherPlayer.transform.position.x)
            {
                DestroyPlayer(otherPlayer, player);
            }
            if (playerController.moveDirection == new Vector2(-1, 0) && player.transform.position.y > otherPlayerController.transform.position.y - 50 && player.transform.position.y < otherPlayerController.transform.position.y + 50 && player.transform.position.x > otherPlayer.transform.position.x)
            {
                DestroyPlayer(otherPlayer, player);
            }

            if (otherPlayerController.moveDirection == new Vector2(0, 1) && otherPlayer.transform.position.x > playerController.transform.position.x - 50 && otherPlayer.transform.position.x < playerController.transform.position.x + 50 && otherPlayer.transform.position.y < player.transform.position.y)
            {
                DestroyPlayer(player, otherPlayer);
            }
            if (otherPlayerController.moveDirection == new Vector2(0, -1) && otherPlayer.transform.position.x > playerController.transform.position.x - 50 && otherPlayer.transform.position.x < playerController.transform.position.x + 50 && otherPlayer.transform.position.y > player.transform.position.y)
            {
                DestroyPlayer(player, otherPlayer);
            }
            if (otherPlayerController.moveDirection == new Vector2(1, 0) && otherPlayer.transform.position.y > playerController.transform.position.y - 50 && otherPlayer.transform.position.y < playerController.transform.position.y + 50 && otherPlayer.transform.position.x < player.transform.position.x)
            {
                DestroyPlayer(player, otherPlayer);
            }
            if (otherPlayerController.moveDirection == new Vector2(-1, 0) && otherPlayer.transform.position.y > playerController.transform.position.y - 50 && otherPlayer.transform.position.y < playerController.transform.position.y + 50 && otherPlayer.transform.position.x > player.transform.position.x)
            {
                DestroyPlayer(player, otherPlayer);
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
        player.transform.position = playerSpawnPositions[_PlayerObject.IndexOf(player.gameObject)].position;
        StartCoroutine(player.GetComponent<CharacterController>().SpawnI());
    }
}
