using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesDamage : MonoBehaviour
{
    [SerializeField] int m_Damage;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInChildren<Player>();
        if (player != null)
        {
            
            player.TakeDamage(m_Damage);
        }
    }
}
