using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : AI
{

    [SerializeField] Animator m_Animator;


   protected override void Start()
    {
        base.Start();
    }

    
    protected override void Update()
    {
        base.Update();
        Attack();
    }




    void Attack()
    {
        float distanceToTarget = Vector3.Distance(p_TargetGameObject.transform.position, transform.position);
        if (distanceToTarget < 15f)
        {
            p_NavMeshAgent.SetDestination(gameObject.transform.position);

            //Play animation when close to the player
            m_Animator.Play("JumpTest");

           

        }
    }

}
