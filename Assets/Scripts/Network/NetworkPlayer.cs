using System;
using GamePlay;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;

namespace Network
{
    public class NetworkPlayer : NetworkBehaviour
    {
        public PlayerManager playerManager;
        public GameObject hud;
        public Camera playerCamera;
        public static event Action OnNetworkPlay;
        public static event Action AfterNetworkPlaying;
        public static event Action OnNetworkPause;
        public static event Action OnNetworkContinue;
        public GameManager gameManager;

        private void Start()
        {
            OnNetworkPlay += OnPlay;
            AfterNetworkPlaying += AfterPlaying;
            OnNetworkPause += OnPause;
            OnNetworkContinue += OnContinue;
        }
        
        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
            hud.SetActive(true); // Print the player HUD only for the player
            playerManager.StrokeManager.playerBall.gameObject.SetActive(true);
            
            Debug.Log("StartAuthority");
            playerCamera.gameObject.SetActive(true);
            playerManager.OnPlay += OnLocalPlay;
            playerManager.AfterPlaying += AfterLocalPlaying;
            playerManager.OnPause += OnLocalPause;
            playerManager.OnContinue += OnLocalContinue;
            playerManager.readyToPlay = true;
            //OnLocalPlay();
        }
        
        [ClientCallback]
        private void OnDestroy()
        {
            if(!hasAuthority) { return; }

            OnNetworkPlay -= OnPlay;
            AfterNetworkPlaying -= AfterPlaying;
            OnNetworkPause -= OnPause;
            OnNetworkContinue -= OnContinue;
        }

        #region Event
        
        public void OnPlay()
        {
            playerCamera.enabled = true;
            hud.SetActive(true);
            playerManager.StrokeManager.StopWait();
            StartCoroutine(playerManager.Playing());
        }

        public void AfterPlaying()
        {
            playerCamera.enabled = false;
            hud.SetActive(false);
            playerManager.StrokeManager.Wait();
            if (gameManager)
                gameManager.Next();
        }

        public void OnPause()
        {
            playerCamera.enabled = false;
        }

        public void OnContinue()
        {
            playerCamera.enabled = true;
        }
        
        #endregion

        #region Network

       
        [Client]
        void OnLocalPlay()
        {
            CommandExec("onPlay");
        }
        
        [Client]
        void AfterLocalPlaying()
        {
            CommandExec("AfterPlaying");
        }
        
        [Client]
        void OnLocalPause()
        {
            CommandExec("OnPause");
        }
        
        [Client]
        void OnLocalContinue()
        {
            CommandExec("OnContinue");
        }


        [Command]
        void CommandExec(string command)
        {
            RPCExec(command);
        }

        [ClientRpc]
        void RPCExec(string command)
        {
            switch (command)
            {
                case "onPlay":
                    OnNetworkPlay?.Invoke(); break;
                case "AfterPlaying":
                    AfterNetworkPlaying?.Invoke(); break;
                case "OnPause":
                    OnNetworkPause?.Invoke(); break;
                case "OnContinue":
                    OnNetworkContinue?.Invoke(); break;
            }
        } 

        #endregion

    }
}
