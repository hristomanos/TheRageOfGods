using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : AI
{

    [Header("Material emmision colour")]
    [ColorUsage(false,true)]
    public Color m_ColourOn;
    [ColorUsage(false, false)]
    public Color m_ColourOff;

    [SerializeField] int m_LerpFastMultiplier;
    [SerializeField] int m_LerpSlowMultiplier;

    Color m_MaterialColour;
    Material m_Material;

    //Math Sin
    float startTime;

    //Flickering
    bool m_IsSlowFlickering = false;
    bool m_IsFastFlickering = false;

    //Charge
    float T;

    protected override void Start()
    {
        startTime = Time.time;
        base.Start();
        m_Material = GetComponent<MeshRenderer>().material;
        m_Material.EnableKeyword("_EMISSION");

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
        //if (m_IsSlowFlickering == false)
        //{
        //    StartCoroutine(SlowFlickering());
        //}


        if (!p_Player.IsDead())
        {
            Attack();
        }
    }

    void Attack()
    {
        float distanceToTarget = Vector3.Distance(p_TargetGameObject.transform.position, transform.position);

        if (distanceToTarget < 5f)
        {
            ChargeBeforeExploding();
        }


        if (distanceToTarget < 1.5f)
        {
            //Damage player!
            p_Player.TakeDamage(p_Damage);


            //Explode!
            SpawnEnemies.Instance.m_AliveEnemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    IEnumerator SlowFlickering()
    {
        m_IsSlowFlickering = true;
        m_MaterialColour = Color.Lerp(m_ColourOff, m_ColourOn, m_LerpSlowMultiplier * Time.time - startTime);
        m_Material.SetColor("_EmissionColor", m_MaterialColour);
        yield return new WaitForSeconds(1.0f);
        m_MaterialColour = Color.Lerp(m_ColourOn, m_ColourOff, m_LerpSlowMultiplier * Time.time - startTime);
        m_Material.SetColor("_EmissionColor", m_MaterialColour);
        yield return new WaitForSeconds(1.0f);
        m_IsSlowFlickering = false;
    }

    void ChargeBeforeExploding()
    {
        T += Time.deltaTime * m_LerpSlowMultiplier;
        T = Mathf.Clamp01(T);


        m_MaterialColour = Color.Lerp(m_ColourOff, m_ColourOn, T);
        m_Material.SetColor("_EmissionColor", m_MaterialColour);
    }
     //1.Flicker faster before exploding
     //2.Charge depending on distance
     //3.Charge once before exploding
}
