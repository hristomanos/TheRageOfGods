using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSpell : MonoBehaviour
{

    [SerializeField] GameObject m_SpellPrefab;
    [SerializeField] Transform m_SpawinningPoint;
    [SerializeField] Animator m_WandAnimator;

    [SerializeField] float m_Damage;

 
    

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
        Debug.Log("Shooting");
        m_WandAnimator.Play("WandDown");
        Instantiate(m_SpellPrefab, m_SpawinningPoint.position, m_SpawinningPoint.rotation);
        
    }

  
}
