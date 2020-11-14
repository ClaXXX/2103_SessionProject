using Inputs;
using Scenes;

public class Player {
    public IInputs inputs;

    public Player(IInputs chosenInputs) {
        inputs = chosenInputs;
    }

    public void initializeConfigs(object playerConfig) {
        throw new System.NotImplementedException();
    }
}
