using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesSpawner : MonoBehaviour
{
    [SerializeField] GameObject m_SpikesPrefab;
    [SerializeField] Transform m_TankPrefab;

    Vector3 m_SpawnPosition;
   public void SetTargetPosition(Vector3 targetPosition) { m_SpawnPosition = targetPosition; }

    void SpawnSpikes()
    {
        m_SpawnPosition.y = -2.5f;
        GameObject spikesGameObject = Instantiate(m_SpikesPrefab, m_SpawnPosition,Quaternion.identity);
        Vector3 tankLookAtPosition = new Vector3(m_TankPrefab.position.x, -2.5f, m_TankPrefab.position.z);
        spikesGameObject.transform.LookAt(tankLookAtPosition);
        Destroy(spikesGameObject, 5f);
    }
}

