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

    public static class GameSettings
    {
        public static PlayerMode PlayerMode = PlayerMode.Local; // default to local game
    }
}