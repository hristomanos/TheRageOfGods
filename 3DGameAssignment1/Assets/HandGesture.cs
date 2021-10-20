using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGesture : MonoBehaviour
{

    [SerializeField] Transform m_LoweredPosition;
    [SerializeField] Transform m_RaisedPosition;
    [SerializeField] float speedOnTheWayUp;
    [SerializeField] float speedOnTheWayDown;

    public void Start()
    {
        transform.rotation = m_LoweredPosition.rotation;
    }

    public void Update()
    {

        if (Input.GetMouseButton(0))
        {
            WandDown();
        }
        if (Input.GetMouseButton(1))
        {
            WandUp();
        }


    }

    void WandUp()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, m_RaisedPosition.rotation, speedOnTheWayUp * Time.deltaTime);
        
    }

    void WandDown()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, m_LoweredPosition.rotation, speedOnTheWayDown * Time.deltaTime);
    }

}
