using Inputs;

namespace DefaultNamespace {
    public class PlayerConfigs {
        private IInputs inputs;

        public PlayerConfigs(IInputs inputs) {
            this.inputs = inputs;
        }

        public IInputs getInputs() {
            return inputs;
        }
    }
}