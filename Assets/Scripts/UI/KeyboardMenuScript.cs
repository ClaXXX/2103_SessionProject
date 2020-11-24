using GamePlay;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class KeyboardMenuScript : MonoBehaviour {
    [SerializeField] private GameManager gameManager;
    
    private Event keyEvent;
    private Text buttonText;
    private KeyControl keyControl;
    private KeyCode newKey;
    
    private string actionName;

    private InputControl[] inputControls;
    
    private bool isWaitingForKey;

    void Start() {
        isWaitingForKey = false;
        inputControls = new InputControl[8];
    }

    private void Update() {
        if (!isWaitingForKey) {
            Player player = gameManager.getCurrentPlayer();
            inputControls = player.inputs.getAllControls();
            transform.GetChild(0).GetComponentInChildren<Text>().text = inputControls[1].name;
            transform.GetChild(1).GetComponentInChildren<Text>().text = inputControls[2].name;
            transform.GetChild(2).GetComponentInChildren<Text>().text = inputControls[3].name;
            transform.GetChild(3).GetComponentInChildren<Text>().text = inputControls[4].name;
            transform.GetChild(4).GetComponentInChildren<Text>().text = inputControls[5].name;
        }
    }


    public void onButtonClick(string action) {
        isWaitingForKey = true;
        actionName = action;
    }
    
    void OnGUI() {
        keyEvent = Event.current;
        if (keyEvent.isKey && isWaitingForKey) {
            keyControl = getPressedKey();
            if (keyControl != null) {
                assignNewKey(keyControl);
                isWaitingForKey = false;
            }
        }
    }

    private void assignNewKey(KeyControl keyControl) {
        switch (actionName) {
            case "Stroke" :
                inputControls[1] = keyControl;
                transform.GetChild(0).GetComponentInChildren<Text>().text = keyControl.name;
                gameManager.getCurrentPlayer().inputs.setControls(inputControls);
                break;
            case "Turn right" :
                inputControls[2] = keyControl;
                transform.GetChild(1).GetComponentInChildren<Text>().text = keyControl.name;
                gameManager.getCurrentPlayer().inputs.setControls(inputControls);
                break;
            case "Turn left" :
                inputControls[3] = keyControl;
                transform.GetChild(2).GetComponentInChildren<Text>().text = keyControl.name;
                gameManager.getCurrentPlayer().inputs.setControls(inputControls);
                break;
            case "Add Stroke Strength" :
                inputControls[4] = keyControl;
                transform.GetChild(3).GetComponentInChildren<Text>().text = keyControl.name;
                gameManager.getCurrentPlayer().inputs.setControls(inputControls);
                break;
            case "Reduce Stroke Strength" :
                inputControls[5] = keyControl;
                transform.GetChild(4).GetComponentInChildren<Text>().text = keyControl.name;
                gameManager.getCurrentPlayer().inputs.setControls(inputControls);
                break;
        }
    }
    
    private KeyControl getPressedKey() {
        var keyControls = Keyboard.current.allKeys;
        Debug.Log(Keyboard.current.aKey.isPressed);
        for (var i = 0; i < keyControls.Count; ++i) {
            if (keyControls[i].isPressed) {
                return keyControls[i];
            }
        }
        return null;
    }
}
