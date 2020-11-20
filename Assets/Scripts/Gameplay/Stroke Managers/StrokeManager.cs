using System;
using System.Collections;
using System.Collections.Generic;
using Mirror.Examples.RigidbodyPhysics;
using UnityEngine;
using UnityEngine.Serialization;

public class StrokeManager : MonoBehaviour
{
    private const float MAXStrokeForce = 15f;

    public Rigidbody playerBall;
    public int StrokeCount { get; protected set; }
    public float StrokeAngle { get; protected set; }
    public float StrokeForce { get; protected set; }
    public float StrokeForcePercentage => (StrokeForce / MAXStrokeForce);

    public enum StrokeMode
    {
        Rolling,
        Stroke,
        Static,
        Paused,
        Waiting
    }

    private StrokeMode LastMode;
    public StrokeMode StrokeModeVar { get; protected set;  }

    private void Start()
    {
        StrokeForce = 1f;
        StrokeModeVar = StrokeMode.Static;
    }

    // Update is called once per frame per visual frame -- use this for input
    void Update()
    {
        if (StrokeModeVar != StrokeMode.Static) // If the ball is not waiting to be strike then return
        {
            return;
        }

        float verticalMov = Input.GetAxis("Vertical") * 100f * Time.deltaTime;
        
        UpdateStrokeForce(verticalMov);
        StrokeAngle += Input.GetAxis("Horizontal") * 100f * Time.deltaTime;
        if (Input.GetKeyUp("space")) // The GetKeyUp method only works fine on the Update methods call
        {
            StrokeModeVar = StrokeMode.Stroke;
        }
    }

    // Fixed Update run on every tick of the physics engine, use this for manipulation
    private void FixedUpdate()
    {
        switch (StrokeModeVar)
        {
            case StrokeMode.Paused:
                return;
            case StrokeMode.Waiting:
                return;
            case StrokeMode.Static:
                return; // Could be break, question of performances
            case StrokeMode.Rolling:
                UpdateStrokeMode();
                return;
            default:
                Vector3 direction = new Vector3(0,0,StrokeForce);

                playerBall.AddForce(Quaternion.Euler(0f, StrokeAngle, 0f) * direction, ForceMode.Impulse);
                StrokeModeVar = StrokeMode.Rolling;
                break;
        }
    }

    public void UpdateStrokeMode()
    {
        if (playerBall.IsSleeping())
        {
            StrokeModeVar = StrokeMode.Waiting;
            StrokeCount++;
        }
    }

    void UpdateStrokeForce(float verticalMov)
    {
        StrokeForce += verticalMov;
        if (StrokeForce < 1f)
        {
            StrokeForce = 1f;
        }
        else if (StrokeForce > MAXStrokeForce)
        {
            StrokeForce = MAXStrokeForce;
        }
    }

    public void StopWait()
    {
        StrokeModeVar = StrokeMode.Static;
    }

    public void Paused()
    {
        if (StrokeModeVar == StrokeMode.Paused)
        {
            return;
        }
        LastMode = StrokeModeVar;
        StrokeModeVar = StrokeMode.Paused;
    }

    public void Continue()
    {
        StrokeModeVar = LastMode;
    }
}
