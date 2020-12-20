using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Stroke_Managers
{
    public class ObjectPositionActualizor: MonoBehaviour
    {
        public GameObject mainObject;
        private Vector3 _offset;

        void Start()
        {
            _offset = transform.position - mainObject.transform.position;
        }

        void Update()
        {
            transform.position = mainObject.transform.position + (transform.rotation * _offset);
        }
    }
}
