using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Inputs {
    public abstract class IInputs {

        public Dictionary<string, int> actionMap = new Dictionary<string, int>();

        public IInputs() {
            actionMap.Add("Tirer", 1);
            actionMap.Add("BougerCamera", 2);
            actionMap.Add("ChangerDirection", 6);
        }
        
        public abstract bool isPressed(int code);

        public abstract InputControl[] getAllControls();

        public abstract void setNewControls(InputControl[] controls);
    }
}