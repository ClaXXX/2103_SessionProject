using System;
using Inputs;
using Scenes;
using UnityEngine;

public class InputService : MonoBehaviour {

    private IInputs inputs; // TODO : Soit Gamepad ou Keyboard

    private void Start() {
        inputs = new KeyboardInputs(); // TODO : Devrait prob être injecte
        
        inputs.actionMap.Add("Tirer", 1);
        inputs.actionMap.Add("BougerCamera", 2);
        inputs.actionMap.Add("ChangerDirection", 6);
        Debug.Log("action map initialized");
    }

    void Update() {
        if (inputs.isPressed(inputs.actionMap["Tirer"])) {
            shoot();
        }
        
        if (inputs.isPressed(inputs.actionMap["BougerCamera"])) {
            changeCameraPosition();
        }
        
        if (inputs.isPressed(inputs.actionMap["ChangerDirection"])) {
            changeDirection();
        }
    }
    
    public void changeCameraPosition() {
        // TODO : Appel méthode du domaine pour changer la position de la cam du joueur
        Debug.Log("Cam go weeeeee");
    }

    public void changeDirection() {
        // TODO : Appel méthode du domaine pour changer la direction de la rondelle du joueur courant
        Debug.Log("Direction spiiinnnnnn");
    }

    public void shoot() {
        // TODO : Appel méthode du domaine pour tirer la rondelle dans la direction choisie
        Debug.Log("pew pew");
    }

    public void openMenu() {
        // TODO : Ouvrir l'écran pause ici
        Debug.Log("Pause!");
    }
    
    public IInputs getInputs() {
        return inputs;
    }

    public void changeInputs(IInputs inputs) {
        this.inputs = inputs;
    }
}
