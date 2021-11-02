using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    protected Player p_Player;
    [SerializeField] protected int     p_HP;
    protected GameObject               p_TargetGameObject;

    [SerializeField] protected int     p_Damage;
   protected NavMeshAgent              p_NavMeshAgent;

    // protected Transform p_TargetTransform;
    // public void SetTargetPosition(Transform targetPosition) { p_TargetPosition = targetPosition; }
    public void SetTargetGameObject(GameObject targetGameObject) { p_TargetGameObject = targetGameObject; }
    public GameObject GetTargetGameObject() { return p_TargetGameObject; }
    protected virtual void Start()
    {
        p_NavMeshAgent = GetComponent<NavMeshAgent>();
        p_Player = p_TargetGameObject.GetComponent<Player>();
    }


    // Update is called once per frame
    protected virtual void Update()
    {
        if (p_Player.IsDead())
        {
            PlayerIsDead();
        }
        else
        {
            p_NavMeshAgent.destination = p_TargetGameObject.transform.position;
        }
        
    }

   public void TakeDamage(int damage)
    {
        p_HP -= damage;

        if (p_HP <= 0)
        {
            p_HP = 0;

            //Enemy Killed
            //Remove enemy from list of alive enemies
            SpawnEnemies.Instance.m_AliveEnemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    protected void PlayerIsDead()
    {
        p_NavMeshAgent.SetDestination(gameObject.transform.position);
    }



}
