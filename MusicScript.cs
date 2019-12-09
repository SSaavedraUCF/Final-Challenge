using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioSource musicSource; // For the game object to be place

    public AudioClip musicClipOne; //holder for variable 1

    public AudioClip musicClipTwo; //holder for variable 2


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        musicSource.clip = musicClipOne;
        musicSource.Play();

        musicSource.loop = true;


    }
}