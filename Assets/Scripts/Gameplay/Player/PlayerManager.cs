using System;
using System.Collections;
using GamePlay;
using Gameplay.Stroke_Managers;
using Mirror;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    public StrokeManager StrokeManager;
    public string PlayerName { get; protected set; } = "Player"; // default name set
    public Action OnPlay;
    public Action AfterPlaying;
    public Action OnPause;
    public Action OnContinue;
    public bool readyToPlay = false;

    public void setPlayerName(string playerName)
    {
        if (string.IsNullOrEmpty(playerName)) // Checkout that we don't set player name to a null or empty string
            return;
        PlayerName = playerName;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Pause()
    {
        OnPause?.Invoke();
        StrokeManager.Paused();
    }

    public void Continue()
    {
        OnContinue?.Invoke();
        StrokeManager.Continue();
    }

    public void Play()
    {
        Debug.Log("Player is playing");
        if (!readyToPlay)
        {
            StartCoroutine(TryToPlay());
        }
        else
        {
            OnPlay?.Invoke();
        }
    }

    IEnumerator TryToPlay()
    {
        yield return new WaitUntil((() => readyToPlay));
        OnPlay?.Invoke();
    }

    public IEnumerator Playing()
    {
        yield return new WaitUntil(() => (StrokeManager.StrokeModeVar == StrokeMode.Waiting));
        Debug.Log("Player has playing");
        AfterPlaying?.Invoke();
    }
}
