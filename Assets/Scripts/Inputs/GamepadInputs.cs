using System.Transactions;
using Inputs;
using UnityEngine.InputSystem;

namespace Scenes {
    public class GamepadInputs : IInputs {  
        
        private InputControl[] buttons = new InputControl[8];

        public GamepadInputs() : base(){
            buttons[1] = Gamepad.current.xButton;
            
            buttons[2] = Gamepad.current.rightStick;

            buttons[5] = Gamepad.current.xButton;

            buttons[6] = Gamepad.current.leftStick;
        }
        
        public override bool isPressed(int code) {
            if (buttons[code] != null) {
                // Ouvrir menu pause
                if (code == 0) {
                    return Keyboard.current.escapeKey.isPressed;
                }
                
                // Tirer
                if (code == 1) {
                    return buttons[code].IsPressed();
                }
            
                // Deplacer camera
                if (code == 2) {
                    // TODO : Check pour voir si W, A, S ou D sont appuyés
                    return buttons[code].IsPressed();
                }
            
                // Changer direction
                if (code == 6) {
                    // TODO : Check pour voir si Left ou Right sont appuyés
                    return buttons[code].IsPressed();
                }
            }
            return false;
        }

        public override InputControl[] getAllControls() {
            return buttons;
        }

        public override void setNewControls(InputControl[] controls) {
            buttons = controls;
        }
    }
}