using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.Player
{
    public class LocalPlayerManager : MonoBehaviour
    {
        public Camera Camera;
        public AudioListener Listener;
        public GameObject Interface;
        public PlayerManager playerManager;
        private GameManager _gameManager;
        
        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();

            playerManager.OnPlay += OnPlay;
            playerManager.AfterPlaying += AfterPlaying;
            playerManager.OnPause += OnPause;
            playerManager.OnContinue += OnContinue;
            playerManager.readyToPlay = true;
            if (!_gameManager)
            {
                Debug.LogError("Game Manager not found !");
            }
        }

        public void OnPlay()
        {
            Listener.enabled = true;
            Camera.enabled = true;
            Interface.SetActive(true);
            playerManager.StrokeManager.StopWait();
            StartCoroutine(playerManager.Playing());
        }

        public void AfterPlaying()
        {
            Listener.enabled = false;
            Camera.enabled = false;
            Interface.SetActive(false);
            _gameManager.Next();
        }

        public void OnPause()
        {
            Listener.enabled = false;
            Camera.enabled = false;
        }

        public void OnContinue()
        {
            Listener.enabled = true;
            Camera.enabled = true;
        }
    }
}