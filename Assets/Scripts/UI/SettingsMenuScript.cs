using GamePlay;
using Inputs;
using UnityEngine;

namespace UI {
    public class SettingsMenuScript : MonoBehaviour {
        [SerializeField] private GameObject PauseMenu;
        [SerializeField] private GameObject KeyboardMenu;
        [SerializeField] private GameObject GamepadMenu;
        [SerializeField] private GameManager gameManager;


        public void openSettings() {
            if (gameManager.getCurrentPlayer().inputs is KeyboardInputs) {
                PauseMenu.SetActive(false);
                KeyboardMenu.SetActive(true);
            } else {
                PauseMenu.SetActive(false);
                GamepadMenu.SetActive(true);
            }
        }
    }
}