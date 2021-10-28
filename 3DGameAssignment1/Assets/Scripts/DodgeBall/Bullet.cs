using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is script describes how a bullet should behave when shot.
public class Bullet : MonoBehaviour
{

    Rigidbody m_Rigidbody;
    Vector3 m_DirectionToTarget;

    [Range(0, 10)]
    [SerializeField] float m_Force;


    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_Rigidbody.AddForce(m_DirectionToTarget * m_Force, ForceMode.Impulse);
    }

   public void SetDirectionToTarget(Vector3 targetPosition)
    {
        m_DirectionToTarget = targetPosition - transform.position;
    }

}
