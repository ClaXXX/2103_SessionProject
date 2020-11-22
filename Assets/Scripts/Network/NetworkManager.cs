using System;
using System.Collections;
using System.Collections.Generic;
using GamePlay;
using Mirror;
using UI.Network;
using UnityEngine;

namespace Network
{
    public class NetworkManager : Mirror.NetworkManager
    {
        #region HUD

        public void setAdress(String addr)
        {
            networkAddress = addr;
        }

        #endregion

        #region Network Events

        public override void OnStartHost()
        {
            base.OnStartHost();
            GameSettings.PlayerMode = PlayerMode.Online;
        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            GameSettings.PlayerMode = PlayerMode.Online;
        }

        #endregion
    }
}
