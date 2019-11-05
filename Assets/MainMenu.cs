using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
   
    public void PlayGame()
    {
        Debug.Log("The Game is now running...");
    }

    public void QuitGame()
    {
        Debug.Log("Game has Quit...");
        Application.Quit();
    }

}
