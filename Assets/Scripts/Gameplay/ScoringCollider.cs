using System;
using System.Collections;
using System.Collections.Generic;
using GamePlay;
using UnityEngine;

public class ScoringCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<GameManager>().GameOver();
    }
}
