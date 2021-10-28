using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeParticleSystemEnabler : MonoBehaviour
{
    [SerializeField] ParticleSystem m_Smoke;


    void PlaySmokeParticleSystem()
    {
        m_Smoke.Play();
    }

}
