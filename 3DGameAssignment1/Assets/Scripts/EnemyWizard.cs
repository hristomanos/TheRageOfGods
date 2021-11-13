using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWizard : AI
{

    [SerializeField] float m_StoppingDistance;
    [SerializeField] float m_retreatDistance;

    float m_TimeBtwShots;
    [SerializeField] float m_StartTimeBtwShots;
    [SerializeField] GameObject m_Projectile;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        m_TimeBtwShots = m_StartTimeBtwShots;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(!p_Player.IsDead())
        {
            Attack();
        }
    }

    void Attack()
    {
        float distanceToTarget = Vector3.Distance(p_TargetGameObject.transform.position, transform.position);
        Vector3 directionToTarget = transform.position - p_TargetGameObject.transform.position;
        if (distanceToTarget < m_StoppingDistance && distanceToTarget > m_retreatDistance)
        {
            //Stop moving
            p_NavMeshAgent.SetDestination(gameObject.transform.position);
           



        }
        else if(distanceToTarget < m_retreatDistance)
        {
            p_NavMeshAgent.SetDestination(gameObject.transform.position + directionToTarget);
            gameObject.transform.LookAt(p_TargetGameObject.transform.position);
        }

        if (m_TimeBtwShots <= 0)
        {
            Shoot();
        }
        else
            m_TimeBtwShots -= Time.deltaTime;


    }


    void Shoot()
    {
        Vector3 spawnPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.5f, gameObject.transform.position.z);
        GameObject projectileGameObject = Instantiate(m_Projectile, spawnPosition, Quaternion.identity);
        //projectileGameObject.transform.LookAt(p_TargetGameObject.transform.position);
        projectileGameObject.GetComponent<Bullet>().SetDirectionToTarget(p_TargetGameObject.transform.position);
        m_TimeBtwShots = m_StartTimeBtwShots;
    }

    //Move towards the player till you got some distance
    //Retreat when the player comes closer
    //shoot projectiles



}
