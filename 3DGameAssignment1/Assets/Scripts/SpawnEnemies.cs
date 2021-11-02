using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is about spawnning different types of enemies from certain spawnning points
public class SpawnEnemies : MonoBehaviour
{
    private static SpawnEnemies m_Instance;

    public static SpawnEnemies Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = GameObject.FindObjectOfType<SpawnEnemies>();
            }

            return m_Instance;
        }
    }

    enum Spawnstate { SPAWNING, WAITING,COUNTING }

    //List of enemy prefabs with extra weighted probability
    [SerializeField] List<ProbabilityEnemy> m_EnemyPrefabs = new List<ProbabilityEnemy>();

    //A list of spawnning points
    [SerializeField] List<Transform> m_SpawnningPoints;

   public List<GameObject> m_AliveEnemies;

    //Target
    [SerializeField] GameObject m_TargetGameObject;

    [Header("Spawn Delay time")]
    [SerializeField]  int m_TimeInBetweenWaves;
    [SerializeField]  int m_SpawnningDelay;

    //Timer before next wave
   [SerializeField] float m_WaveCountdown;
   [SerializeField] Spawnstate m_Spawnstate;

    //Number of enemies to spawn in the wave
    int m_EnemyAmount = 2;

    float m_SearchCountDown;
    int m_CurrentWave;
    
    void Start()
    {
        m_Spawnstate = Spawnstate.COUNTING;
        m_WaveCountdown = m_TimeInBetweenWaves;
        m_CurrentWave = 1;
        m_SearchCountDown = 1f;
    }


    private void Update()
    {

        if (m_Spawnstate == Spawnstate.WAITING)
        {
            if (EnemyIsAlive() == false)
            {
                WaveCompleted();
            }
            else
            {
                return;
            }

            return;
        }

        if (m_WaveCountdown <= 0)
        {
            if (m_Spawnstate != Spawnstate.SPAWNING)
            {
                StartCoroutine(SpawnWaves());
            }
        }
        else
        {
            m_WaveCountdown -= Time.deltaTime;
        }
    }

    //Time to spawn enemies based on wave rate
    IEnumerator SpawnWaves()
    {
        
        m_Spawnstate = Spawnstate.SPAWNING;
       
        for (int i = 0; i < m_EnemyAmount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(m_SpawnningDelay);
        }

        
        m_Spawnstate = Spawnstate.WAITING;
    }

    bool EnemyIsAlive()
    {
        m_SearchCountDown -= Time.deltaTime;
        if (m_SearchCountDown <= 0)
        {
            m_SearchCountDown = 1f;
            if (m_AliveEnemies.Count == 0)
            {
               
                return false;
            }
        }
        return true;
    }

    Vector3 GetRandomSpawnPoint()
    {
        int randomNumber = Random.Range(0, m_SpawnningPoints.Count);

        return m_SpawnningPoints[randomNumber].position;

    }

    void SpawnEnemy()
    {

       GameObject instanceGameObject = Instantiate(GetRandomEnemyType(), GetRandomSpawnPoint(), Quaternion.identity);
       instanceGameObject.GetComponent<AI>().SetTargetGameObject(m_TargetGameObject);
       m_AliveEnemies.Add(instanceGameObject);
    }

    void WaveCompleted()
    {
        Debug.Log("Current Wave : " + m_CurrentWave);
        m_Spawnstate = Spawnstate.COUNTING;
        m_EnemyAmount += 2;
        m_WaveCountdown = m_TimeInBetweenWaves;
        m_CurrentWave++;
        UIManager.Instance.UpdateCurrentWavesUI(m_CurrentWave);
        //Update UI
        //Give player some health
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


