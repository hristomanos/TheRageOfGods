using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    [SerializeField] GameObject m_WeaponPrefab;
    [SerializeField] float m_Force;

    Rigidbody m_WeaponRigidbody;
    SphereCollider m_WeaponSphereCollider;
    PickUp m_PickUpScript;

    [SerializeField] Transform m_SpawningPoint;

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
            if (m_PickUpScript.GetIsPickedUp())
            { 
                //1.Reset flag
                m_PickUpScript.SetIsPickeUp(false);

                //2.Free the ball from being a child of camera
                m_WeaponPrefab.transform.parent = null;

                //3.Enable gravity
                m_WeaponRigidbody.useGravity = true;

                //4.Change layer back to "Default"
                m_WeaponPrefab.layer = LayerMask.NameToLayer("Default");
               
                //6.Shoot dodgeball
                m_WeaponRigidbody.AddForce(m_WeaponPrefab.transform.forward * m_Force, ForceMode.Impulse);
                
                //5.Disable trigger collider
                m_WeaponSphereCollider.isTrigger = false;
                
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && !m_PickUpScript.GetIsPickedUp())
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
        
        //2. Place the ball at the spawning point
        m_WeaponPrefab.gameObject.transform.position = new Vector3(m_SpawningPoint.position.x, m_SpawningPoint.position.y, m_SpawningPoint.position.z);

        //Enable trigger collider
        m_WeaponSphereCollider.isTrigger = true;

        //Disable gravity
        m_WeaponRigidbody.useGravity = false;

        //Change layer to "Weapon" to make Culling mask work
        m_WeaponPrefab.gameObject.layer = LayerMask.NameToLayer("Weapon");

        //Flag isPicked
        m_PickUpScript.SetIsPickeUp(true);
    }

}


