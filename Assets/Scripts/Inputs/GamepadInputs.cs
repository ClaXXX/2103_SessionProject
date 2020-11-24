﻿using Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes {
    public class GamepadInputs : IInputs {  
        
        private InputControl[] buttons = new InputControl[8];

        public GamepadInputs(int playerId) : base(playerId) {
            PlayerPrefs.SetString("Player" + playerId + "ControlType", "Gamepad");
            PlayerPrefs.Save();
            
            if (PlayerPrefs.HasKey("Player" + playerId + "GamepadStroke")) {
                buttons[1] = 
                    Gamepad.current.GetChildControl(PlayerPrefs.GetString("Player" + playerId + "GamepadStroke"));
                buttons[2] =
                    Gamepad.current.GetChildControl(PlayerPrefs.GetString("Player" + playerId + "GamepadAddPower"));
                buttons[4] = 
                    Gamepad.current.GetChildControl(PlayerPrefs.GetString("Player" + playerId + "GamepadChangeDirection"));
            } else {
                buttons[1] = Gamepad.current.buttonWest;
                buttons[2] = Gamepad.current.leftStick;
                buttons[4] = Gamepad.current.leftStick;
            }
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
            
                // Changer la puissance ou la direction
                if (code == 2 || code == 4) {
                    return buttons[code].IsPressed();
                }
            }
            return false;
        }

        public override InputControl[] getAllControls() {
            return buttons;
        }

        public override void setControls(InputControl[] controls) {
            buttons = controls;
            
            PlayerPrefs.SetString("Player" + playerId + "GamepadStroke", buttons[1].name);
            PlayerPrefs.SetString("Player" + playerId + "GamepadAddPower", buttons[2].name);
            PlayerPrefs.SetString("Player" + playerId + "GamepadChangeDirection", buttons[3].name);
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