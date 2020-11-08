using UnityEngine.InputSystem;

namespace Inputs {
    public class KeyboardInputs : IInputs {

        private InputControl[] keys = new InputControl[8];
        
        // TODO : Doit contenir une liste de touches
        // Faudrait regrouper WASD sous un objet qui extend button.

        public KeyboardInputs() {
            keys[1] = Keyboard.current.spaceKey;
            
            keys[2] = Keyboard.current.wKey;
            keys[3] = Keyboard.current.aKey;
            keys[4] = Keyboard.current.sKey;
            keys[5] = Keyboard.current.dKey;

            keys[6] = Keyboard.current.leftArrowKey;
            keys[7] = Keyboard.current.rightArrowKey;
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
                           keys[code + 1].IsPressed() ||
                           keys[code + 2].IsPressed() ||
                           keys[code + 3].IsPressed();
                }
            
                // Changer direction
                if (code == 6) {
                    // TODO : Check pour voir si Left ou Right sont appuyés
                    return keys[code].IsPressed() ||
                           keys[code + 1].IsPressed();
                }
            }
            return false;
        }

        public override InputControl[] getAllControls() {
            return keys;
        }

        public override void setNewControls(InputControl[] controls) {
            keys = controls;
        }
    }
}