using System;
using UnityEngine;

namespace Gameplay.Stroke_Managers
{
    public class LocalStrokeManager : MonoBehaviour
    {
        public StrokeManager StrokeManager;

        private void Update()
        {
            StrokeManager.UpdateFrame();
        }

        private void FixedUpdate()
        {
            StrokeManager.FixedUpdatePhysic();
        }
    }
}