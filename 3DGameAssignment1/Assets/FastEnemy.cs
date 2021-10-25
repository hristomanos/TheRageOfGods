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
        Attack();
    }

    void Attack()
    {
        float distanceToTarget = Vector3.Distance(p_TargetGameObject.transform.position, transform.position);
        if (distanceToTarget < 1.5f)
        {
            //Damage player!
            m_Player.TakeDamage(p_Damage);
            //Explode!
            Destroy(gameObject);
        }
    }


}
