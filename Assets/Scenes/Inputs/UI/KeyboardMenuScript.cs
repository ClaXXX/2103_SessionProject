using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class KeyboardMenuScript : MonoBehaviour {
    private Transform menuPanel;
    private Event keyEvent;
    private Text buttonText;
    private KeyControl keyControl;

    private bool isWaitingForKey;
    
    
    void Start() {
        menuPanel = transform.Find("Keyboard Controls Panel");
        //menuPanel.gameObject.SetActive(false);
        menuPanel.gameObject.SetActive(true);  // TODO : Seulement pour tester
        isWaitingForKey = false;

        // TODO : Pour chaque bouton, prendre les contrôles du joueur courant et les afficher
        // Exemple, mainloop.currentPlayer.InputService.getInputs(). 

        menuPanel.GetChild(0).GetComponentInChildren<Text>().text = "";
        menuPanel.GetChild(1).GetComponentInChildren<Text>().text = "";
        menuPanel.GetChild(2).GetComponentInChildren<Text>().text = "";
        menuPanel.GetChild(3).GetComponentInChildren<Text>().text = "";
        menuPanel.GetChild(4).GetComponentInChildren<Text>().text = "";
    }

    // NOTE : Panel parent (lui qui contient toutes les options) qui décident quand l'afficher
    void Update() {
        // TODO : Lorsque joueur courant clique sur un des boutons, on save le bouton clavier
        // On modifie ensuite directement l'objet inputs, puis on le renvoi au InputService du joueur
        // On update ensuite le texte du bouton, pour afficher la différence
    }
}
