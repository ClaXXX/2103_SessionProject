using System;
using Mirror;
using TMPro;
using UnityEngine;

namespace GamePlay
{
    public class PlayerIdentity : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _name;
        private const string PlayerNameKey = "PlayerName";
        public static string PlayerName { get; protected set; } = "";

        private void Start()
        {
            if (!PlayerPrefs.HasKey(PlayerNameKey))
            {
                return;
            }
            PlayerName = PlayerPrefs.GetString(PlayerNameKey);
            _name.text = PlayerName;
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