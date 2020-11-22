using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs {
    public abstract class IInputs {

        public Dictionary<string, int> actionMap = new Dictionary<string, int>();

        public IInputs() {
            actionMap.Add("Stroke", 1);
            actionMap.Add("Modify Stroke Strength", 2);
            actionMap.Add("Change Stroke Direction", 4);
        }
        
        public abstract bool isPressed(int code);

        public abstract InputControl[] getAllControls();

        public abstract void setNewControls(InputControl[] controls);
        public abstract Vector3 getVerticalDirection();
        public abstract Vector3 getHorizontalDirection();
    }
}