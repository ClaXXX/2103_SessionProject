using System;
using System.Collections;
using System.Collections.Generic;
using GamePlay;
using UnityEngine;

public class ScoringCollider : MonoBehaviour {
    public GameObject BallInHoleSound;
    public Action OnGameWin;
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(gameWin());
    }

    private IEnumerator gameWin() {
        GameObject go = Instantiate(BallInHoleSound);
        Destroy(go, 1.4f);

        yield return new WaitForSeconds(1);
        
        OnGameWin?.Invoke();
    }
}
