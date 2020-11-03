using System.Collections.Generic;

namespace Inputs {
    public abstract class IInputs {

        public Dictionary<string, int> actionMap = new Dictionary<string, int>();

        public abstract bool isPressed(int code);
    }
}