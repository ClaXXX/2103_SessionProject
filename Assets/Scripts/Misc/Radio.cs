using Particles;
using UnityEngine;

public class Radio : MonoBehaviour {
    public ParticleSystemPool activePlayerParticlesSystemPool;
    // Start is called before the first frame update
    void Start() {
        // TODO : Get system de particules
        var test = activePlayerParticlesSystemPool.GetFromPool(this.transform);
        test.rotation = Quaternion.Euler(90, 0, 0);
        //particle.GetComponent<PooledParticleSystem>().startParticleSystem(activePlayerParticlesSystemPool);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
