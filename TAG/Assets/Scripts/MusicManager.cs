using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioSource youWin;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartMusic()
    {
        audioSource.Play();
    }
}
