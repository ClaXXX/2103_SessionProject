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
        private ServerManager _server = null;

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
        
        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);

            if (mode == NetworkManagerMode.Host) 
            {            
                Debug.Log(FindObjectOfType<NetworkMenus>());
                FindObjectOfType<NetworkMenus>().SetMenuActive(true, "Host");
            }
            else
            {
                FindObjectOfType<NetworkMenus>().SetMenuActive(true, "Client");
            }
        }

        public override void OnServerConnect(NetworkConnection conn)
        {
            base.OnServerConnect(conn);
            if (!_server)
            {
                _server = FindObjectOfType<ServerManager>();
            }
            _server.AddClient(conn);
        }
        
        public override void OnServerDisconnect(NetworkConnection conn)
        {
            base.OnServerDisconnect(conn);
            _server.End();
        }
        
        #endregion

        #region GamePlay Method
        
        #endregion
    }
}
