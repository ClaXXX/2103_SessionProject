using System.Collections.Generic;
using UnityEngine;

namespace Particles {
    public class ParticleSystemPool : MonoBehaviour {
        private Stack<Transform> objectPool; 
        private Transform cache;
        private Transform particleSystemPrefab;
        
        [SerializeField]
        private int maxAmount = 1;

        void Awake() { 
            for (int i = 0; i < maxAmount; i++) {
                // TODO : Vérifier l'ordre ici
                particleSystemPrefab.transform.parent = cache;
                Instantiate(particleSystemPrefab);
                objectPool.Push(particleSystemPrefab);
            }
        }
        
        public Transform GetFromPool(Transform parent) {
            if (objectPool.Count == 0) {
                return null;
            }
            
            particleSystemPrefab = objectPool.Pop();
            particleSystemPrefab.transform.parent = parent;

            return particleSystemPrefab; 
        }

        public void ReturnToPool(Transform unusedInstance) {
            objectPool.Push(unusedInstance);
            unusedInstance.transform.parent = cache;
        }
    }
}