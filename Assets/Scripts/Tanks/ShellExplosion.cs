using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class ShellExplosion : MonoBehaviour {

    public AudioSource m_audioSource;

    public ParticleSystem m_particleSystem;

    public GameObject shell;



    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag == "environment")
        {
            ExplosionAudio(); ;
            ParticleExplosion();
        }

        else if (collision.gameObject.tag == "enemy")
        {
            ExplosionAudio();
            ParticleExplosion();
        }
    }

    private void ExplosionAudio()
    {
        m_audioSource.Play();
        Invoke("destroy", 2f);
    }

    private void ParticleExplosion()
    {
        //if (m_particleSystem.isStopped)
        //{
        //    m_particleSystem.Play();
        //}
        m_particleSystem.Play();
    }


    private void destroy()
    {
        Destroy(shell);
    }
}
