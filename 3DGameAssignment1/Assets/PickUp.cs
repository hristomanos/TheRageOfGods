using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
   [SerializeField] Transform m_SpawningPoint;
   [SerializeField] Camera m_MainCamera;
    

    //Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If the player picked up the ball
        if (collision.gameObject.CompareTag("Dodgeball"))
        {
            //1. Make the ball a child of the camera
            collision.gameObject.transform.parent = m_MainCamera.transform;

            //2. Place the ball at the spawning point
            collision.gameObject.transform.position = new Vector3(m_SpawningPoint.position.x, m_SpawningPoint.position.y, m_SpawningPoint.position.z);

            //3. Enable trigger collider
            collision.gameObject.GetComponent<SphereCollider>().isTrigger = true;

            //4.Disable gravity
            collision.gameObject.GetComponent<Rigidbody>().useGravity = false;

            //5.Change layer to "Weapon" to make Culling mask work
            collision.gameObject.layer = LayerMask.NameToLayer("Weapon");
        }
    }
    



}
