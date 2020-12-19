using Particles;
using UnityEngine;

public class Radio : MonoBehaviour {
    public ParticleSystemPool activePlayerParticlesSystemPool;
    void Start() {
        var test = activePlayerParticlesSystemPool.GetFromPool(this.transform);
        test.rotation = Quaternion.Euler(90, 0, 0);
    }
}
