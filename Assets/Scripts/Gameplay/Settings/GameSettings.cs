using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public enum PlayerMode
    {
        Online,
        Local
    }
    
    public enum BotDifficulties
    {
        Normal,
        Hard
    }

    public static class GameSettings
    {
        public static PlayerMode PlayerMode = PlayerMode.Local; // default to local game
        public static int BotNumber = 0;
        public static BotDifficulties Diffulty = BotDifficulties.Normal; // default: Normal
    }
}