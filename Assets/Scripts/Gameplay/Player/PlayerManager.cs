using System.Collections;
using GamePlay;
using Gameplay.Stroke_Managers;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    public StrokeManager StrokeManager;
    public Camera Camera;
    public GameObject Interface;
    private GameManager _gameManager;

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
