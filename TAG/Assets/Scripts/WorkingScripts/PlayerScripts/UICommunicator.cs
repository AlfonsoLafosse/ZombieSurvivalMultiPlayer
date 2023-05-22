using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UICommunicator : MonoBehaviour
{
    public int playerInt;
    private PlayerandSoawnManager playerandSoawnManager;
    private CharacterController characterController;
    private PlayerUIScript playerUIScript;
    private PlayerInput playerInput;
    // Start is called before the first frame update
    void Awake()
    {
        playerandSoawnManager = FindObjectOfType<PlayerandSoawnManager>();
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        playerandSoawnManager.UICommunicators.Add(this);
        playerInput.actions["Swap"].Enable();
        playerInput.actions["Start"].Enable();
        playerInt = playerandSoawnManager._PlayerObject.Count;
        playerandSoawnManager.UIElements[playerInt].SetActive(true);
        playerUIScript = playerandSoawnManager.UIElements[playerInt].GetComponent<PlayerUIScript>();
        playerUIScript.playerText.text = "Player " + (playerInt + 1).ToString();
        if (playerandSoawnManager.team1.Count < 2)
        {
            playerandSoawnManager.team1.Add(this.gameObject);
            playerUIScript.teamText.text = "Team 1";
            playerUIScript.playerImage.sprite = playerUIScript.team1Sprites[playerandSoawnManager.team1.IndexOf(this.gameObject)];
            characterController.playerSprite.sprite = playerUIScript.team1Sprites[playerandSoawnManager.team1.IndexOf(this.gameObject)];
        }
        else
        {
            playerandSoawnManager.team2.Add(this.gameObject);
            playerUIScript.teamText.text = "Team 2";
            playerUIScript.playerImage.sprite = playerUIScript.team2Sprites[playerandSoawnManager.team2.IndexOf(this.gameObject)];
            characterController.playerSprite.sprite = playerUIScript.team2Sprites[playerandSoawnManager.team2.IndexOf(this.gameObject)];
        }
    }

    // Update is called once per frame
    void Update()
    {
            playerInput.actions["Swap"].performed += SwapTeam;
            playerInput.actions["Start"].performed += StartGame;
    }
    private void SwapTeam(InputAction.CallbackContext context)
    {
        Debug.Log("TeamSwapped");
        if (playerandSoawnManager.gameStarted == false)
        {
            if (playerandSoawnManager.team1.Contains(this.gameObject))
            {
                if (playerandSoawnManager.team2.Count < 2)
                {
                    playerandSoawnManager.team1.Remove(this.gameObject);
                    playerandSoawnManager.team2.Add(this.gameObject);
                    playerUIScript.teamText.text = "Team 2";
                    playerUIScript.playerImage.sprite = playerUIScript.team2Sprites[playerandSoawnManager.team2.IndexOf(this.gameObject)];
                    characterController.playerSprite.sprite = playerUIScript.team2Sprites[playerandSoawnManager.team2.IndexOf(this.gameObject)];
                }
            }
            else if (playerandSoawnManager.team2.Contains(this.gameObject))
            {
                if (playerandSoawnManager.team1.Count < 2)
                {
                    playerandSoawnManager.team2.Remove(this.gameObject);
                    playerandSoawnManager.team1.Add(this.gameObject);
                    playerUIScript.teamText.text = "Team 1";
                    playerUIScript.playerImage.sprite = playerUIScript.team1Sprites[playerandSoawnManager.team1.IndexOf(this.gameObject)];
                    characterController.playerSprite.sprite = playerUIScript.team1Sprites[playerandSoawnManager.team1.IndexOf(this.gameObject)];
                }
            }
        }
    }
    private void StartGame(InputAction.CallbackContext context)
    {
        playerandSoawnManager.StartGame();
    }

    }
