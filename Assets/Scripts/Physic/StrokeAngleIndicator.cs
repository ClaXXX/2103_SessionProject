using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrokeAngleIndicator : MonoBehaviour
{
    private StrokeManager _strokeManager;

    private void Start()
    {
        _strokeManager = FindObjectOfType<StrokeManager>();

        if (_strokeManager == null)
        {
            Debug.LogError("No Stroke Manager found", this);
        }
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0f, _strokeManager.StrokeAngle, 0f);
    }
}
