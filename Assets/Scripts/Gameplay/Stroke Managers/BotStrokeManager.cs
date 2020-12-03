using System;
using System.Collections.Generic;
using System.Linq;
using GamePlay;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace Gameplay.Stroke_Managers
{
    public class BotStrokeManager : MonoBehaviour
    {
        public StrokeManager strokeManager;
        public float errorPercentage = 0.25f;
        private GameObject _hole;
        private List<GameObject> _players;

        private void Start()
        {
            // Set the difficulty 
            SetDifficulty();
            // Set the random seed
            strokeManager.Stroke += StrokeTheBall;
            _hole = GameObject.FindGameObjectWithTag("Finish"); // Find the end
            _players = GameObject.FindGameObjectsWithTag("Player").ToList(); // Get all components
            
            // take off the current player gameobject
            foreach (GameObject player in _players)
            {
                if (player.gameObject == strokeManager.playerBall.gameObject)
                {
                    _players.Remove(player);
                    break;
                }
            }
        }

        void SetDifficulty()
        {
            switch (GameSettings.Diffulty)
            {
                case BotDifficulties.Normal:
                    errorPercentage = 0.25f; break;
                case BotDifficulties.Hard:
                    errorPercentage = 0.10f; break;
            }
        }

        float CalculateNeededForce(Vector3 vector, float angle)
        {
            float h = vector.z;
            float a = angle * Mathf.Deg2Rad;
            vector.z = 0;
            float distance = vector.magnitude;
            
            vector.z = distance * Mathf.Tan(a);
            distance += h / Mathf.Tan(a);
            return (Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * a))
                    * vector.normalized).z;
        }
        private void FixedUpdate()
        {
            if (strokeManager.StrokeModeVar == StrokeMode.Static)
            {
                strokeManager.Reactions["Stroke"]?.Invoke();
            }
            strokeManager.FixedUpdatePhysic();
        }

        void Stroke(Vector3 vector)
        {
            Quaternion rotation = Quaternion.LookRotation(vector.normalized);
            float force = Mathf.Max(1f, CalculateNeededForce(vector, rotation.w));
            
            strokeManager.playerBall.AddForce(
                rotation
                * new Vector3(0,0, float.IsNaN(force) ? 1f : force), ForceMode.Impulse);
        }
        
        void StrokeTheBall()
        {
            Vector3 target = _hole.gameObject.transform.position;
            Vector3 origin = strokeManager.playerBall.transform.position;
            Vector3 vector = target - origin;

            foreach (GameObject player in _players)
            {
                if (vector.z + vector.x > (target.z - player.transform.position.z) +
                    (target.x - player.transform.position.x)  && vector.z + vector.x >
                    (player.transform.position.z - origin.z) + (player.transform.position.x - origin.x))
                {
                    vector = player.transform.position - origin;
                }
            }

            Stroke(vector + Random.Range(-(errorPercentage / 2), errorPercentage/2) * Vector3.one);
        }

    }
}
