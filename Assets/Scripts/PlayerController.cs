using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Motor _motor;
    private Camera _camera;
    private Plane _groundPlane;
    private Vector3 _mousePosition;

    private void Awake()
    {
        _motor = GetComponent<Motor>();
        _camera = Camera.main;
        _groundPlane = new Plane(Vector3.up, Vector3.zero);
        _mousePosition = Vector3.zero;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        UpdateMousePosition();
        SetTargetPosition();
        HandleInput();
    }

    /// <summary>
    /// Update the current mouse position.
    /// </summary>
    private void UpdateMousePosition()
    {
        // Raycast to the ground plane to get the mouse position
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (_groundPlane.Raycast(ray, out var enter))
        {
            _mousePosition = ray.GetPoint(enter);
        }
        else
        {
            _mousePosition = Vector3.zero;
        }
    }

    /// <summary>
    /// Set the target position on the motor.
    /// </summary>
    private void SetTargetPosition()
    {
        _motor.SetTargetPosition(_mousePosition);
    }

    /// <summary>
    /// Handle mouse input.
    /// </summary>
    private void HandleInput()
    {
        // Drive with left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            _motor.Drive = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _motor.Drive = false;
        }
    }
}
