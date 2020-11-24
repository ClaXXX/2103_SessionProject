using Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes {
    public class GamepadInputs : IInputs {  
        
        private InputControl[] buttons = new InputControl[8];

        public GamepadInputs(int playerId) : base(playerId) {
            PlayerPrefs.SetString("Player" + playerId + "ControlType", "Gamepad");
            PlayerPrefs.Save();
            
            if (!PlayerPrefs.HasKey("Player" + playerId + "GamepadStroke")) {
                buttons[1] = 
                    Gamepad.current.GetChildControl(PlayerPrefs.GetString("Player" + playerId + "GamepadStroke"));
                Debug.Log(PlayerPrefs.GetString("Player" + playerId + "GamepadAddPower"));
                buttons[2] =
                    Gamepad.current.GetChildControl(PlayerPrefs.GetString("Player" + playerId + "GamepadAddPower"));
                buttons[3] =
                    Gamepad.current.GetChildControl(PlayerPrefs.GetString("Player" + playerId + "GamepadReducePower"));
                buttons[4] = 
                    Gamepad.current.GetChildControl(PlayerPrefs.GetString("Player" + playerId + "GamepadChangeDirectionLeft"));
                buttons[5] = 
                    Gamepad.current.GetChildControl(PlayerPrefs.GetString("Player" + playerId + "GamepadChangeDirectionRight"));
            } else {
                buttons[1] = Gamepad.current.buttonWest; //Tirer
                buttons[2] = Gamepad.current.leftStick.up; // Augmenter puissance
                buttons[3] = Gamepad.current.leftStick.down; // Reduire puissance
                buttons[4] = Gamepad.current.leftStick.left; // Tourner à gauche
                buttons[5] = Gamepad.current.leftStick.right; // Tourner à droite
                setControls(buttons);
            }
        }
        
        public override bool isPressed(int code) {
            if (buttons[code] != null) {
                // Ouvrir menu pause
                if (code == 0) {
                    // TODO : À IMPLÉMENTER
                    return Keyboard.current.escapeKey.isPressed;
                }
                return buttons[code].IsPressed();
            }
            return false;
        }

        public override InputControl[] getAllControls() {
            return buttons;
        }

        public override void setControls(InputControl[] controls) {
            buttons = controls;
            string name;
            
            name = buttons[1].parent.name;
            if (name != "leftStick" && name != "rightStick") {
                PlayerPrefs.SetString("Player" + playerId + "GamepadStroke", buttons[1].name);   
            } else {
                PlayerPrefs.SetString("Player" + playerId + "GamepadStroke", buttons[1].parent.name + "/" + buttons[1].name);
            }

            name = buttons[2].parent.name;
            if (name != "leftStick" && name != "rightStick") {
                PlayerPrefs.SetString("Player" + playerId + "GamepadAddPower", buttons[2].name);
            } else {
                PlayerPrefs.SetString("Player" + playerId + "GamepadAddPower", buttons[2].parent.name + "/" + buttons[2].name);
            }

            name = buttons[3].parent.name;
            if (name != "leftStick" && name != "rightStick") {
                PlayerPrefs.SetString("Player" + playerId + "GamepadReducePower", buttons[2].name);
            } else {
                PlayerPrefs.SetString("Player" + playerId + "GamepadReducePower", buttons[3].parent.name + "/" + buttons[3].name);
            }
            
            name = buttons[4].parent.name;
            if (name != "leftStick" && name != "rightStick") {
                PlayerPrefs.SetString("Player" + playerId + "GamepadChangeDirectionLeft", buttons[2].name);
            } else {
                PlayerPrefs.SetString("Player" + playerId + "GamepadChangeDirectionLeft", buttons[4].parent.name + "/" + buttons[4].name);
            }
            
            name = buttons[4].parent.name;
            if (name != "leftStick" && name != "rightStick") {
                PlayerPrefs.SetString("Player" + playerId + "GamepadChangeDirectionRight", buttons[2].name);
            }
            else {
                PlayerPrefs.SetString("Player" + playerId + "GamepadChangeDirectionRight", buttons[5].parent.name + "/" + buttons[5].name);
            }
            PlayerPrefs.Save();
        }

        public override Vector3 getVerticalDirection() {
            if (Gamepad.current.leftStick.up.isPressed) {
            return Vector3.up;
            }

            if (Gamepad.current.leftStick.down.isPressed) {
                return Vector3.down;
            }
            return Vector3.zero;
        }

        public override Vector3 getHorizontalDirection() {
            if (Gamepad.current.leftStick.right.isPressed) {
                return Vector3.right;
            }
            if (Gamepad.current.leftStick.left.isPressed) {
                return Vector3.left;
            }
            return Vector3.zero;
        }
    }
}