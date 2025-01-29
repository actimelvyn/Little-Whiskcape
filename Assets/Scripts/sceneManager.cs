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
        Cursor.visible = true; // Make cursor visible
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
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
        Cursor.visible = true; // Make cursor visible
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
    }

    // Example: Trigger Win Condition scene
    public void WinCondition()
    {
        SceneManager.LoadScene("WinMenu");
        Cursor.visible = true; // Make cursor visible
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Check if ESC is pressed
        {
            Application.Quit(); // Quit the application
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
#endif
        }
    }

}

