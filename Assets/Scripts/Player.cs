﻿using DefaultNamespace;
using Inputs;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public IInputs inputs;
    // TODO : Mettre la classe en charge du déplacement ici

    public void initializeConfigs(PlayerConfigs playerConfig) {
        inputs = playerConfig.getInputs();
    }
    
    
}
