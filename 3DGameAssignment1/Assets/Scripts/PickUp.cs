using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
   [SerializeField] Transform m_SpawningPoint;
   [SerializeField] Camera m_MainCamera;

    [SerializeField] bool isPickedUp = false;
    
    public bool GetIsPickedUp() { return isPickedUp; }
    public void SetIsPickeUp(bool flag) { isPickedUp = flag; }

    private void OnCollisionEnter(Collision collision)
    {
        //If the player picked up the ball
        if (collision.gameObject.CompareTag("Dodgeball"))
        {
            //1. Make the ball a child of the camera
            collision.gameObject.transform.parent = m_MainCamera.transform;

            //2. Place the ball at the spawning point
            collision.gameObject.transform.position = new Vector3(m_SpawningPoint.position.x, m_SpawningPoint.position.y, m_SpawningPoint.position.z);
            collision.gameObject.transform.localRotation = Quaternion.identity;

            //Reset Physics
            Rigidbody weaponRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            weaponRigidbody.rotation = Quaternion.identity;
            weaponRigidbody.velocity = Vector3.zero;
            weaponRigidbody.angularVelocity = Vector3.zero;

            //4.Disable gravity
            weaponRigidbody.useGravity = false;

            //3. Enable trigger collider
            collision.gameObject.GetComponent<SphereCollider>().isTrigger = true;

            //5.Change layer to "Weapon" to make Culling mask work
            collision.gameObject.layer = LayerMask.NameToLayer("Weapon");

            //6.Flag isPicked
            isPickedUp = true;
        }
    }
    

   

}
