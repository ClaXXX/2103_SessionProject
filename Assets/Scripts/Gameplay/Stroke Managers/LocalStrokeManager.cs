using System;
using UnityEngine;

namespace Gameplay.Stroke_Managers
{
    public class LocalStrokeManager : MonoBehaviour
    {
        public StrokeManager StrokeManager;

        private void Start()
        {
            StrokeManager.Stroke += Stroke;
        }

        private void Update()
        {
            StrokeManager.UpdateFrame();
        }

        private void FixedUpdate()
        {
            StrokeManager.FixedUpdatePhysic();
        }

        void Stroke()
        {
            Vector3 direction = new Vector3(0,0,StrokeManager.StrokeForce);

            StrokeManager.playerBall.AddForce(Quaternion.Euler(0f, StrokeManager.StrokeAngle, 0f) * direction, ForceMode.Impulse);
        }
    }
}