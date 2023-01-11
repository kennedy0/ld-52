using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Motor : MonoBehaviour
{
    [Header("Speed")]
    public float MaxSpeed = 10f;
    public float MaxSpeedTime = 2f;
    public float StopTime = .5f;
    
    [Header("Rotation")]
    public float MinRotationSpeed = .5f;
    public float MaxRotationSpeed = 3f;
    
    [Header("Driving")]
    public bool Drive;

    // If the vehicle is close to the target, stop driving
    private const float STOPPING_DISTANCE = 3f;

    // Private variables
    private Rigidbody _rb;
    private Vector3 _targetDirection;
    private float _distanceToTarget;
    private Quaternion _driveRotation;
    
    // Properties
    public float Speed { get; private set; }
    public bool IsDriving { get; private set; }
    public Vector3 TargetPosition { get; private set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _targetDirection = Vector3.zero;
        _distanceToTarget = 0f;
    }

    private void Start()
    {
        
    }

    /// <summary>
    /// Set the target position that the vehicle is driving towards.
    /// </summary>
    public void SetTargetPosition(Vector3 targetPosition)
    {
        TargetPosition = targetPosition;
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
        Speed += (MaxSpeed / MaxSpeedTime) * Time.deltaTime;
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
        Speed -= (MaxSpeed / StopTime) * Time.deltaTime;
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
            // Scale the rotation speed by the max speed
            var rotationPerc = Mathf.Pow(Speed / MaxSpeed, 2f);
            var rotationSpeed = Mathf.Lerp(MinRotationSpeed, MaxRotationSpeed, rotationPerc);
            rotationSpeed *= Time.deltaTime;
            
            // Calculate new direction to face.
            var newDirection = Vector3.RotateTowards(transform.forward, _targetDirection, rotationSpeed, 0f);
            
            // If the target is exactly 180 degrees opposite of the forward direction, the RotateTowards calculation
            // may try to rotate on an axis other than the Y-axis. That results in a non-zero Y direction, which will
            // move the vehicle away from the ground plane.
            newDirection.y = 0f;

            // Calculate rotation towards new direction
            _driveRotation = Quaternion.LookRotation(newDirection);
        }
        else
        {
            // No rotation
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Boundary"))
        {
            HandleBoundaryCollision(collision);
        }
    }

    private void HandleBoundaryCollision(Collision collision)
    {
    }
}
