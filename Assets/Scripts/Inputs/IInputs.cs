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
            actionMap.Add("Add Stroke Strength", 2);
            actionMap.Add("Reduce Stroke Strength", 3);
            actionMap.Add("Turn Stroke Direction Left", 4);
            actionMap.Add("Turn Stroke Direction Right", 5);
        }
        
        public abstract bool isPressed(int code);

        public abstract InputControl[] getAllControls();

        public abstract void setControls(InputControl[] controls);
        public abstract Vector3 getVerticalDirection();
        public abstract Vector3 getHorizontalDirection();
    }
}