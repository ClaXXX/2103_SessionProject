using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Stroke_Managers
{
    public enum Effect
    {
        easeInQuart,
        easeInOutExpo,
        easeInOutQuad,
        NONE
    }

    public class ObjectPositionActualizor: MonoBehaviour
    {
        public GameObject mainObject;
        public Effect effect = Effect.NONE;

        private Vector3 _offset;
        private bool _descendant = false;
        private float x = 1f;

        float easeInQuart(float x) {
            return x * x * x * x;
        }
        
        float easeInOutExpo(float x) {
            return x == 0
                ? 0
                : Math.Abs(x - 1) < 0.01f
                    ? 1
                    : x < 0.5 ? Mathf.Pow(2, 20 * x - 10) / 2
                        : (2 - Mathf.Pow(2, -20 * x + 10)) / 2;;
        }
        
        float easeInOutQuad(float x) {
            return x < 0.5 ? 2 * x * x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2;
        }
        
        void Start()
        {
            _offset = transform.position - mainObject.transform.position;
        }

        void Update()
        {
            Vector3 offset = _offset;

            if (effect != Effect.NONE)
            {
                if (x < 0)
                    _descendant = false;
                else if (x > 1f)
                    _descendant = true;
                x = _descendant ? x - 0.05f * Time.deltaTime : x + 0.05f * Time.deltaTime;
            }
            
            switch (effect)
            {
                case Effect.easeInOutExpo:
                    offset *= (easeInOutExpo(x)); break;
                case Effect.easeInQuart:
                    offset *= (easeInQuart(x)); break;
                case Effect.easeInOutQuad:
                    offset *= (easeInQuart(x)); break;            }

            transform.position = mainObject.transform.position + (transform.rotation * offset);
        }
    }
}
