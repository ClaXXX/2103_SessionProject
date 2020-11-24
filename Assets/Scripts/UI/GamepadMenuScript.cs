using GamePlay;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadMenuScript : MonoBehaviour {

    [SerializeField] private GameManager gameManager;
    
    private Event keyEvent;
    private Text buttonText;
    
    private InputControl inputControl;
    private KeyCode newKey;
    
    private string actionName;

    private InputControl[] inputControls;

    private bool isWaitingForInput;


    void Start() {
        isWaitingForInput = false;
        inputControls = new InputControl[8];
    }

    private void Update() {
        if (!isWaitingForInput) {
            Player player = gameManager.getCurrentPlayer();
            inputControls = player.inputs.getAllControls();
            transform.GetChild(0).GetComponentInChildren<Text>().text = inputControls[1].name;
            transform.GetChild(1).GetComponentInChildren<Text>().text = inputControls[2].name;
            transform.GetChild(2).GetComponentInChildren<Text>().text = inputControls[4].name;
        }
    }

    public void onButtonClick(string action) {
        isWaitingForInput = true;
        actionName = action;
    }
    
    void OnGUI() {
        if (isWaitingForInput) {
            inputControl = getPressedKey();
            if (inputControl != null) {
                assignNewKey(inputControl);
                isWaitingForInput = false;
            }
        }
    }

    private void assignNewKey(InputControl control) {
        switch (actionName) {
            case "Stroke" :
                Debug.Log("stroke");
                inputControls[1] = control;
                transform.GetChild(0).GetComponentInChildren<Text>().text = control.name;
                gameManager.getCurrentPlayer().inputs.setControls(inputControls);
                break;
            case "Modify Stroke Strength" :
                inputControls[2] = control;
                transform.GetChild(1).GetComponentInChildren<Text>().text = control.name;
                gameManager.getCurrentPlayer().inputs.setControls(inputControls);
                break;
            case "Change Stroke Direction" :
                inputControls[4] = control;
                transform.GetChild(2).GetComponentInChildren<Text>().text = control.name;
                gameManager.getCurrentPlayer().inputs.setControls(inputControls);
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
