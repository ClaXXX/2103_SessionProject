using Inputs;
using Scenes;
using UI;

namespace DefaultNamespace {
    public class PlayerFactory {


        public Player createPlayer(PlayerDto playerDto) {
            IInputs chosenInputs;
            if (playerDto.controlType == "Keyboard") {
                chosenInputs = new KeyboardInputs(); // TODO : Mettre ça dans une input factory :/
            }
            else {
                chosenInputs = new GamepadInputs();
            }
            if (playerDto.isBot) {
                // TODO : Pour tester, on a pas encore de notion de AI
                return new Player(chosenInputs);
            }
            return new Player(chosenInputs);
        }
    }
}