using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public float RotationSpeed = 20f;
    public bool CounterClockwise;

    private Motor _motor;

    private void Awake()
    {
        _motor = transform.parent.parent.gameObject.GetComponent<Motor>();
    }

    private void FixedUpdate()
    {
        var direction = 1f;
        if (CounterClockwise)
        {
            direction = -1f;
        }

        transform.Rotate(Vector3.forward, _motor.Speed * RotationSpeed * direction * Time.deltaTime);
    }
}
