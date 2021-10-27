using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesDamage : MonoBehaviour
{
    [SerializeField] int m_Damage;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTrigger");
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("Player detected");
            player.TakeDamage(m_Damage);
        }
    }
}
