using System.Collections.Generic;
using DefaultNamespace;
using Inputs;
using UnityEngine;

public class InputService : MonoBehaviour {
    private TurnManager turnManager;

    private void Start() {
        turnManager = new TurnManager();
    }

    void Update() {
        if (turnManager.getActivePlayer().inputs.isPressed(turnManager.getActivePlayer().inputs.actionMap["Tirer"])) {
            shoot();
        }
        
        if (turnManager.getActivePlayer().inputs.isPressed(turnManager.getActivePlayer().inputs.actionMap["BougerCamera"])) {
            changeCameraPosition();
        }
        
        if (turnManager.getActivePlayer().inputs.isPressed(turnManager.getActivePlayer().inputs.actionMap["ChangerDirection"])) {
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
        return turnManager.getActivePlayer().inputs;
    }

    public void changeInputs(IInputs inputs) {
        turnManager.getActivePlayer().inputs = inputs;
    }

    public void setPlayers(Player[] sentPlayers) {
        turnManager.setPlayers(sentPlayers);
    }
}
