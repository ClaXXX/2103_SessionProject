using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class KeyboardMenuScript : MonoBehaviour {
    private Transform menuPanel;
    private Event keyEvent;
    private Text buttonText;
    private KeyControl keyControl;
    private KeyCode newKey;
    
    private string actionName;

    private InputControl[] inputControls;
    
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

        inputControls = new InputControl[8];
        inputControls = player.inputs.getAllControls();

        menuPanel.GetChild(0).GetComponentInChildren<Text>().text = inputControls[1].name;
        menuPanel.GetChild(1).GetComponentInChildren<Text>().text = inputControls[2].name;
        menuPanel.GetChild(2).GetComponentInChildren<Text>().text = inputControls[3].name;
        menuPanel.GetChild(3).GetComponentInChildren<Text>().text = inputControls[4].name;
        menuPanel.GetChild(4).GetComponentInChildren<Text>().text = "TEST SWITCH PAGE";
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
                SceneManager.LoadScene("Game");
            }
        }
    }

    private void assignNewKey(KeyControl keyControl) {
        Debug.Log(keyControl.name);

        switch (actionName) {
            case "Tirer" :
                inputControls[1] = keyControl;
                menuPanel.GetChild(0).GetComponentInChildren<Text>().text = keyControl.name;
                turnManager.getActivePlayer().inputs.setNewControls(inputControls);
                break;
            case "Tourner à droite" :
                inputControls[2] = keyControl;
                menuPanel.GetChild(1).GetComponentInChildren<Text>().text = keyControl.name;
                turnManager.getActivePlayer().inputs.setNewControls(inputControls);
                break;
            case "Tourner à gauche" :
                inputControls[3] = keyControl;
                menuPanel.GetChild(2).GetComponentInChildren<Text>().text = keyControl.name;
                turnManager.getActivePlayer().inputs.setNewControls(inputControls);
                break;
            case "Augmenter la puissance" :
                inputControls[4] = keyControl;
                menuPanel.GetChild(3).GetComponentInChildren<Text>().text = keyControl.name;
                turnManager.getActivePlayer().inputs.setNewControls(inputControls);
                break;
            case "Réduire la puissance" :
                inputControls[5] = keyControl;
                menuPanel.GetChild(4).GetComponentInChildren<Text>().text = keyControl.name;
                turnManager.getActivePlayer().inputs.setNewControls(inputControls);
                break;
        }
        
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
