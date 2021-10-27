using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] int m_HealthPoints;
    [SerializeField] int m_ManaPoints;




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
        m_HealthPoints -= damage;
        //UI Red overlay

        //UI HP orb update
        UIManager.Instance.DecreaseHealthValue(damage);

        if (m_HealthPoints <= 0)
        {
            m_HealthPoints = 0;

            //UI Game over
            
        }
    }

   public void DecreaseMana(int decreaseAmount)
    {
        m_ManaPoints -= decreaseAmount;

        //Update UI

        if (m_ManaPoints <= 0)
        {
            m_ManaPoints = 0;
        }
        
    }

}
