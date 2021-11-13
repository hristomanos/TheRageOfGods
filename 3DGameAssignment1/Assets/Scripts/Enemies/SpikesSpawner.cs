using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesSpawner : MonoBehaviour
{
    [SerializeField] GameObject m_SpikesPrefab;
    [SerializeField] Transform m_TankPrefab;

    float m_PositionOffsetY = 3.5f;
    float m_OrientationCorrection = 5f;


    Vector3 m_SpawnPosition;
   public void SetTargetPosition(Vector3 targetPosition) { m_SpawnPosition = targetPosition; }

    void SpawnSpikes()
    {
        m_SpawnPosition.y -= m_PositionOffsetY;
        GameObject spikesGameObject = Instantiate(m_SpikesPrefab, m_SpawnPosition,Quaternion.identity);
        Vector3 lookAtTankPosition = new Vector3(m_TankPrefab.position.x, m_TankPrefab.position.y - m_OrientationCorrection, m_TankPrefab.position.z);
        spikesGameObject.transform.LookAt(lookAtTankPosition);
        Destroy(spikesGameObject, 5f);
    }
}

