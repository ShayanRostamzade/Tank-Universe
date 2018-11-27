using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject projectile;

    public AudioSource audioSource;

    public ParticleSystem ps;

    public float firePower;

    private void StoppingParticle()
    {
        ps.Stop();
    }

    public void shoot()
    {
        GameObject clone = Instantiate(projectile, transform.position, transform.rotation);
        clone.GetComponent<Rigidbody>().AddRelativeForce(0, 0, firePower, ForceMode.Impulse);
        audioSource.Play();
        ps.Play();
        Invoke("StoppingParticle", 0.5f);                
    }
    
}
