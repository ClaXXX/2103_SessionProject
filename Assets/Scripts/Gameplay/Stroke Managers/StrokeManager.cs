using System;
using System.Collections.Generic;
using DefaultNamespace.Sounds;
using UnityEngine;

namespace Gameplay.Stroke_Managers
{
    public class StrokeManager : MonoBehaviour
    {
        private const float MAXStrokeForce = 15f;
        public Dictionary<String, Action> Reactions = new Dictionary<string, Action>();
        public Player player;
        private SoundManager soundManager;
        
        public Rigidbody playerBall;
        public int StrokeCount { get; protected set; }
        public float StrokeAngle { get; protected set; }
        public float StrokeForce { get; protected set; }
        public float StrokeForcePercentage => (StrokeForce / MAXStrokeForce);

        private StrokeMode _lastMode;
        public StrokeMode StrokeModeVar { get; protected set;  }

        public Action Stroke;

        private void Start() 
        {
            soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            StrokeForce = 1f;
            StrokeModeVar = StrokeMode.Waiting;
            
            // Set all existing reaction
            Reactions.Add("Add Stroke Strength", 
                () => UpdateStrokeForce(1 * 100f * Time.deltaTime));
            Reactions.Add("Reduce Stroke Strength", 
                () => UpdateStrokeForce(-1 * 100f * Time.deltaTime));
            Reactions.Add("Turn Stroke Direction Left", 
                () => UpdateStrokeAngle(-1 * 100f * Time.deltaTime));
            Reactions.Add("Turn Stroke Direction Right", 
                () => UpdateStrokeAngle(1 * 100f * Time.deltaTime));
            Reactions.Add("Stroke", () => StrokeModeVar = StrokeMode.Stroke);
        }
        
        public void StrokeTheBall() {

            soundManager.playHitSound(playerBall.transform);

            playerBall.AddForce(
                Quaternion.Euler(0f, StrokeAngle, 0f)
                * new Vector3(0,0,StrokeForce), ForceMode.Impulse);
        }

        #region Updates

        // Update is called once per frame per visual frame -- use this for input
        public void UpdateFrame()
        {
            if (StrokeModeVar != StrokeMode.Static) // If the ball is not waiting to be strike then return
            {
                return;
            }
            
            foreach (KeyValuePair<string,Action> pair in Reactions)
            {
                if (player.inputs.isPressed(player.inputs.actionMap[pair.Key]))
                {
                    pair.Value?.Invoke();
                }
            }
        }

        // Fixed Update run on every tick of the physics engine, use this for manipulation
        public void FixedUpdatePhysic()
        {
            if (StrokeModeVar == StrokeMode.Rolling)
            {
                UpdateStrokeMode();
            } else if (StrokeModeVar == StrokeMode.Stroke)
            {
                Stroke?.Invoke();
                StrokeModeVar = StrokeMode.Rolling;
            }
        }

        public void UpdateStrokeMode()
        {
            if (playerBall.IsSleeping())
            {
                StrokeModeVar = StrokeMode.Waiting;
                StrokeCount++;
            }
        }

        public void UpdateStrokeForce(float verticalMov)
        {
            StrokeForce += verticalMov;
            if (StrokeForce < 1f)
            {
                StrokeForce = 1f;
            }
            else if (StrokeForce > MAXStrokeForce)
            {
                StrokeForce = MAXStrokeForce;
            }
        }

        public void UpdateStrokeAngle(float angle)
        {
            StrokeAngle += angle;
        }

        #endregion

        #region GeneralGame

        public void Wait()
        {
            StrokeModeVar = StrokeMode.Waiting;
        }

        public void StopWait()
        {
            StrokeModeVar = StrokeMode.Static;
        }

        public void Paused()
        {
            if (StrokeModeVar == StrokeMode.Paused)
            {
                return;
            }
            _lastMode = StrokeModeVar;
            StrokeModeVar = StrokeMode.Paused;
        }

        public void Continue()
        {
            StrokeModeVar = _lastMode;
        }
        
        #endregion
    }
}
