using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatField : MonoBehaviour
{
    [Header("Dimensions")]
    public int Columns = 10;
    public int Rows = 10;
    public int Spacing = 2;

    [Header("Wheat")]
    public GameObject Wheat;

    private void Awake()
    {
        GenerateField();
    }

    void Start()
    {
        // Placement
        var xStart = -(Columns * Spacing / 2f) + (Spacing / 2f);
        var yStart = -(Rows * Spacing / 2f) + (Spacing / 2f);
        
        // Place each wheat
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Columns; x++)
            {
                var wheat = Instantiate(Wheat, transform);
                wheat.transform.localPosition = new Vector3(xStart + (x * Spacing), 0f, yStart + (y * Spacing));
            }
        }
    }

    /// <summary>
    /// Generate the field.
    /// </summary>
    private void GenerateField()
    {
    }
    
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0f, .5f);
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.DrawCube(Vector3.zero, new Vector3(Columns * Spacing, .1f, Rows * Spacing));
    }
}
