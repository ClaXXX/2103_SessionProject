using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Sounds;
using GamePlay;
using UnityEngine;

public class ScoringCollider : MonoBehaviour {
    public SoundManager soundManager;
    public Action OnGameWin;
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
