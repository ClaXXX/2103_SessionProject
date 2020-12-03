﻿using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI {
    public class MainMenuScript : MonoBehaviour {

        [SerializeField] private Dropdown onlinePlayerControls;
        [SerializeField] private Dropdown localPlayer1Controls;
        [SerializeField] private Dropdown localPlayer2Controls;
        [SerializeField] private Dropdown player2Select;

        private PlayerAssembler playerAssembler;
        private ConfigManager _configManager;
        
        private Transform menuPanel;

        public void Start() {
            playerAssembler = new PlayerAssembler();
            _configManager = ConfigManager.instance;
        }

        public void quitGame() {
            Application.Quit();
        }

        public void configureLocalGame() {
                int player1ControlsValue = localPlayer1Controls.value;
                int player2ControlsValue = localPlayer2Controls.value;

                int player2SelectValue = player2Select.value;
                
                PlayerDto[] players = new PlayerDto[2];

                players[0] = playerAssembler.assemble(player1ControlsValue, 1, 1);
                players[1] = playerAssembler.assemble(player2ControlsValue, player2SelectValue, 2);
            
                _configManager.addPlayers(players);
            
            
                
                LoadingData.sceneToLoad = "NetworkGame";
                SceneManager.LoadScene("Loading");
        }

        public void configureOnlineGame() {
            int player1ControlsValue = onlinePlayerControls.value;
            PlayerDto[] players = new PlayerDto[1];
            players[0] = playerAssembler.assemble(player1ControlsValue, 1, 1);
            
            Debug.Log("Player Configuration added");
            _configManager.addPlayers(players);
        }

        public void initPlayerOneDropdowns() {
            string player1ControlType = PlayerPrefs.GetString("Player1ControlType");
            if (player1ControlType == "Keyboard") {
                localPlayer1Controls.value = 0;
                //onlinePlayerControls.value = 0;
            } else if (player1ControlType == "Gamepad") {
                localPlayer1Controls.value = 1;
                //onlinePlayerControls.value = 1;
            }
            else {
                localPlayer1Controls.value = 0;
                //onlinePlayerControls.value = 0;
            }
        }

        public void initPlayerTwoDropdown() {
            string player2ControlType = PlayerPrefs.GetString("Player2ControlType");
            if (player2ControlType == "Keyboard") {
                localPlayer2Controls.value = 0;
            } else if (player2ControlType == "Gamepad") {
                localPlayer2Controls.value = 1;
            }
            else {
                localPlayer2Controls.value = 1;
            }
        }
    }
}