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
        // TODO : Play sound on linked object here
        StartCoroutine(gameWin());
    }

    private IEnumerator gameWin() {
        GameObject go = Instantiate(BallInHoleSound);
        Destroy(go, 2f);

        yield return new WaitForSeconds(2f);
        
        OnGameWin?.Invoke();
    }
}
