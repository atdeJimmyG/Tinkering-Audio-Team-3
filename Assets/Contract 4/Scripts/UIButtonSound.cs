using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonSound : MonoBehaviour{

    /*
     * author = Thomas O'Leary
     * GitHub repo = https://github.com/atdeJimmyG/Tinkering-Audio-Team-3
     * copyright = MIT License Copyright (c) 2019
     * license = MIT
     * 
     * This script is used for playing the AudioSource when the button is clicked
     */

    // Created a public AudioSource variable called sound 
    // where the AudioSource on the GameObject is referenced
    public AudioSource sound;


    // Upon start of this script
    // If sound doesn't already have the AudioSource reference,
    // It grabs the component using GetComponent<>()
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Created a function called PlaySound()
    // That firstly outputs a debug message,
    // Then plays the AudioClip in the AudioSource refernced in sound
    public void PlaySound()
    {
        Debug.Log("Playing sound...");
        sound.Play();
    }
    
}
