using Inputs;
using Scenes;

public class Player {
    public IInputs inputs;

    public Player(IInputs chosenInputs) {
        inputs = chosenInputs;
    }
}
