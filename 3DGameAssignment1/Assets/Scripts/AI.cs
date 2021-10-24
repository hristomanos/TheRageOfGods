using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private Transform m_TargetPosition;
    NavMeshAgent m_NavMeshAgent;

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
}
