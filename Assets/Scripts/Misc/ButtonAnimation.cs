using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    public float yMovement;
    public float frequency;
    private Vector3 _initialPosition;
    private float _initialPositionY;
    
    private void Start()
    {
        _initialPosition = transform.position;
        _initialPositionY = _initialPosition.y;
    }

    private void Update()
    {
        _initialPosition.y = _initialPositionY + Mathf.Sin(Time.time * frequency) * Mathf.PI * yMovement;
        transform.position = _initialPosition;
    }
}