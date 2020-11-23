using Mono.CecilX;

namespace UI {
    public class PlayerDto {
        public string controlType;
        public bool isBot;
        public int playerId;

        public PlayerDto(string controlType, bool isBot, int playerId) {
            this.controlType = controlType;
            this.isBot = isBot;
            this.playerId = playerId;
        }
    }
}