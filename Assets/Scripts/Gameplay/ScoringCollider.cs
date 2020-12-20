using System;
using System.Collections;
using System.Collections.Generic;
using GamePlay;
using Particles;
using Sounds;
using UnityEngine;
using UnityEngine.Serialization;

public class ScoringCollider : MonoBehaviour {
    private GameObject soundObject;
    private SoundManager soundManager;
    public ParticleSystemPool particleSystemPool;
    public Action OnGameWin;
    
    public void Awake() {
        soundObject = GameObject.Find("SoundManager");
        soundManager = soundObject.GetComponent<SoundManager>();
        particleSystemPool = FindObjectOfType<ParticleSystemPool>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(gameWin());
    }

    private IEnumerator gameWin() {
        soundManager.playBallInHoleSound(this.transform);

        yield return new WaitForSeconds(1);
        particleSystemPool.GetFromPool(transform);
        var test = particleSystemPool.GetComponent<PooledParticleSystem>();
        test.startParticleSystem(particleSystemPool);
        
        OnGameWin?.Invoke();
    }
}
