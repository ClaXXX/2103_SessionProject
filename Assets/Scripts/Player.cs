using DefaultNamespace;
using Inputs;
using UnityEngine;

public class Player: MonoBehaviour
{
    public IInputs inputs;

    public int playerId;
    // TODO : Mettre la classe en charge du déplacement ici

    public void initializeConfigs(PlayerConfigs playerConfig) {
        inputs = playerConfig.getInputs();
        playerId = playerConfig.getPlayerId();
    }
    
    
}
