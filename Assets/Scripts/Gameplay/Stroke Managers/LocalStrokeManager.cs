using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Stroke_Managers
{
    public class LocalStrokeManager : MonoBehaviour
    {
        public StrokeManager strokeManager;

        private void Start()
        {
            strokeManager.Stroke += strokeManager.StrokeTheBall;
        }

        private void Update()
        {
            strokeManager.UpdateFrame();
        }

        private void FixedUpdate()
        {
            strokeManager.FixedUpdatePhysic();
        }
    }
}