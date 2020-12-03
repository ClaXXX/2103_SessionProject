using System;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay
{
    public class PlayerIdentity : MonoBehaviour
    {
        public TMP_InputField inputName;
        private const string PlayerNameKey = "PlayerName";
        public static string PlayerName { get; protected set; } = "";

        private void Start()
        {
            if (!PlayerPrefs.HasKey(PlayerNameKey))
            {
                return;
            }
            PlayerName = PlayerPrefs.GetString(PlayerNameKey);
            inputName.text = PlayerName;
        }
        
        public void SavePlayerName()
        {
            if (!string.IsNullOrEmpty(PlayerName))
            {
                PlayerPrefs.SetString(PlayerNameKey, PlayerName);
            }
        }

        public void SetPlayerName(string playerName)
        {
            PlayerName = playerName;
        }
    }
}