using System;
using Inputs;
using Scenes;
using UnityEngine;

public class InputService : MonoBehaviour {

    private Player player = new Player();

    private TurnManager turnManager;

    private void Start() {
        Player[] players = new Player[1];
        players[0] = player;
        turnManager = TurnManager.getInstance();
        
        // TODO : Chaque joueur contient un IInput. Et chaque instance de InputService contient un joueur
        player.inputs = new KeyboardInputs();
        
        player.inputs.actionMap.Add("Tirer", 1);
        player.inputs.actionMap.Add("BougerCamera", 2);
        player.inputs.actionMap.Add("ChangerDirection", 6);
        Debug.Log("action map initialized");
    }

    void Update() {
        if (player.inputs.isPressed(player.inputs.actionMap["Tirer"])) {
            shoot();
        }
        
        if (player.inputs.isPressed(player.inputs.actionMap["BougerCamera"])) {
            changeCameraPosition();
        }
        
        if (player.inputs.isPressed(player.inputs.actionMap["ChangerDirection"])) {
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
        return player.inputs;
    }

    public void changeInputs(IInputs inputs) {
        player.inputs = inputs;
    }
}
