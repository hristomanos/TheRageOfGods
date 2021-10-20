using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Rigidbody m_Rigidbody;

    [Range(0, 10)]
    [SerializeField] float m_Force;


    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Rigidbody.AddForce(Camera.main.transform.forward * m_Force, ForceMode.Impulse);
    }
}
