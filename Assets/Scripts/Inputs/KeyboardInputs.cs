using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs {
    public class KeyboardInputs : IInputs {

        private InputControl[] keys = new InputControl[8];

        public KeyboardInputs(int playerId) : base(playerId) {
            PlayerPrefs.SetString("Player" + playerId + "ControlType", "Keyboard");
            PlayerPrefs.Save();

            if (PlayerPrefs.HasKey("Player" + playerId + "KeyboardStroke")) {
                keys[1] = 
                    Keyboard.current.GetChildControl(PlayerPrefs.GetString("Player" + playerId + "KeyboardStroke"));
                keys[2] = 
                    Keyboard.current.GetChildControl(PlayerPrefs.GetString("Player" + playerId + "KeyboardAddPower"));
                keys[3] = 
                    Keyboard.current.GetChildControl(PlayerPrefs.GetString("Player" + playerId + "KeyboardReducePower"));
                keys[4] = 
                    Keyboard.current.GetChildControl(PlayerPrefs.GetString("Player" + playerId + "KeyboardTurnLeft"));
                keys[5] = 
                    Keyboard.current.GetChildControl(PlayerPrefs.GetString("Player" + playerId + "KeyboardTurnRight"));
            } else {
                keys[1] = Keyboard.current.spaceKey; // Tirer
                keys[2] = Keyboard.current.wKey; // Augmenter force
                keys[3] = Keyboard.current.sKey; // Réduire force
                keys[4] = Keyboard.current.aKey; // Tourner à gauche
                keys[5] = Keyboard.current.dKey; // Tourner à droite
            }
        }
        
        public override bool isPressed(int code) {
            if (keys[code] != null) {
                // Ouvrir menu pause
                if (code == 0) {
                    return Keyboard.current.escapeKey.isPressed;
                }

                return keys[code].IsPressed();
            }
            return false;
        }

        public override InputControl[] getAllControls() {
            return keys;
        }

        public override void setControls(InputControl[] controls) {
            keys = controls;
            
            PlayerPrefs.SetString("Player" + playerId + "KeyboardStroke", keys[1].name);
            PlayerPrefs.SetString("Player" + playerId + "KeyboardAddPower", keys[2].name);
            PlayerPrefs.SetString("Player" + playerId + "KeyboardReducePower", keys[3].name);
            PlayerPrefs.SetString("Player" + playerId + "KeyboardTurnLeft", keys[4].name);
            PlayerPrefs.SetString("Player" + playerId + "KeyboardTurnRight", keys[5].name);
            PlayerPrefs.Save();
        }
    }
}