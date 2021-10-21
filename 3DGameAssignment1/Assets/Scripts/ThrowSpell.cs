using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSpell : MonoBehaviour
{

    [SerializeField] GameObject m_SpellPrefab;
    [SerializeField] Transform m_SpawinningPoint;
    [SerializeField] Animator m_WandAnimator;

    [SerializeField] float m_Damage;
    [SerializeField] Camera m_Cam;
    [SerializeField] float projectileSpeed;
    Vector3 m_Destination; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        
           


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

        Debug.Log("Shooting");
        m_WandAnimator.Play("WandDown");
        GameObject projectile =  Instantiate(m_SpellPrefab, m_SpawinningPoint.position, m_SpawinningPoint.rotation);
        projectile.GetComponent<Rigidbody>().velocity = (m_Destination - m_SpawinningPoint.position).normalized * projectileSpeed;
    }

  
}
