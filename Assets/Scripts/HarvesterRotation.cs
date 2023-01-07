using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvesterRotation : MonoBehaviour
{
    public float RotationSpeed = 10f;
    
    private Motor _motor;
    
    private void Awake()
    {
        _motor = transform.parent.parent.parent.gameObject.GetComponent<Motor>();
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, _motor.Speed * RotationSpeed * Time.deltaTime);
    }
}
