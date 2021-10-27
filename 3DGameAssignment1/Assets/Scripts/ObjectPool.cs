using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script is about generating a random list enemies and instantiating them at the start of the game for better performance
//Need to change it into generating a pool for each enemy and then choosing randomly wich one to spawn
public class ObjectPool : MonoBehaviour
{
    //List of each enemy prefab
    [SerializeField] List<ProbabilityEnemy> m_EnemyPrefabs = new List<ProbabilityEnemy>();

    //List of object pool
    List<GameObject> m_EnemyPrefabPool = new List<GameObject>();

    //A list of the spawinning points
    [SerializeField] List<Transform> m_SpawnningPoints;
    const int MAXNUMENEMIES = 10;
    void Start()
    {
        
    }



    
    
  void GenerateEnemyPoolList()
    {
        for (int i = 0; i < MAXNUMENEMIES; i++)
        {
            m_EnemyPrefabPool.Add(GetRandomEnemyType());
        }
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


[System.Serializable]
public class ProbabilityEnemy
{
    public GameObject spawnObject;
    public int minProbabilityRange = 0;
    public int maxProbabilityRange = 0;
}

