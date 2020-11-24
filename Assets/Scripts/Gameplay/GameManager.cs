using System;
using System.Collections.Generic;
using DefaultNamespace;
using JetBrains.Annotations;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlay
{
    public class GameManager : NetworkBehaviour
    {
        private List<PlayerManager> _playersManagers = new List<PlayerManager>();
        private List<Player> _players = new List<Player>(); // TODO : Player devrait être remplacé par PlayerManager
        private const int MAXPlayerNbr = 4;
        public int PlayerNbr { get; protected set; }
        public int PlayerIndex { get; protected set; }

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
            Finished
        }

        public GameMode GameModeVar { get; protected set; }

        void Start()
        {
            // if (GameSettings.PlayerMode == PlayerMode.Local)
            // {
            //     LaunchGame(2);
            // }
            LaunchGame(2);
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
                _playersManagers[PlayerIndex - 1].Pause();
                mainCamera.enabled = true;
            }
        }

        #region GamePlay

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
            } else if (PlayerIndex >= _playersManagers.Count)
            {
                int playersToManage;
                GameObject go = Instantiate(playerPrefabs);
                var playerConfigs = ConfigManager.instance.getPlayerConfigs().ToArray();
                if (GameSettings.PlayerMode == PlayerMode.Online) {
                    if (PlayerIndex == 0) {
                        go.GetComponent<Player>().initializeConfigs(playerConfigs[PlayerIndex]);
                        _players.Add(go.GetComponent<Player>());
                    }
                }
                else {
                    go.GetComponent<Player>().initializeConfigs(playerConfigs[PlayerIndex]);
                    _players.Add(go.GetComponent<Player>());
                }
                _playersManagers.Add(go.GetComponent<PlayerManager>());
                // Player Creation Post Action called
                OnPlayerCreated?.Invoke(go, PlayerIndex);

            }
            
            _playersManagers[PlayerIndex].Play();
            PlayerIndex++;
        }
        
        #endregion
        
        #region Menus

        public void Continue()
        {
            GameModeVar = GameMode.Running;
            mainCamera.enabled = false;
            _playersManagers[PlayerIndex - 1].Continue();
        }
        
        public void GameOver()
        {
            GameModeVar = GameMode.Finished;
            _playersManagers.ForEach(manager => manager.Destroy());
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

        public Player getCurrentPlayer() {
            return _players[PlayerIndex - 1];
        }
        
        #endregion
    }
    
}
