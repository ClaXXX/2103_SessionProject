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
        LoadingData.sceneToLoad = "Game";
        SceneManager.LoadScene("Loading");
    }

    public void Settings()
    {
        // Debug.Log("Game Preferences Menu open");
    }
    
    public void BackToMainMenu()
    {
        LoadingData.sceneToLoad = "Menus";
        SceneManager.LoadScene("Loading");
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