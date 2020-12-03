using Inputs;

namespace DefaultNamespace {
    public class PlayerConfigs {
        private IInputs inputs;
        private int playerNumber;

        public PlayerConfigs(IInputs inputs, int playerNumber) {
            this.inputs = inputs;
            this.playerNumber = playerNumber;
        }

        public IInputs getInputs() {
            return inputs;
        }

        public int getPlayerId() {
            return playerNumber;
        }
    }
}