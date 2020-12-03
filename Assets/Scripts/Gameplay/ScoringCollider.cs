using System;
using System.Collections;
using System.Collections.Generic;
using GamePlay;
using UnityEngine;

public class ScoringCollider : MonoBehaviour
{
    public Action OnGameWin;
    private void OnTriggerEnter(Collider other)
    {
        OnGameWin?.Invoke();
    }
}
