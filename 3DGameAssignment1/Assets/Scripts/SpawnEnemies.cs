using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is about spawnning different types of enemies from certain spawnning points
public class SpawnEnemies : MonoBehaviour
{
    //List of enemy prefabs with extra weighted probability
    [SerializeField] List<ProbabilityEnemy> m_EnemyPrefabs = new List<ProbabilityEnemy>();

    //A list of spawnning points
    [SerializeField] List<Transform> m_SpawnningPoints;

    
    [SerializeField] Transform m_PlayerPosition;

    [Header("Spawn Delay time")]
    [SerializeField]  int m_InitialSpawnningDelay;
    [SerializeField]  int m_SpawnningDelay;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(m_InitialSpawnningDelay);
        while(true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(m_SpawnningDelay);
        }


    }

    Vector3 GetRandomSpawnPoint()
    {
        int randomNumber = Random.Range(0, m_SpawnningPoints.Count);

        return m_SpawnningPoints[randomNumber].position;

    }


    void SpawnEnemy()
    {

       GameObject m_Instance = Instantiate(GetRandomEnemyType(), GetRandomSpawnPoint(), Quaternion.identity);
        m_Instance.GetComponent<AI>().SetTargetPosition(m_PlayerPosition);
    }


    GameObject GetRandomEnemyType()
    {
        int randomNumber = Random.Range(0, 100);
        for (int i = 0; i < m_EnemyPrefabs.Count; i++)
        {
            if (randomNumber >= m_EnemyPrefabs[i].minProbabilityRange && randomNumber <= m_EnemyPrefabs[i].maxProbabilityRange)
            {
                return m_EnemyPrefabs[i].spawnObject;
            }
        }

        return m_EnemyPrefabs[0].spawnObject;
    }



}


