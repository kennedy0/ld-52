using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    // Speed controls
    public float MaxSpeed = 10f;
    public float Speed = 0f;
    public float TimeToMaxSpeed = 1f;
    
    // Rotation
    public float RotationSpeed = 3f;

    // The target position that the vehicle is driving towards
    public Vector3 TargetPosition = Vector3.zero;
    
    // Whether or not the vehicle is driving
    public bool Drive;

    // If the vehicle is close to the mouse, stop driving
    private const float STOPPING_DISTANCE = 4f;

    private Rigidbody _rb;
    private Vector3 _targetDirection;
    private float _distanceToTarget;
    private Quaternion _driveRotation;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _targetDirection = Vector3.zero;
        _distanceToTarget = 0f;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        CalculateTarget();
    }

    /// <summary>
    ///  Update the target calculations.
    /// </summary>
    private void CalculateTarget()
    {
        _targetDirection = TargetPosition - transform.position;
        _distanceToTarget = _targetDirection.magnitude;
    }

    private void FixedUpdate()
    {
        UpdateDriveSpeed();
        UpdateDriveRotation();
        ApplyMovement();
    }

    /// <summary>
    /// Update the current driving speed.
    /// </summary>
    private void UpdateDriveSpeed()
    {
        // Accelerate and decelerate
        if (Drive && _distanceToTarget > STOPPING_DISTANCE)
        {
            Accelerate();
        }
        else
        {
            Decelerate();
        }
    }

    /// <summary>
    ///  Accelerate the vehicle.
    /// </summary>
    private void Accelerate()
    {
        Speed += (MaxSpeed / TimeToMaxSpeed) * Time.deltaTime;
        if (Speed >= MaxSpeed)
        {
            Speed = MaxSpeed;
        }
    }
    
    /// <summary>
    ///  Accelerate the vehicle.
    /// </summary>
    private void Decelerate()
    {
        Speed -= (MaxSpeed / TimeToMaxSpeed) * Time.deltaTime;
        if (Speed <= 0f)
        {
            Speed = 0f;
        }
    }

    /// <summary>
    /// Rotate the vehicle towards the target.
    /// </summary>
    private void UpdateDriveRotation()
    {
        if (Drive)
        {
            var rotationSpeed = RotationSpeed * Time.deltaTime;
            var newDirection = Vector3.RotateTowards(transform.forward, _targetDirection, rotationSpeed, 0f);
            _driveRotation = Quaternion.LookRotation(newDirection);
        }
        else
        {
            _driveRotation = Quaternion.LookRotation(transform.forward);
        }
    }

    /// <summary>
    /// Apply all movement forces.
    /// </summary>
    private void ApplyMovement()
    {
        // Calculate driving velocity
        var driveVelocity = transform.forward * Speed;
        
        // Do movement
        _rb.MovePosition(_rb.position + driveVelocity * Time.deltaTime);
        
        // Do rotation
        
        _rb.MoveRotation(_driveRotation);
    }
}