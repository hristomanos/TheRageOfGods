using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is about spawnning different types of enemies from certain spawnning points
public class SpawnEnemies : MonoBehaviour
{
    //A list of the enemy prefabs that need to be spawned
  [SerializeField]  List<ProbabilityEnemy> m_EnemyPrefabs;
    //A list of the spawinning points
  [SerializeField] List<Vector3> m_SpawnningPoints;

    [Header("Spawn")]
    int m_InitialSpawnningDelay;
    int m_SpawnningDelay;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(m_InitialSpawnningDelay);
        while (true)
        {
            yield return new WaitForSeconds(m_SpawnningDelay);

        }
    }


    GameObject GetRandomEnemyType()
    {
        int randomNumber = Random.Range(0, 100);
        for (int i = 0; i < m_EnemyPrefabs.Count; i++)
        {
            if (randomNumber >= m_EnemyPrefabs[i].maxProbabilityRange && randomNumber <= m_EnemyPrefabs[i].maxProbabilityRange)
            {
                return m_EnemyPrefabs[i].spawnObject;
            }
        }

        return m_EnemyPrefabs[0].spawnObject;
    }

}

[System.Serializable]
public class ProbabilityEnemy
{
    public GameObject spawnObject;
    public int minProbabilityRange = 0;
    public int maxProbabilityRange = 0;
}
