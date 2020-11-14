using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI {
    public class MainMenuScript : MonoBehaviour {

        [SerializeField] private Dropdown player1Controls;
        [SerializeField] private Dropdown player2Controls;
        [SerializeField] private Dropdown player2Select;

        private PlayerAssembler playerAssembler;
        private ConfigManager _configManager; // TODO : Devrait recevoir notre GameInitializer
        
        private Transform menuPanel;

        public void Start() {
            playerAssembler = new PlayerAssembler();
            _configManager = ConfigManager.instance;
        }

        public void quitGame() {
            Application.Quit();
        }

        public void configureGame() {
                int player1ControlsValue = player1Controls.value;
                int player2ControlsValue = player2Controls.value;

                int player2SelectValue = player2Select.value;
                
                PlayerDto[] players = new PlayerDto[2];

                players[0] = playerAssembler.assemble(player1ControlsValue, 1);
                players[1] = playerAssembler.assemble(player2ControlsValue, player2SelectValue);
                
                
                _configManager.addPlayers(players);
            
            
                
                LoadingData.sceneToLoad = "Game";
                SceneManager.LoadScene("Loading");
        }
    }
}