using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesSpawner : MonoBehaviour
{
    [SerializeField] GameObject m_SpikesPrefab;
    [SerializeField] Transform m_TankPrefab;
    [SerializeField] Tank m_TankScript;

    void SpawnSpikes()
    {
        Vector3 spawnPosition = m_TankScript.GetTargetGameObject().transform.position;
        spawnPosition.y = -2.5f;
        GameObject spikesGameObject = Instantiate(m_SpikesPrefab, spawnPosition,Quaternion.identity);
        Vector3 tankLookAtPosition = new Vector3(m_TankPrefab.position.x, -2.5f, m_TankPrefab.position.z);
        spikesGameObject.transform.LookAt(tankLookAtPosition);
        Destroy(spikesGameObject, 5f);
    }

}

