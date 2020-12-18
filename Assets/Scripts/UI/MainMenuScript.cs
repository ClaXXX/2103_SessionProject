using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI {
    public class MainMenuScript : MonoBehaviour {

        [SerializeField] private Dropdown onlinePlayerControls;
        [SerializeField] private Dropdown localPlayer1Controls;
        [SerializeField] private Dropdown localPlayer2Controls;
        [SerializeField] private Dropdown player2Select;

        #region UIGameObjects

        [SerializeField] private GameObject LowerLocalGameSetterGameObject;
        [SerializeField] private GameObject UpperLocalGameSetterGameObject;
        // TODO : Add main menu thing here
        [SerializeField] private TextMeshProUGUI title;

        #endregion
        
        #region VisibleCoordinates

        [SerializeField] private Transform LowerLocalGameSetterVisiblePosition;
        [SerializeField] private Transform UpperLocalGameSetterVisiblePosition;
        // TODO : Add main menu things here

        #endregion
        

        public bool toMainMenu;
        public bool toGameConfig;
        public bool toGame;
        
        
        private PlayerAssembler playerAssembler;
        private ConfigManager _configManager;
        
        private Transform menuPanel;

        public void Start() {
            playerAssembler = new PlayerAssembler();
            _configManager = ConfigManager.instance;
        }

        public void Update() {
            float time = Time.deltaTime;
            if (toMainMenu) {
                // TODO : Implement
            }

            if (true) {
                easeOutMainMenu(time);
                // TODO : Une fois le main menu "out", on ease "in" le play menu
                if (true) {
                    easeInPlayPage(time);
                }
            }

            if (toGame) {
                
            }
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

        public void setToMainMenu(bool boolean) {
            toMainMenu = boolean;
        }
        
        public void setToPlayMenu(bool boolean) {
            toGameConfig = boolean;
        }
        
        private void easeOutMainMenu(float time) {
            // TODO : Mth that is call to move menu items   
            Color color = title.color;
            color.a = interpolateFloat(color.a, 0, time);
            title.color = color;
        }

        private void easeOutPlayPage(float time) {
            // TODO : Mth that is call to move menu items   
        }

        private void easeInPlayPage(float time) {
            bool isUpperDone = false;
            bool isLowerDone = false;

            if (UpperLocalGameSetterGameObject.transform.position.y - 0.1f < UpperLocalGameSetterVisiblePosition.position.y) {
                UpperLocalGameSetterGameObject.transform.position = UpperLocalGameSetterVisiblePosition.position;
                isUpperDone = true;
            }
            else {
                UpperLocalGameSetterGameObject.transform.position =
                    interpolateToPosition(UpperLocalGameSetterGameObject.transform.position,
                        UpperLocalGameSetterVisiblePosition.position, time);
            }
            
            if (LowerLocalGameSetterGameObject.transform.position.y > LowerLocalGameSetterVisiblePosition.position.y - 0.1f) {
                LowerLocalGameSetterGameObject.transform.position = LowerLocalGameSetterVisiblePosition.position;
                isLowerDone = true;
            }
            else {
                LowerLocalGameSetterGameObject.transform.position =
                    interpolateToPosition(LowerLocalGameSetterGameObject.transform.position,
                        LowerLocalGameSetterVisiblePosition.position, time);
            }

            if (isLowerDone && isUpperDone) {
                toGameConfig = false;
            }
        }

        private void makeButtonRainbow(float time) {
            // TODO : Mth that is called to interpolate button color
        }
        private Vector3 interpolateToPosition(Vector3 a, Vector3 b, float t) {
            t = Mathf.Clamp01(t); 
            
            t = (2f * t) - (2f * t * t);

            return new Vector3(t * b.x + (1 - t) * a.x,
                t * b.y + (1 - t) * a.y, 
                t * b.z + (1 - t) * a.z);
        }

        private float interpolateFloat(float a, float b, float time) {
            time = Mathf.Clamp01(time);
            time = (2f * time) - (2f * time * time);  
            
            return time * b + (1 - time) * a;
        }
    }
}