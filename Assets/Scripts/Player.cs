using Inputs;
using Scenes;

public class Player {
    public IInputs inputs;

    public Player() {
        inputs = new GamepadInputs();
        //inputs = new KeyboardInputs();
    }
}
