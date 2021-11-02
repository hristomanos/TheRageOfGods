using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : AI
{

    [SerializeField] SpikesSpawner m_SpikeSpawnner;
    [SerializeField] Animator m_Animator;
    [SerializeField] float m_AttackCoolDown;
    [SerializeField] float m_StoppingDistance;

    float m_AttackCoolDownTime;
    bool m_bAttacked = false;
   protected override void Start()
    {
        base.Start();
        m_AttackCoolDownTime = m_AttackCoolDown;
    }

    
    protected override void Update()
    {
        base.Update();
        if (!p_Player.IsDead())
        {
            Attack();
            AttackCoolDownTimer();

        }
    }




    void Attack()
    {
        float distanceToTarget = Vector3.Distance(p_TargetGameObject.transform.position, transform.position);
        if (distanceToTarget < m_StoppingDistance)
        {
            p_NavMeshAgent.SetDestination(gameObject.transform.position);
            if (m_bAttacked == false)
            {
                //Play animation when close to the player     
                m_Animator.Play("JumpTest");
                
                if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("JumpTest"))
                {
                    StartCoroutine(PauseAnimation());
                    
                }
            }

            
            
        }
    }

    IEnumerator PauseAnimation()
    {
        m_bAttacked = true;
        //Get player's position and pass it 
        m_SpikeSpawnner.SetTargetPosition(p_TargetGameObject.transform.position);
        yield return new WaitForSeconds(1.5f);
        m_Animator.Play("CrushDown");

        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("CrushDown"))
        {
            Debug.Log("Should play smoke");
        }
    }

    void AttackCoolDownTimer()
    {
        if (m_bAttacked == true)
        {

            m_AttackCoolDownTime -= Time.deltaTime;
          
            if (m_AttackCoolDownTime <= 0)
            {
                m_bAttacked = false;
                m_AttackCoolDownTime = m_AttackCoolDown;
            }

        } 
    }

}
