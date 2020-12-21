using System.Collections.Generic;
using UnityEngine;

namespace Particles {
    public class ParticleSystemPool : MonoBehaviour {
        private Stack<Transform> objectPool = new Stack<Transform>(); 
        private Transform cache;
        
        [SerializeField]
        private Transform particleSystemPrefab;
        
        [SerializeField]
        private int maxAmount = 1;

        void Awake() {
            cache = this.transform.parent;
            for (int i = 0; i < maxAmount; i++) {
                particleSystemPrefab.transform.parent = cache;
                var instance = Instantiate(particleSystemPrefab, cache);
                instance.GetComponent<ParticleSystemRenderer>().enabled = false;
                objectPool.Push(instance);
            }
        }
        
        public Transform GetFromPool(Transform parent) {
            if (objectPool.Count == 0) {
                return null;
            }
            
            var test = objectPool.Pop();
            test.position = parent.position;

            return test; 
        }

        public void ReturnToPool(Transform unusedInstance) {
            objectPool.Push(unusedInstance);
            unusedInstance.transform.parent = cache;
        }
    }
}