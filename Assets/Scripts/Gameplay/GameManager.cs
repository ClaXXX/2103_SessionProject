using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlay
{
    public class GameManager : NetworkBehaviour
    {
        // General GamePlay //
        [Header("GamePlay Settings")]
        private List<PlayerManager> _players = new List<PlayerManager>();
        private const int MAXPlayerNbr = 4;
        public int PlayerNbr { get; protected set; }
        public int PlayerIndex { get; protected set; }

        public GameObject playerPrefabs;
        public Camera mainCamera;
        public GameObject gameOverObj;
        public GameObject pauseObj;

        public enum GameMode
        {
            Running,
            Paused,
            Finished
        }

        public GameMode GameModeVar;
        
        [Header("Network Settings")]

        private Network.NetworkManager _networkManager;

        void Start()
        {
            if (GameSettings.PlayerMode == PlayerMode.Local)
            {
                LaunchGame(2);
                return;
            }

            _networkManager = FindObjectOfType<Network.NetworkManager>();
        }

        void Update()
        {
            if (GameModeVar != GameMode.Running)
            {
                return;
            }

            if (Input.GetKeyUp("escape"))
            {
                GameModeVar = GameMode.Paused;
                pauseObj.SetActive(true);
                _players[PlayerIndex - 1].Pause();
                mainCamera.enabled = true;
            }
        }

        #region GamePlay

        public void LaunchNetworkGame()
        {
        }
        
        public void LaunchGame(int playerNbr)
        {
            if (playerNbr >= MAXPlayerNbr)
            {
                Debug.LogError("Une erreur est survenue avec le nombre de joueur, le nombre maximal est 4");
                playerNbr = 4;
            }
            
            GameModeVar = GameMode.Running;
            PlayerNbr = playerNbr;
            PlayerIndex = 0;
            mainCamera.enabled = false;
            Next();
        }

        public void Next()
        {
            if (GameModeVar == GameMode.Finished)
            {
                return;
            }
            if (PlayerIndex + 1 > PlayerNbr)
            {
                PlayerIndex = 0;
            } else if (PlayerIndex >= _players.Count)
            {
                GameObject go = Instantiate(playerPrefabs);
                _players.Add(go.GetComponent<PlayerManager>());
            }
            
            _players[PlayerIndex].Play();
            PlayerIndex++;
        }

        public void Continue()
        {
            GameModeVar = GameMode.Running;
            mainCamera.enabled = false;
            _players[PlayerIndex - 1].Continue();
        }
        
        public void GameOver()
        {
            GameModeVar = GameMode.Finished;
            _players.ForEach(manager => Destroy(manager.gameObject));
            mainCamera.enabled = true;
            gameOverObj.SetActive(true);
        }

        public void Replay()
        {
            LoadScene.LoadTo(SceneManager.GetActiveScene().name);
        }

        #endregion

        public void Quit()
        {
            LoadScene.LoadTo("Main");
        }
        
        #region Network
        
        public void CreateLocalPlayer()
        {
            GameObject go = Instantiate(playerPrefabs);
            _players.Add(go.GetComponent<PlayerManager>());
        }

        public void CreateOnlinePlayer()
        {
            GameObject go = Instantiate(playerPrefabs);
            _players.Add(go.GetComponent<PlayerManager>());
            NetworkServer.Spawn(go);
        }
        
        public void ClientConnect()
        {
            Debug.Log("Client Added");
            PlayerNbr++;
        }

        #endregion
    }
    
}
