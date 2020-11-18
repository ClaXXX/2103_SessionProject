using System.Collections.Generic;
using UI;
using UnityEngine;

namespace DefaultNamespace {
    public class ConfigManager : MonoBehaviour {

        public static ConfigManager instance;

        private InputsFactory inputsFactory;
        
        private List<PlayerConfigs> playerConfigs = new List<PlayerConfigs>();
        

        void Awake() {
            if (instance == null) {
                instance = this;
                inputsFactory = new InputsFactory();
                DontDestroyOnLoad(instance);
            }
        }

        public void addPlayers(PlayerDto[] sentPlayerDtos) {
            for (int i = 0; i < sentPlayerDtos.Length; i++) {
                playerConfigs.Add(new PlayerConfigs(inputsFactory.createInputs(sentPlayerDtos[i])));
            }
        }

        public List<PlayerConfigs> getPlayerConfigs() {
            return playerConfigs;
        }
    }
}