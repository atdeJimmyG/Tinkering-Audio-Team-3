using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    /*
     * author = Thomas O'Leary
     * GitHub repo = https://github.com/atdeJimmyG/Tinkering-Audio-Team-3
     * copyright = MIT License Copyright (c) 2019
     * license = MIT
     * 
     * These functions were created for debugging purposes.
     * So that I am aware that these buttons do work when they are clicked 
     * whilst the scene is being run.
     */

    // Created a new function called PlayGame()
    public void PlayGame()
    {
        Debug.Log("The Game is now running...");
    }

    // Created a new function called QuitGame()
    public void QuitGame()
    {
        Debug.Log("Game has Quit...");
        Application.Quit();
    }

}
