using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : AI
{

    [SerializeField] Animator m_Animator;
    [SerializeField] float m_AttackCoolDown;
    float m_AttackCoolDownTime;
    bool m_bAttacked = false;
   [SerializeField] SpikesSpawner m_SpikeSpawnner;
   protected override void Start()
    {
        base.Start();
        m_AttackCoolDownTime = m_AttackCoolDown;
    }

    
    protected override void Update()
    {
        base.Update();
        AttackCoolDownTimer();
        Attack();
    }




    void Attack()
    {
        float distanceToTarget = Vector3.Distance(p_TargetGameObject.transform.position, transform.position);
        if (distanceToTarget < 15f)
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
        //Get player's position and pass it 
        m_SpikeSpawnner.SetTargetPosition(p_TargetGameObject.transform.position);
        yield return new WaitForSeconds(1.5f);
        m_Animator.Play("CrushDown");

        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("CrushDown"))
        {
            m_bAttacked = true;
        }

    }

    void AttackCoolDownTimer()
    {
        if (m_bAttacked == true)
        {

            m_AttackCoolDownTime -= Time.deltaTime;
            Debug.Log("Cooldown time" + m_AttackCoolDownTime);
            if (m_AttackCoolDownTime <= 0)
            {
                m_bAttacked = false;
                m_AttackCoolDownTime = m_AttackCoolDown;
            }

        } 
    }

}
