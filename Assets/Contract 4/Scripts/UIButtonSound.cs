using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonSound : MonoBehaviour{

    public AudioSource sound;

    void Start()
    {
        sound = GetComponent<AudioSource>();
    }
    public void PlaySound()
    {
        Debug.Log("Playing sound...");
        sound.Play();
    }
    
}
