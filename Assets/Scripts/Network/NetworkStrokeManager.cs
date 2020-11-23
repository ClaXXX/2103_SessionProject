using Gameplay.Stroke_Managers;
using Mirror;
using UnityEngine;

namespace Network
{
    public class NetworkStrokeManager : NetworkBehaviour
    {
        public StrokeManager StrokeManager;

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
