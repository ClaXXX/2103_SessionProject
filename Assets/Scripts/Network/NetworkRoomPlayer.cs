using System.Linq;
using DefaultNamespace;
using GamePlay;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;

namespace Network
{
    public class NetworkRoomPlayer : Mirror.NetworkRoomPlayer
    {
        [SyncVar] public string PlayerName = "Player ";

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
            setPlayerName(PlayerIdentity.PlayerName);
        }

        public void setPlayerName(string playerName)
        {
            if (string.IsNullOrEmpty(playerName)) // Checkout that we don't set player name to a null or empty string
                return;
            PlayerName = playerName;
        }
        public override void OnGUI()
        {
            NetworkRoomManager room = Mirror.NetworkManager.singleton as NetworkRoomManager;
            if (room)
            {
                if (!Mirror.NetworkManager.IsSceneActive(room.RoomScene))
                    return;

                DrawPlayerReadyState();
                DrawPlayerReadyButton();
            }
        }

        void DrawPlayerReadyState()
        {
            GUILayout.BeginArea(new Rect(20f + (index * 100), 200f, 90f, 130f));

            GUILayout.Label($"{PlayerName} [{index + 1}]");

            if (readyToBegin)
                GUILayout.Label("Ready");
            else
                GUILayout.Label("Not Ready");

            if (((isServer && index > 0) || isServerOnly) && GUILayout.Button("REMOVE"))
            {
                // This button only shows on the Host for all players other than the Host
                // Host and Players can't remove themselves (stop the client instead)
                // Host can kick a Player this way.
                GetComponent<NetworkIdentity>().connectionToClient.Disconnect();
            }

            GUILayout.EndArea();
        }

        void DrawPlayerReadyButton()
        {
            if (NetworkClient.active && isLocalPlayer)
            {
                GUILayout.BeginArea(new Rect(20f, 300f, 120f, 20f));

                if (readyToBegin)
                {
                    if (GUILayout.Button("Cancel"))
                        CmdChangeReadyState(false);
                }
                else
                {
                    if (GUILayout.Button("Ready"))
                        CmdChangeReadyState(true);
                }

                GUILayout.EndArea();
            }
        }
    }
}
