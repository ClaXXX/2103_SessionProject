using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs {
    public class KeyboardInputs : IInputs {

        private InputControl[] keys = new InputControl[8];

        public KeyboardInputs(int playerId) : base(playerId) {
            keys[1] = Keyboard.current.spaceKey; // Tirer
            
            keys[2] = Keyboard.current.wKey; // Augmenter force
            keys[3] = Keyboard.current.sKey; // Réduire force
            keys[4] = Keyboard.current.aKey; // Tourner à gauche
            keys[5] = Keyboard.current.dKey; // Tourner à droite
        }
        
        public override bool isPressed(int code) {
            if (keys[code] != null) {
                // Ouvrir menu pause
                if (code == 0) {
                    return Keyboard.current.escapeKey.isPressed;
                }
                
                // Tirer
                if (code == 1) {
                    return keys[code].IsPressed();
                }
            
                // Deplacer camera
                if (code == 2) {
                    // TODO : Check pour voir si W, A, S ou D sont appuyés
                    return keys[code].IsPressed() ||
                           keys[code + 1].IsPressed();
                }
            
                // Changer direction
                if (code == 4) {
                    // TODO : Check pour voir si Left ou Right sont appuyés
                    return keys[code].IsPressed() ||
                           keys[code + 1].IsPressed();
                }
            }
            return false;
        }

        public override Vector3 getVerticalDirection() {
            if (keys[2].IsPressed()) {
                return Vector3.up;
            } else if (keys[3].IsPressed()) {
                return Vector3.down;
            }
            else {
                return Vector3.zero;
            }
        }

        public override Vector3 getHorizontalDirection() {
            if (keys[4].IsPressed()) {
                return Vector3.left;
            } else if (keys[5].IsPressed()) {
                return Vector3.right;
            }
            else {
                return Vector3.zero;
            }
        }

        public override InputControl[] getAllControls() {
            return keys;
        }

        public override void setControls(InputControl[] controls) {
            keys = controls;
        }
    }
}