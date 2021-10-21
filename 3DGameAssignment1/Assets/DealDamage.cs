using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
   [SerializeField] GameObject impactVFX;

    bool collided = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player" && !collided)
        {
            collided = true;

            GameObject impact = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.identity);

            Destroy(impact, 2);
            Destroy(gameObject);
        }
    }
    






}
