using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PlayerUIScript : MonoBehaviour
{
    public Sprite[] team1Sprites;
    public Sprite[] team2Sprites;
    public Sprite unassignedSprite;
    public Image playerImage;
    public TextMeshProUGUI teamText;
    public TextMeshProUGUI playerText;
    public List<Color> teamColors;
    public List<Color> player1Colors;
    public List<Color> player2Colors;
    public AudioSource playerAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
