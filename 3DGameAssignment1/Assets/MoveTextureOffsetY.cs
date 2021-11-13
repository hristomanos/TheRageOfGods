using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTextureOffsetY : MonoBehaviour
{
    [SerializeField] float m_ScrollSpeed;
    
    Material m_Material;
    float m_MovementSpeed;
    Vector2 m_OffsetDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Material = GetComponent<MeshRenderer>().material;
        m_Material.EnableKeyword("_MainTex");
    }

    // Update is called once per frame
    void Update()
    {
        m_MovementSpeed = Time.time * m_ScrollSpeed;
        m_OffsetDirection = new Vector2(0, m_MovementSpeed);
        m_Material.mainTextureOffset = m_OffsetDirection;
    }
}
