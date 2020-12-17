using UnityEngine;

namespace Particles {
    public class PooledParticleSystem : MonoBehaviour {
        public ParticleSystem particleSystem;
        private ParticleSystemRenderer particleSystemRenderer;
        private ParticleSystemPool _pool;

        public void Start() {
            particleSystemRenderer = GetComponent<ParticleSystemRenderer>();
        }

        void startParticleSystem(ParticleSystemPool pool) {
            _pool = pool;

            particleSystemRenderer.enabled = true;
            particleSystem.time = 0;
            particleSystem.Clear(true);
            particleSystem.Play(true);
        }

        void stopParticleSystem() {
            particleSystem.Stop();
            particleSystem.time = 0;
            particleSystem.Clear(true);
            particleSystemRenderer.enabled = false;
        }

        void Update() {
            if (!particleSystem.IsAlive(true) && particleSystemRenderer.enabled)
            {
                _pool.ReturnToPool(transform);
            }
        }
    }
}