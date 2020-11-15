
using DefaultNamespace;
using UI;

public class TurnManager {
    private static TurnManager instance;

    private Player[] players;

    private int playerIterator = 0;
    private Player activePlayer;

    public void nextTurn() {
        if (playerIterator == players.Length) {
            playerIterator = 0;
        }
        activePlayer = players[playerIterator];
    }

    public Player getActivePlayer() {
        return activePlayer;
    }

    public void setPlayers(Player[] sentPlayers) {
        players = sentPlayers;
        activePlayer = players[0];
    }
}
