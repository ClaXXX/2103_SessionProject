using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StrokeAngleIndicator : MonoBehaviour
{
    public StrokeManager strokeManager;

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0f, strokeManager.StrokeAngle, 0f);
    }
}
