using DefaultNamespace;
using Gameplay.Stroke_Managers;
using Mirror;
using UnityEngine;

namespace Network
{
    public class NetworkStrokeManager : NetworkBehaviour
    {
        public StrokeManager StrokeManager;

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
            StrokeManager.player.initializeConfigs(ConfigManager.instance.getPlayerConfigs()[0]);
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
            StrokeManager.FixedUpdatePhysic();
        }
    }
}
