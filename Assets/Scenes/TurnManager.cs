using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class TurnManager {
    
    // TODO : Serait pas pire que se soit un Singleton

    private static TurnManager instance;
    private static object m_Lock = new object();
    
    private Player[] players;

    private int playerIterator = 0;
    private Player activePlayer;

    private TurnManager(Player[] players) {
        this.players = players;
    }

    public static TurnManager getInstance() {
        lock (m_Lock) {
            if (instance == null) {
                // Search for existing instance.
                //instance = (T)FindObjectOfType(typeof(T));
 
                // Create new instance if one doesn't already exist.
                if (instance == null)
                {
                    // Need to create a new GameObject to attach the singleton to.
                    var singletonObject = new GameObject();
                    //instance = singletonObject.AddComponent<T>();
                    //singletonObject.name = typeof(T).ToString() + " (Singleton)";
 
                    // Make instance persistent.
                    //DontDestroyOnLoad(singletonObject);
                }
            }
 
            return instance;
        }
        if (instance == null) {
            
        }
    }
    

    public void nextTurn() {
        if (playerIterator == players.Length) {
            playerIterator = 0;
        }
        // TODO : Desactiver le joueur
        activePlayer = players[playerIterator];
    }

    public Player getActivePlayer() {
        return activePlayer;
    }
}
