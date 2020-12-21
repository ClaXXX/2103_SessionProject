using Particles;
using UnityEngine;

public class Radio : MonoBehaviour {
    [SerializeField] ParticleSystemPool radioParticlesSystemPool;
    
    public void playSoundParticles() {
        var test = radioParticlesSystemPool.GetFromPool(transform);
        test.GetComponent<PooledParticleSystem>().startParticleSystem(radioParticlesSystemPool);
        test.rotation = Quaternion.Euler(90, 0, 0);
    }
}
