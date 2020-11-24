using System;
using System.Collections.Generic;
using DefaultNamespace;
using JetBrains.Annotations;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using NetworkManager = Network.NetworkManager;

namespace GamePlay
{
    public class GameManager : MonoBehaviour
    {
        private readonly List<PlayerManager> _players = new List<PlayerManager>();
        private const int MAXPlayerNbr = 4;
        public int PlayerNbr { get; protected set; }
        public int PlayerIndex { get; protected set; }
        
        // Events
        public Action<GameObject, int> OnPlayerCreated;

        [Header("General")]
        public GameObject playerPrefabs; // to change to Network Player prefabs ?
        public Camera mainCamera;
        
        [Header("Menus")]
        public GameObject gameOverObj;
        public GameObject pauseObj;

        public enum GameMode
        {
            Running,
            Paused,
            Finished,
            Undefined
        }

        public GameMode GameModeVar { get; protected set; }

        void Start()
        {
            GameModeVar = GameMode.Undefined;
            if (GameSettings.PlayerMode == PlayerMode.Local)
            {
                OnPlayerCreated = onLocalPlayerCreated;
                LaunchGame(2);
            }
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

        public void LaunchGame(int playerNbr)
        {
            Debug.Log("On the Launch method beginning");
            if (GameModeVar == GameMode.Running)
            {
                Debug.Log("Game Already running");
                return;
            }
            if (playerNbr >= MAXPlayerNbr)
            {
                Debug.LogError("Une erreur est survenue avec le nombre de joueur, le nombre maximal est 4");
                playerNbr = 4;
            }
            
            GameModeVar = GameMode.Running;
            PlayerNbr = playerNbr;
            PlayerIndex = 0;
            mainCamera.enabled = false;
            Debug.Log("Call Launcg method");
            Next();
        }

        void onLocalPlayerCreated(GameObject go, int index)
        {
            PlayerConfigs[] playerConfigs = ConfigManager.instance.getPlayerConfigs().ToArray();

            go.GetComponent<Player>().initializeConfigs(playerConfigs[index]);
        }

        void CreatePlayer()
        {
            GameObject go = Instantiate(playerPrefabs);

            // GameManager
            _players.Add(go.GetComponent<PlayerManager>());
            
            // Player Creation Post Action called
            Debug.Log("Call OnPlayerCreated method");
            OnPlayerCreated?.Invoke(go, PlayerIndex);
        }

        public void Next()
        {
            Debug.Log("Enter the Next GameManager method");
            if (GameModeVar == GameMode.Finished)
            {
                return;
            }
            
            if (PlayerIndex + 1 > PlayerNbr)
            {
                PlayerIndex = 0;
            } else if (PlayerIndex >= _players.Count)
            {
                CreatePlayer();
            }
            
            _players[PlayerIndex].Play();
            PlayerIndex++;
        }
        
        #endregion
        
        #region Menus

        public void Continue()
        {
            GameModeVar = GameMode.Running;
            mainCamera.enabled = false;
            _players[PlayerIndex - 1].Continue();
        }
        
        public void GameOver()
        {
            GameModeVar = GameMode.Finished;
            _players.ForEach(manager => manager.Destroy());
            mainCamera.enabled = true;
            gameOverObj.SetActive(true);
        }

        public void Replay()
        {
            LoadScene.LoadTo(SceneManager.GetActiveScene().name);
        }
        
        public void Quit()
        {
            LoadScene.LoadTo("Main");
        }
        
        #endregion
    }
    
}
