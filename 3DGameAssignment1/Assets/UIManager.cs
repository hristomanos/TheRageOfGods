using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager m_Instance;

    public static UIManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = GameObject.FindObjectOfType<UIManager>();
            }

            return m_Instance;
        }
    }

    [SerializeField] Slider m_HealthOrbSlider;
    [SerializeField] float refillSpeed;
    bool refilling = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void Decrease(int decreaseAmount)
    {
        m_HealthOrbSlider.value -= decreaseAmount;
    }

    public void Refill()
    {
        if (refilling)
        {
            m_HealthOrbSlider.value = m_HealthOrbSlider.value < 100f ? m_HealthOrbSlider.value + (Time.deltaTime * refillSpeed) : m_HealthOrbSlider.value;

            if (m_HealthOrbSlider.value >= 100f)
            {
                refilling = false;
            }
            
        }
    }


}
