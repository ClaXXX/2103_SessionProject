using System;
using System.Linq;
using DefaultNamespace;
using Gameplay.Stroke_Managers;
using Mirror;
using UnityEngine;

namespace Network
{
    public class NetworkStrokeManager : NetworkBehaviour
    {
        public StrokeManager StrokeManager;
        private static event Action<Vector3> UpdateBallPlayer;

        private void Start()
        {
            UpdateBallPlayer += UpdateStroke;
        }

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
            Debug.Log("Start Authority");
            StrokeManager.Stroke += ClientStroke;
            if (!hasAuthority)
                return;
            StrokeManager.player.initializeConfigs(ConfigManager.instance.getPlayerConfigs().First());
        }

        [ClientCallback]
        private void OnDestroy()
        {
            if(!hasAuthority) { return; }

            UpdateBallPlayer -= UpdateStroke;
        }

        void UpdateStroke(Vector3 stroke)
        {
            StrokeManager.playerBall.AddForce(stroke, ForceMode.Impulse);
        }

        [Client]
        void ClientStroke()
        {
            if (!hasAuthority) { return; }
            Vector3 direction = new Vector3(0,0,StrokeManager.StrokeForce);

            CommandStroke(Quaternion.Euler(0f, StrokeManager.StrokeAngle, 0f) * direction);
        }

        [Command]
        void CommandStroke(Vector3 stroke)
        {
            RPCStroke(stroke);
        }

        [ClientRpc]
        void RPCStroke(Vector3 stroke)
        {
            UpdateBallPlayer?.Invoke(stroke);
        }

        private void Update()
        {
            if (!hasAuthority)
            {
                return;
            }
            StrokeManager.UpdateFrame();
        }

        private void FixedUpdate()
        {
            if (!hasAuthority)
            {
                return;
            }
            StrokeManager.FixedUpdatePhysic();
        }
    }
}
