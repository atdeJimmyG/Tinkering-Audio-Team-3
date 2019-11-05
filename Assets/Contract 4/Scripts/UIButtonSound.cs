using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonSound : MonoBehaviour{

    public AudioSource sound;
    public AudioClip hoverSound;
    public AudioClip clickSound;


    public void HoverSound()
    {
        sound.PlayOneShot(hoverSound);
    }
    public void ClickSound()
    {
        sound.PlayOneShot(clickSound);
    }


}
