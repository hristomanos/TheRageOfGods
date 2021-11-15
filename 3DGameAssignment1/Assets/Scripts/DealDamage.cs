using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is about dealing damage to the receiver when the projectile collides with them
public class DealDamage : MonoBehaviour
{
   [SerializeField] GameObject m_ImpactVFX;
   [SerializeField] float m_TimeAlive;
   [SerializeField] int m_Damage;
   [SerializeField] string m_Shooter;
   [SerializeField] string m_Receiver;

    private void Start()
    {
        //If the projectile does not hit anything within the time limit, destroy object
        Destroy(gameObject, m_TimeAlive);
    }

    bool collided = false;
    private void OnCollisionEnter(Collision collision)
    {
        //Check that you are not colliding with yourself and the shooter
        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != m_Shooter && !collided)
        {
            collided = true;

            //Check the tag against the receiver and depending on the name, call the associated script
            if (collision.gameObject.CompareTag(m_Receiver))
            {
                switch(m_Receiver)
                {
                    case "Enemy":
                        collision.gameObject.GetComponent<AI>().TakeDamage(m_Damage);
                        break;
                    case "Player":
                        collision.gameObject.GetComponentInChildren<Player>().TakeDamage(m_Damage);
                        break;
                }
               
            }

            //Instantiate the particle effect at the point of impact
            GameObject impact = Instantiate(m_ImpactVFX, collision.contacts[0].point, Quaternion.identity);

            //Let particle effect play the animation and destroy both the objects
            Destroy(impact, 1);
            Destroy(gameObject);
        }
    }
    






}
