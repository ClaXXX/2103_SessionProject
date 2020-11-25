﻿using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlay
{
    public class GameManager : MonoBehaviour
    {
        private readonly List<PlayerManager> _playersManagers = new List<PlayerManager>();
        private List<global::Player> _players = new List<global::Player>(); // TODO : Player devrait être remplacé par PlayerManager
        private const int MAXPlayerNbr = 4;
        public int PlayerNbr { get; protected set; }

        public int PlayerIndex { get; protected set; }
        public int BotIndex { get; protected set; }

        // Events
        public Action<GameObject, int> OnPlayerCreated;
        public Action OnGameLaunched;
        public Action OnGameOver;

        [Header("General")]
        public Camera mainCamera;
        public Transform position;
        
        [Header("Prefabs")]
        public GameObject playerPrefabs; // to change to Network Player prefabs ?
        public GameObject botPlayerPrefabs;
        
        [Header("Menus")]
        public GameObject gameOverObj;
        public GameObject pauseObj;

        [Header("Network")]
        public GameObject networkGameOverObj;

        public enum GameMode
        {
            Running,
            Paused,
            Finished,
            Undefined
        }

        public GameMode GameModeVar { get; protected set; }

        /**
         * <summary>Function called when the Game scene is loaded</summary>
         */
        void Start()
        {
            GameModeVar = GameMode.Undefined;
            // Local game setting init
            if (GameSettings.PlayerMode == PlayerMode.Local)
            {
                // Set all local events
                OnPlayerCreated = onLocalPlayerCreated;
                OnGameLaunched = onLocalGameLaunched;
                OnGameOver = onLocalGameOver;
                FindObjectOfType<ScoringCollider>().OnGameWin = GameOver;

                // then launch the game
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
                _playersManagers[PlayerIndex - 1].Pause();
                mainCamera.enabled = true;
            }
        }

        #region GamePlay

        /**
         * <param name="playerNbr">Number of player to play for the game</param>
         * <param name="botNbr">Number of bots to play against players (default: 0)</param>
         * <summary>Launch the game process</summary>
         */
        public void LaunchGame(int playerNbr, int botNbr = 0)
        {
            // Cannot launch the game twice
            if (GameModeVar == GameMode.Running)
            {
                return;
            }
            // if too much player, reset the player number to the maximum
            if (playerNbr > MAXPlayerNbr)
            {
                Debug.LogError("Une erreur est survenue avec le nombre de joueur, le nombre maximal est 4");
                playerNbr = 4;
            }

            if (playerNbr + botNbr > MAXPlayerNbr)
            {
                botNbr = MAXPlayerNbr - playerNbr;
            }
            
            GameModeVar = GameMode.Running;
            PlayerNbr = playerNbr + botNbr;
            PlayerIndex = 0;
            BotIndex = playerNbr;
            OnGameLaunched?.Invoke();
            Next();
        }

        void CreatePlayer()
        {
            GameObject go;
            
            if (PlayerIndex < BotIndex)
            {
               go = Instantiate(playerPrefabs, position);
            GameObject go = Instantiate(playerPrefabs, position);
            var playerConfigs = ConfigManager.instance.getPlayerConfigs().ToArray();
            
            if (GameSettings.PlayerMode == PlayerMode.Online) {
                if (PlayerIndex == 0) {
                    go.GetComponent<global::Player>().initializeConfigs(playerConfigs[PlayerIndex]);
                    _players.Add(go.GetComponent<global::Player>());
                }
            }
            else {
                go.GetComponent<global::Player>().initializeConfigs(playerConfigs[PlayerIndex]);
                _players.Add(go.GetComponent<global::Player>());
            }
            
               // Player Creation Post Action called
               OnPlayerCreated?.Invoke(go, PlayerIndex);
            }
            else
            {
                go = Instantiate(botPlayerPrefabs, position);
            }

            // GameManager
            _playersManagers.Add(go.GetComponent<PlayerManager>());
        }

        /**
         * <summary>Pass to the next player if it exist, otherwise create it</summary>
         */
        public void Next()
        {
            // If the game is finished then stop the game
            if (GameModeVar == GameMode.Finished)
            {
                return;
            }
            
            // pass to first player turn
            if (PlayerIndex + 1 > PlayerNbr)
            {
                PlayerIndex = 0;
            } else if (PlayerIndex >= _playersManagers.Count)
            {
                CreatePlayer();
            }
            Debug.Log("Player about to play");

            // Let's play the next player
            _playersManagers[PlayerIndex].Play();
            PlayerIndex++;
        }
        
        #endregion

        #region Events

        void onLocalPlayerCreated(GameObject go, int index)
        {
            PlayerConfigs[] playerConfigs = ConfigManager.instance.getPlayerConfigs().ToArray();

            go.GetComponent<global::Player>().initializeConfigs(playerConfigs[index]);
        }

        void onLocalGameLaunched()
        {
            mainCamera.enabled = true;
        }

        void onLocalGameOver()
        {
            _playersManagers.ForEach(manager => manager.Destroy());
            mainCamera.enabled = true;
            gameOverObj.SetActive(true);
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
            OnGameOver?.Invoke();
        }

        public void Replay()
        {
            LoadScene.LoadTo(SceneManager.GetActiveScene().name);
        }
        
        public void Quit()
        {
            LoadScene.LoadTo("Main");
        }

        public global::Player getCurrentPlayer() {
            return _players[PlayerIndex - 1];
        }
        
        #endregion
    }
    
}
