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

        public override void OnRoomServerSceneChanged(string sceneName)
        {
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
            
            _gameManager.OnPlayerCreated = OnPlayerCreated;
            _gameManager.playerPrefabs = playerPrefab;
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

                GameObject newRoomGameObject = Instantiate(roomPlayerPrefab.gameObject, clientIndex * new Vector3(140f/4, 30f), Quaternion.identity);

                NetworkServer.AddPlayerForConnection(conn, newRoomGameObject);
                RecalculateRoomPlayerIndices();
            }
            clientIndex++;
            _connections.Add(conn);
        }

        public void OnPlayerCreated(GameObject player, int index)
        {
            if (index >= _connections.Count)
            {
                Debug.LogError("Index of player out of range", this);
            }
            player.GetComponent<PlayerManager>().setPlayerName((roomSlots[index] as Network.NetworkRoomPlayer).PlayerName);
            Debug.Log((roomSlots[index] as Network.NetworkRoomPlayer).PlayerName + " is the true name");

            if (NetworkServer.active)
            {
                NetworkServer.ReplacePlayerForConnection(_connections[index], player.gameObject);
            }
        }

        #endregion
    }
}
