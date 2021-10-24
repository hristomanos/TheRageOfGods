using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    Transform m_TargetPosition;
    
    NavMeshAgent m_NavMeshAgent;

    [SerializeField] int m_HP;


    public void SetTargetPosition(Transform targetPosition) { m_TargetPosition = targetPosition; }



    // Start is called before the first frame update
    void Awake()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        m_NavMeshAgent.destination = m_TargetPosition.position;
    }

   public void TakeDamage(int damage)
    {
        m_HP -= damage;

        if (m_HP <= 0)
        {
            m_HP = 0;

            //Enemy Killed
            Destroy(gameObject);
        }
    }


}
