using Inputs;
using Scenes;
using UI;

namespace DefaultNamespace {
    public class InputsFactory {


        public IInputs createInputs(PlayerDto playerDto) {
            if (playerDto.controlType == "Keyboard") {
                return new KeyboardInputs();
            }
            else { 
                return new GamepadInputs();
            }
        }
    }
}