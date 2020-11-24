using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs {
    public abstract class IInputs {

        public int playerId;
        
        public Dictionary<string, int> actionMap = new Dictionary<string, int>();

        public IInputs(int playerId) {
            this.playerId = playerId;
            
            // TODO : Doit devenir plus exhaustif
            actionMap.Add("Stroke", 1);
            actionMap.Add("Modify Stroke Strength", 2);
            actionMap.Add("Change Stroke Direction", 4);
        }
        
        public abstract bool isPressed(int code);

        public abstract InputControl[] getAllControls();

        public abstract void setControls(InputControl[] controls);
        public abstract Vector3 getVerticalDirection();
        public abstract Vector3 getHorizontalDirection();
    }
}