using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Debug = UnityEngine.Debug;

public class KeyboardMenuScript : MonoBehaviour {
    private Transform menuPanel;
    private Event keyEvent;
    private Text buttonText;
    private KeyControl keyControl;
    private KeyCode newKey;

    private TurnManager turnManager;
    private bool isWaitingForKey;

    // TODO : Ajouter une instance du TurnManager


    void Start() {
        menuPanel = transform.Find("Keyboard Controls Panel");
        //menuPanel.gameObject.SetActive(false);
        menuPanel.gameObject.SetActive(true); // TODO : Seulement pour tester
        isWaitingForKey = false;

        turnManager = TurnManager.getInstance();

        Player player = turnManager.getActivePlayer();

        InputControl[] inputControl = new InputControl[8];
        inputControl = player.inputs.getAllControls();

        menuPanel.GetChild(0).GetComponentInChildren<Text>().text = inputControl[1].name;
        menuPanel.GetChild(1).GetComponentInChildren<Text>().text = inputControl[2].name;
        menuPanel.GetChild(2).GetComponentInChildren<Text>().text = inputControl[3].name;
        menuPanel.GetChild(3).GetComponentInChildren<Text>().text = inputControl[4].name;
        menuPanel.GetChild(4).GetComponentInChildren<Text>().text = "TEST SWITCH PAGE";
    }

    public void onButtonClick(string buttonString) {
        isWaitingForKey = true;
        // TODO : Set une variable (string) ici (utiliser par une autre fonction)
    }
    
    void OnGUI() {
        keyEvent = Event.current;

        if (keyEvent.isKey && isWaitingForKey) {
            keyControl = getPressedKey();
            if (keyControl != null) {
                Debug.Log(keyControl.name);
                // TODO : Call la mth pour changer le contrôle ici
                isWaitingForKey = false;
            }
        }
    }

    private void assignNewKey(KeyControl keyControl) {
        // TODO : On utilise le array de inputs et on le renvoit au Player. cela devient maintenant son nouveau array.
        // TODO : Par contre, comment dire au InputManager que le player n'est plus le bon...?
        // TODO : En fait, est-ce que c'est toujours le même player...
    }
    
    private KeyControl getPressedKey() {
        var keyControls = Keyboard.current.allKeys;
        for (var i = 0; i < keyControls.Count; ++i) {
            if (keyControls[i].isPressed) {
                return keyControls[i];
            }
        }
        return null;
    }
}
