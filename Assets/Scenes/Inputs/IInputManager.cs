using System;
using Inputs;
using Scenes;
using UnityEngine;

public class InputManager : MonoBehaviour{

    private IInputs inputs; // TODO : Soit Gamepad ou Keyboard

    private void Start() {
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
    }

    public void changeDirection() {
        // TODO : Appel méthode du domaine pour changer la direction de la rondelle du joueur courant
    }

    public void shoot() {
        // TODO : Appel méthode du domaine pour tirer la rondelle dans la direction choisie
    }
}
