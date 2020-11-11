using UI;
using UnityEngine;

namespace DefaultNamespace {
    public class GameInitializer {
        
        private TurnManager turnManager;
        private PlayerFactory playerFactory;


        public GameInitializer() {
            playerFactory = new PlayerFactory();
            turnManager = TurnManager.getInstance();
            
            //Rigidbody rb = GameObject.Find("Game Manager").GetComponent<Rigidbody>();
            
            //Debug.Log(rb.name);
        }
        
        public void addPlayers(PlayerDto[] playerDtos) {
            // TODO : Dans le futur, ça va être la liste des joueurs et non les dtos
            
            turnManager.setPlayers(playerDtos);
        }
    }
}