using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StaticMode : MonoBehaviour
{
    public GameObject target;
    public StrokeManager strokeManager;

    void Update()
    {
        target.SetActive(strokeManager.StrokeModeVar == StrokeManager.StrokeMode.Static);
    }
}
