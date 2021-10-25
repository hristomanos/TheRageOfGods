using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    protected Player m_Player;
    [SerializeField] protected int    p_HP;
    protected GameObject               p_TargetGameObject;

    [SerializeField] protected int p_Damage;
    NavMeshAgent                      m_NavMeshAgent;

    // protected Transform p_TargetTransform;
    // public void SetTargetPosition(Transform targetPosition) { p_TargetPosition = targetPosition; }
    public void SetTargetGameObject(GameObject targetGameObject) { p_TargetGameObject = targetGameObject; }

    protected virtual void Start()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Player = p_TargetGameObject.GetComponent<Player>();
    }


    // Update is called once per frame
    protected virtual void Update()
    {
        m_NavMeshAgent.destination = p_TargetGameObject.transform.position;
        
    }

   public void TakeDamage(int damage)
    {
        p_HP -= damage;

        if (p_HP <= 0)
        {
            p_HP = 0;

            //Enemy Killed
            Destroy(gameObject);
        }
    }


}
