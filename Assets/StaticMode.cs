using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticMode : MonoBehaviour
{
    private StrokeManager _strokeManager;
    void Start()
    {
        if (!(_strokeManager = FindObjectOfType<StrokeManager>()))
        {
            Debug.LogError("Stroke Manager not found", gameObject);
        }
    }

    void Update()
    {
        gameObject.SetActive(_strokeManager.StrokeModeVar == StrokeManager.StrokeMode.Static);
    }
}
