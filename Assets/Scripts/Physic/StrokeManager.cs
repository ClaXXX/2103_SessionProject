using System;
using System.Collections;
using System.Collections.Generic;
using Mirror.Examples.RigidbodyPhysics;
using UnityEngine;

public class StrokeManager : MonoBehaviour
{
    private const float MAXStrokeForce = 15f;

    Rigidbody _playerBall;
    public int StrokeCount { get; protected set; }
    public float StrokeAngle { get; protected set; }
    public float StrokeForce { get; protected set; }
    public float StrokeForcePercentage => (StrokeForce / MAXStrokeForce);

    public enum StrokeMode
    {
        Rolling,
        Stroke,
        Static
    }
    public StrokeMode StrokeModeVar { get; protected set;  }

    private void Start()
    {
        Find_playerBall();
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
        if (Input.GetButtonUp("Fire1")) // The GetKeyUp method only works fine on the Update methods call
        {
            StrokeModeVar = StrokeMode.Stroke;
        }
    }

    // Fixed Update run on every tick of the physics engine, use this for manipulation
    private void FixedUpdate()
    {
        switch (StrokeModeVar)
        {
            case StrokeMode.Static:
                return; // Could be break, question of performances
            case StrokeMode.Rolling:
                UpdateStrokeMode();
                return;
            default:
                Vector3 direction = new Vector3(0,0,StrokeForce);

                _playerBall.AddForce(Quaternion.Euler(0f, StrokeAngle, 0f) * direction, ForceMode.Impulse);
                StrokeModeVar = StrokeMode.Rolling;
                break;
        }
    }

    public void UpdateStrokeMode()
    {
        if (_playerBall.IsSleeping())
        {
            StrokeModeVar = StrokeMode.Static;
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

    private void Find_playerBall()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");

        if (go == null)
        {
            Debug.LogError("Player object not found");
            return;
        }

        _playerBall = go.GetComponent<Rigidbody>();

        if (_playerBall == null)
        {
            Debug.LogError("RigidBody not found on the player " + _playerBall.name);
        }
    }
}
