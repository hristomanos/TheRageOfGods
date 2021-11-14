using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is includes the shooting mechanic but also describes how a bullet should behave when shot.
public class ThrowSpell : MonoBehaviour
{

    [SerializeField] GameObject m_SpellPrefab;
    [SerializeField] Transform m_SpawinningPoint;
    [SerializeField] Animator m_WandAnimator;

    [SerializeField] Camera m_Cam;
    [SerializeField] float projectileSpeed;
    Vector3 m_Destination;

    [SerializeField] AudioSource m_FireBallShotAudioSource;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            // m_WandAnimator.Play("WandDown");
            //m_WandAnimator.SetBool("isWandDown", true);
            //m_WandAnimator.SetBool("isWandUp", false);
        }
        //} else if(Input.GetMouseButtonUp(0))
        //{
        //    m_WandAnimator.SetBool("isWandUp", true);
        //    m_WandAnimator.SetBool("isWandDown", false);
        //}
    }

    void Shoot()
    {

        Ray ray = m_Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            m_Destination = hit.point;
        }
        else
            m_Destination = ray.GetPoint(1000);

        
        m_WandAnimator.Play("WandDown");
        m_FireBallShotAudioSource.Play();
        GameObject projectile =  Instantiate(m_SpellPrefab, m_SpawinningPoint.position, m_SpawinningPoint.rotation);
        projectile.GetComponent<Rigidbody>().velocity = (m_Destination - m_SpawinningPoint.position).normalized * projectileSpeed;
    }


    //Break shooting mechanic to two staget
    //One is chargeFireball when mouse is hold
    //Second is shoot when mouse is up
    //Which involves releasing the fireball
  
}
