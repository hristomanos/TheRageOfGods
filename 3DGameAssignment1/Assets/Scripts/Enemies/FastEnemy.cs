using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : AI
{

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!p_Player.IsDead())
        {
            Attack();
        }
    }

    void Attack()
    {
        float distanceToTarget = Vector3.Distance(p_TargetGameObject.transform.position, transform.position);
        if (distanceToTarget < 1.5f)
        {
            //Damage player!
            p_Player.TakeDamage(p_Damage);
            //Explode!
            SpawnEnemies.Instance.m_AliveEnemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }


}
