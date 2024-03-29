﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public GameObject _playerBall;
    private Vector3 _offset;

    void Start()
    {
        _offset = transform.position - _playerBall.transform.position;
    }

    void Update()
    {
        transform.position = _playerBall.transform.position + (transform.rotation * _offset);
    }
}
