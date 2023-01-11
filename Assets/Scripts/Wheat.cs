using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheat : MonoBehaviour
{
    public GameObject GrownModel;
    public GameObject HarvestedModel;
    public ParticleSystem WheatParticles;

    private BoxCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        Grow();
    }

    private void Grow()
    {
        GrownModel.SetActive(true);
        HarvestedModel.SetActive(false);
        _collider.enabled = true;
    }

    public void Harvest()
    {
        ParticleBurst();
        GrownModel.SetActive(false);
        HarvestedModel.SetActive(true);
        _collider.enabled = false;
    }

    /// <summary>
    /// Create a burst of wheat particles.
    /// </summary>
    private void ParticleBurst()
    {
        WheatParticles.Play();
    }
}
