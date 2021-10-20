using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    [SerializeField] GameObject m_WeaponPrefab;
    [SerializeField] Transform m_SpawningPoint;
    [SerializeField] float m_Force;

    Rigidbody m_WeaponRigidbody;
    SphereCollider m_WeaponSphereCollider;
    PickUp m_PickUpScript;


    // Start is called before the first frame update
    void Start()
    {
       m_WeaponRigidbody = m_WeaponPrefab.GetComponent<Rigidbody>();

       m_WeaponSphereCollider = m_WeaponPrefab.GetComponent<SphereCollider>();

       m_PickUpScript = GetComponentInChildren<PickUp>();
    }

    // Update is called once per frame
    void Update()
    {
        //Shoot
        if (Input.GetMouseButton(0))
        {
            if (m_PickUpScript.GetIsHolding())
            { 
                //Reset flag
                m_PickUpScript.SetIsHolding(false);

                //Free the ball from being a child of camera
                m_WeaponPrefab.transform.parent = null;

                //Enable gravity
                m_WeaponRigidbody.useGravity = true;

                //Change layer back to "Default" to allow 
                m_WeaponPrefab.layer = LayerMask.NameToLayer("Default");
               
                //Shoot dodgeball
                m_WeaponRigidbody.AddForce(Camera.main.transform.forward * m_Force, ForceMode.Impulse);
                
                //Disable trigger collider
                m_WeaponSphereCollider.isTrigger = false;
                
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && !m_PickUpScript.GetIsHolding())
        {
            ResetBall();
        }


    }

    void ResetBall()
    {
        //Reset rotation
        m_WeaponRigidbody.velocity = Vector3.zero;
        m_WeaponPrefab.transform.localRotation = Quaternion.identity;
        m_WeaponRigidbody.rotation = Quaternion.identity;
        m_WeaponRigidbody.angularVelocity = Vector3.zero;

        //Make the ball a child of the camera
        m_WeaponPrefab.gameObject.transform.parent = Camera.main.transform;
        
        //Place the ball at the spawning point
        m_WeaponPrefab.gameObject.transform.position = new Vector3(m_SpawningPoint.position.x, m_SpawningPoint.position.y, m_SpawningPoint.position.z);

        //Enable trigger collider
        m_WeaponSphereCollider.isTrigger = true;

        //Disable gravity
        m_WeaponRigidbody.useGravity = false;

        //Change layer to "Weapon" to make Culling mask work
        m_WeaponPrefab.gameObject.layer = LayerMask.NameToLayer("Weapon");

        //Flag isPicked
        m_PickUpScript.SetIsHolding(true);
    }

}


