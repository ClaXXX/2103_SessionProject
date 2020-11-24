using System.Collections;
using GamePlay;
using Gameplay.Stroke_Managers;
using Mirror;
using UnityEngine;


public class PlayerManager : NetworkBehaviour
{
    public StrokeManager StrokeManager;
    public Camera Camera;
    public GameObject Interface;
    private GameManager _gameManager;
    public string PlayerName { get; protected set; } = "Player"; // default name set

    public void setPlayerName(string playerName)
    {
        if (string.IsNullOrEmpty(playerName)) // Checkout that we don't set player name to a null or empty string
            return;
        PlayerName = playerName;
    }

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

        if (!_gameManager)
        {
            Debug.LogError("Game Manager not found !");
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Pause()
    {
        Camera.enabled = false;
        StrokeManager.Paused();
    }

    public void Continue()
    {
        Camera.enabled = true;
        StrokeManager.Continue();
    }

    public void Play()
    {
        Camera.enabled = true;
        StrokeManager.StopWait();
        Interface.SetActive(true);
        StartCoroutine(Playing());
    }

    IEnumerator Playing()
    {
        yield return new WaitUntil(() => (StrokeManager.StrokeModeVar == StrokeMode.Waiting));
        Camera.enabled = false;
        Interface.SetActive(false);
        _gameManager.Next();
    }
}
