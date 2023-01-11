using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wheat"))
        {
            other.GetComponent<Wheat>().Harvest();
        }
    }
}
