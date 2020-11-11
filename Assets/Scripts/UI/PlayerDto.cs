namespace UI {
    public class PlayerDto {
        public string controlType;
        public bool isBot;

        public PlayerDto(string controlType, bool isBot) {
            this.controlType = controlType;
            this.isBot = isBot;
        }
    }
}