using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] int m_healthPoints;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void TakeDamage(int damage)
    {
        m_healthPoints -= damage;
        //UI Red overlay

        //UI HP orb update
        UIManager.Instance.Decrease(damage);

        if (m_healthPoints <= 0)
        {
            m_healthPoints = 0;

            //UI Game over
            
        }
    }

   

}
