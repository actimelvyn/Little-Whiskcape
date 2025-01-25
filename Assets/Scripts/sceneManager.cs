using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class sceneManager : MonoBehaviour
{
 
    /*private void Start()
    {
        SceneManager.LoadScene("EntranceMenu");
    }*/
    // Method to load a scene by name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        print("loaded " + sceneName);
    }

    // Method to quit the game
    public void QuitGame()
    {
        Application.Quit();
    }

    // Example: Trigger Game Over scene
    public void GameOver()
    {
        SceneManager.LoadScene("DeathMenu");
    }

    // Example: Trigger Win Condition scene
    public void WinCondition()
    {
        SceneManager.LoadScene("WinMenu");
    }

 
}

