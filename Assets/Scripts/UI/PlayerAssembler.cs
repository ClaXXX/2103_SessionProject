namespace UI {
    public class PlayerAssembler {

        public PlayerDto assemble(int chosenControls, int isBot) {
            string controlType;
            bool isPlayerABot;

            if (chosenControls == 0) {
                controlType = "Keyboard";
            }
            else {
                controlType = "Gamepad";
            }

            if (isBot == 0) {
                isPlayerABot = true;
            }
            else {
                isPlayerABot = false;
            }
            
            return new PlayerDto(controlType, isPlayerABot);
        }
        
    }
}