using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Stroke_Managers
{
    public class BotStrokeManager : MonoBehaviour
    {
        public StrokeManager strokeManager;
        private GameObject _hole;

        private void Start()
        {
            strokeManager.Stroke += strokeManager.StrokeTheBall;
            _hole = GameObject.FindGameObjectWithTag("Finish"); // Find the end
        }

        private void FixedUpdate()
        {
            if (strokeManager.StrokeModeVar == StrokeMode.Static)
            {
                Vector3 vector = _hole.gameObject.transform.position - transform.position;
                
            }
            strokeManager.FixedUpdatePhysic();
        }
    }
}
