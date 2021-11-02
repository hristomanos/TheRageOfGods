using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] int m_HealthPoints;
    [SerializeField] int m_ManaPoints;

    [SerializeField]   StarterAssets.StarterAssetsInputs m_StarterAssetsInputs;
    [SerializeField] ThrowSpell m_ThrowspellScript;

    public bool IsDead()
    {
        if (m_HealthPoints <= 0)
        {
            return true;
        }
        else
            return false;
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
            m_StarterAssetsInputs.cursorLocked = false;
            //m_StarterAssetsInputs.cursorInputForLook = false;
            m_ThrowspellScript.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            UIManager.Instance.InitiateGameOverScreenSequence();
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
