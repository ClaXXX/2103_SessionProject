using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI {
    public class MainMenuScript : MonoBehaviour {

        [SerializeField] private Dropdown onlinePlayerControls;
        [SerializeField] private Dropdown localPlayer1Controls;
        [SerializeField] private Dropdown localPlayer2Controls;
        [SerializeField] private Dropdown player2Select;
        [SerializeField] private InputField seedInput;

        #region UIGameObjects

        [SerializeField] private GameObject LowerLocalGameSetterGameObject;
        [SerializeField] private GameObject UpperLocalGameSetterGameObject;
        // TODO : Add main menu thing here
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Image playButton;
        [SerializeField] private TextMeshProUGUI playButtonText;
        [SerializeField] private Image quitButton;
        [SerializeField] private TextMeshProUGUI quitButtonText;
        [SerializeField] private Image settingsButton;
        [SerializeField] private TextMeshProUGUI settingsButtonText;
        [SerializeField] private Image onlineButton;
        [SerializeField] private TextMeshProUGUI onlineButtonText;
        [SerializeField] private Image proceduralButton;
        [SerializeField] private TextMeshProUGUI proceduralButtonText;

        #endregion
        
        #region VisibleCoordinates

        [SerializeField] private Transform LowerLocalGameSetterVisiblePosition;
        private Vector3 LowerLocalGameSetterBasePosition;
        [SerializeField] private Transform UpperLocalGameSetterVisiblePosition;
        Vector3 UpperLocalGameSetterBasePosition;
        // TODO : Add main menu things here

        #endregion
        

        public bool toMainMenu;
        public bool toGameConfig;
        public bool isGameSetterEasingOut;
        public bool isGameSetterEasingIn;


        private PlayerAssembler playerAssembler;
        private ConfigManager _configManager;
        private SeedManager _seedManager;
        
        private Transform menuPanel;

        public void Awake() {
            playerAssembler = new PlayerAssembler();
            _seedManager = SeedManager.instance;
            toMainMenu = false;
            toGameConfig = false;
            isGameSetterEasingIn = false;
            isGameSetterEasingOut = false;
            LowerLocalGameSetterBasePosition = LowerLocalGameSetterGameObject.transform.position;
            UpperLocalGameSetterBasePosition = UpperLocalGameSetterGameObject.transform.position;

        }

        public void Update() {
            float time = Time.deltaTime;
            if (toMainMenu) {
                easeInMainMenu(time);
            }
            if (isGameSetterEasingOut) { 
                easeOutPlayPage(time);
            }

            if (toGameConfig) {
                easeOutMainMenu(time);
            }
            if (isGameSetterEasingIn) {
                easeInPlayPage(time);
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
            
                ConfigManager.instance.addPlayers(players);
                
                LoadingData.sceneToLoad = "NetworkGame";
                SceneManager.LoadScene("Loading");
        }
        
        public void configureProceduralGame() {
            int player1ControlsValue = localPlayer1Controls.value;
            int player2ControlsValue = localPlayer2Controls.value;

            int player2SelectValue = player2Select.value;
                
            PlayerDto[] players = new PlayerDto[2];

            players[0] = playerAssembler.assemble(player1ControlsValue, 1, 1);
            players[1] = playerAssembler.assemble(player2ControlsValue, player2SelectValue, 2);
            
            ConfigManager.instance.addPlayers(players);
            _seedManager.setSeed(seedInput.text);
                
            // TODO : Envoyer la germe!
            
            LoadingData.sceneToLoad = "ProceduralGeneration";
            SceneManager.LoadScene("Loading");
        }

        public void configureOnlineGame() {
            int player1ControlsValue = onlinePlayerControls.value;
            PlayerDto[] players = new PlayerDto[1];
            players[0] = playerAssembler.assemble(player1ControlsValue, 1, 1);
            
            Debug.Log("Player Configuration added");
            ConfigManager.instance.addPlayers(players);
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
            isGameSetterEasingOut = boolean;
            
            toGameConfig = !boolean;
            isGameSetterEasingIn = !boolean;
        }
        
        public void setToPlayMenu(bool boolean) {
            toGameConfig = boolean;
            isGameSetterEasingIn = boolean;
            
            toMainMenu = !boolean;
            isGameSetterEasingOut = !boolean;
        }

        private void easeInMainMenu(float time) {
            Color titleColor = title.color;
            
            Color buttonColor = quitButton.color;
            
            Color playColor = playButton.color;
           
            titleColor.a = interpolateFloat(titleColor.a, 1, time);
            buttonColor.a = interpolateFloat(buttonColor.a, 1, time);
            playColor.a = interpolateFloat(playColor.a, 1, time);
            title.color = titleColor;
            playButton.color = playColor;
            playButtonText.color = titleColor;
            quitButton.color = buttonColor;
            quitButtonText.color = titleColor;
            settingsButton.color = buttonColor;
            settingsButtonText.color = titleColor;
            onlineButton.color = buttonColor;
            onlineButtonText.color = titleColor;
            proceduralButton.color = buttonColor;
            proceduralButtonText.color = titleColor;
            
            if (titleColor.a + 0.001 > 1) {
                titleColor.a = 0;
                buttonColor.a = 0;
                playColor.a = 0;
                    
                title.color = titleColor;
                playButton.color = playColor;
                playButtonText.color = titleColor;
                quitButton.color = buttonColor;
                quitButtonText.color = titleColor;
                settingsButton.color = buttonColor;
                settingsButtonText.color = titleColor;
                onlineButton.color = buttonColor;
                onlineButtonText.color = titleColor;
                proceduralButton.color = buttonColor;
                proceduralButtonText.color = titleColor;
                toGameConfig = false;
            }
        }
        
        private void easeOutMainMenu(float time) {
            Color titleColor = title.color;
            
            Color buttonColor = quitButton.color;
            
            Color playColor = playButton.color;
           
            titleColor.a = interpolateFloat(titleColor.a, 0, time);
            buttonColor.a = interpolateFloat(buttonColor.a, 0, time);
            playColor.a = interpolateFloat(playColor.a, 0, time);
            title.color = titleColor;
            playButton.color = playColor;
            playButtonText.color = titleColor;
            quitButton.color = buttonColor;
            quitButtonText.color = titleColor;
            settingsButton.color = buttonColor;
            settingsButtonText.color = titleColor;
            onlineButton.color = buttonColor;
            onlineButtonText.color = titleColor;
            proceduralButton.color = buttonColor;
            proceduralButtonText.color = titleColor;
            
            if (titleColor.a - 0.001 < 0) {
                titleColor.a = 0;
                buttonColor.a = 0;
                playColor.a = 0;
                title.color = titleColor;
                playButton.color = playColor;
                playButtonText.color = titleColor;
                quitButton.color = buttonColor;
                quitButtonText.color = titleColor;
                settingsButton.color = buttonColor;
                settingsButtonText.color = titleColor;
                onlineButton.color = buttonColor;
                onlineButtonText.color = titleColor;
                proceduralButton.color = buttonColor;
                proceduralButtonText.color = titleColor;
                toGameConfig = false;
            }
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
                isGameSetterEasingIn = false;
            }
        }

        private void easeOutPlayPage(float time) {
            bool isUpperDone = false;
            bool isLowerDone = false;

            if (UpperLocalGameSetterGameObject.transform.position.y > UpperLocalGameSetterBasePosition.y - 0.1f) {
                UpperLocalGameSetterGameObject.transform.position = UpperLocalGameSetterBasePosition;
                isUpperDone = true;
            }
            else {
                UpperLocalGameSetterGameObject.transform.position =
                    interpolateToPosition(UpperLocalGameSetterGameObject.transform.position,
                        UpperLocalGameSetterBasePosition, time);
            }
            
            if (LowerLocalGameSetterGameObject.transform.position.y - 0.1f < LowerLocalGameSetterBasePosition.y) {
                LowerLocalGameSetterGameObject.transform.position = LowerLocalGameSetterBasePosition;
                isLowerDone = true;
            }
            else {
                LowerLocalGameSetterGameObject.transform.position =
                    interpolateToPosition(LowerLocalGameSetterGameObject.transform.position,
                        LowerLocalGameSetterBasePosition, time);
            }

            if (isLowerDone && isUpperDone) {
                isGameSetterEasingOut = false;
            }
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