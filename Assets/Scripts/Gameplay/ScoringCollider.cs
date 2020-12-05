using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Sounds;
using GamePlay;
using UnityEngine;

public class ScoringCollider : MonoBehaviour {
    private GameObject soundObject;
    private SoundManager soundManager;
    public Action OnGameWin;

    public void Awake() {
        soundObject = GameObject.Find("SoundManager");
        soundManager = soundObject.GetComponent<SoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(gameWin());
    }

    private IEnumerator gameWin() {
        soundManager.playBallInHoleSound(this.transform);

        yield return new WaitForSeconds(1);
        
        OnGameWin?.Invoke();
    }
}
