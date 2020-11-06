using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
    }

    public void Play()
    {
        // Debug.Log("Game Started");
    }

    public void LaunchGame()
    {
        // Debug.Log("Game Launched");
    }

    public void Settings()
    {
        // Debug.Log("Game Preferences Menu open");
    }

    /**
     * Potentially useless and to replace by Restart Game
     */
    public void BackToMainMenu()
    {
        // Debug.Log("Back To Main Menu");
    }

    public void BackToGame()
    {
        // Debug.Log("Back To Game");
    }

    public void RestartGame()
    {
        // Debug.Log("Game Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void Quit()
    {
        // Debug.Log("Game Application Ended");
        Application.Quit();
    }
}