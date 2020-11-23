using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class GamepadMenuScript : MonoBehaviour {
    private Transform menuPanel;
    private Event keyEvent;
    private Text buttonText;

    // TODO : À CHANGER
    private InputControl inputControl;
    private KeyCode newKey;
    
    private string actionName;

    private InputControl[] inputControls;
    
    private TurnManager turnManager;
    
    private bool isWaitingForInput;


    void Start() {
        menuPanel = transform.Find("Gamepad Controls Panel");
        //menuPanel.gameObject.SetActive(false);
        menuPanel.gameObject.SetActive(true); // TODO : Seulement pour tester
        isWaitingForInput = false;

        //turnManager = TurnManager.getInstance();

        Player player = turnManager.getActivePlayer();

        inputControls = new InputControl[8];
        inputControls = player.inputs.getAllControls();

        menuPanel.GetChild(0).GetComponentInChildren<Text>().text = inputControls[1].name;
        menuPanel.GetChild(1).GetComponentInChildren<Text>().text = inputControls[2].name;
        menuPanel.GetChild(2).GetComponentInChildren<Text>().text = inputControls[5].name;
        menuPanel.GetChild(3).GetComponentInChildren<Text>().text = inputControls[6].name;
        menuPanel.GetChild(4).GetComponentInChildren<Text>().text = "TEST SWITCH PAGE";
    }

    public void onButtonClick(string action) {
        isWaitingForInput = true;
        actionName = action;
    }
    
    void OnGUI() {
        if (Gamepad.current.IsPressed() && isWaitingForInput) {
            inputControl = getPressedKey();
            if (inputControl != null) {
                assignNewKey(inputControl);
                isWaitingForInput = false;
                SceneManager.LoadScene("Game");
            }
        }
    }

    private void assignNewKey(InputControl control) {
        switch (actionName) {
            case "Tirer" :
                inputControls[1] = control;
                menuPanel.GetChild(0).GetComponentInChildren<Text>().text = control.name;
                turnManager.getActivePlayer().inputs.setControls(inputControls);
                break;
            case "Tourner à droite" :
                inputControls[2] = control;
                menuPanel.GetChild(1).GetComponentInChildren<Text>().text = control.name;
                turnManager.getActivePlayer().inputs.setControls(inputControls);
                break;
            case "Tourner à gauche" :
                inputControls[5] = control;
                menuPanel.GetChild(2).GetComponentInChildren<Text>().text = control.name;
                turnManager.getActivePlayer().inputs.setControls(inputControls);
                break;
            case "Augmenter la puissance" :
                inputControls[6] = control;
                menuPanel.GetChild(3).GetComponentInChildren<Text>().text = control.name;
                turnManager.getActivePlayer().inputs.setControls(inputControls);
                break;
            case "Réduire la puissance" :
                inputControls[6] = control;
                menuPanel.GetChild(4).GetComponentInChildren<Text>().text = control.name;
                turnManager.getActivePlayer().inputs.setControls(inputControls);
                break;
        }
    }
    
    private InputControl getPressedKey() {
        var keyControls = Gamepad.current.allControls;
        for (var i = 0; i < keyControls.Count; ++i) {
            if (keyControls[i].IsPressed()) {
                return keyControls[i];
            }
        }
        return null;
    }
}
