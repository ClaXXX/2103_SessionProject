using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class TurnManager {
    
    // TODO : Serait pas pire que se soit un Singleton

    private static TurnManager instance;

    private Player[] players;

    private int playerIterator = 0;
    private Player activePlayer;

    private TurnManager() {
        players = new Player[2];
        players[0] = new Player(); // TODO : Ligne pour tester
        activePlayer = players[0];
    }

    public static TurnManager getInstance() {
        if (instance == null) {
            instance = new TurnManager();
        }
        return instance;
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

    public void setPlayers(Player[] sadfg) {
        this.players = sadfg;
    }
}
