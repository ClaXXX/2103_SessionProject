using System;
using System.Collections;
using System.Collections.Generic;
using GamePlay;
using Mirror;
using UnityEngine;

namespace Network
{
    public class NetworkManager : NetworkRoomManager
    {
        private List<NetworkConnection> _connections = new List<NetworkConnection>();
        private GameManager _gameManager;

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
                // Instantiate Host Menu
            }
            else
            {
                // Instantiate Client Menu
            }
        }

        public override void OnRoomServerSceneChanged(string sceneName)
        {
            base.OnRoomServerSceneChanged(sceneName);
            if (sceneName != GameplayScene)
            {
                return;
            }
            _gameManager = FindObjectOfType<GameManager>();
            if (!_gameManager)
            {
                Debug.LogError("GameManager not found");
                return;
            }
            _gameManager.playerPrefabs = playerPrefab; // Get the network prefabs
            _gameManager.OnPlayerCreated += OnPlayerCreated;
            _gameManager.LaunchGame(_connections.Count);

        }

        public override void OnServerConnect(NetworkConnection conn)
        {
            base.OnServerConnect(conn);

            if (IsSceneActive(RoomScene))
            {
                if (roomSlots.Count == maxConnections)
                    return;

                allPlayersReady = false;

                //if (logger.LogEnabled()) logger.LogFormat(LogType.Log, "NetworkRoomManager.OnServerAddPlayer playerPrefab:{0}", roomPlayerPrefab.name);

                GameObject newRoomGameObject = OnRoomServerCreateRoomPlayer(conn);
                if (newRoomGameObject == null)
                    newRoomGameObject = Instantiate(roomPlayerPrefab.gameObject, clientIndex * new Vector3(140f/4, 30f), Quaternion.identity);

                NetworkServer.AddPlayerForConnection(conn, newRoomGameObject);
                RecalculateRoomPlayerIndices();
            }
            clientIndex++;
            _connections.Add(conn);
        }

        void OnPlayerCreated(GameObject player, int index)
        {
            if (index >= _connections.Count)
            {
                Debug.LogError("Index of player out of range", this);
            }
            NetworkServer.Spawn(player, _connections[index]);
        }

        #endregion
    }
}
