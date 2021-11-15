using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is about spawning different types of enemies in waves from certain spawning points
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

    //Enum of enemy spawning states
    enum Spawnstate { SPAWNING, WAITING,COUNTING }

    //List of enemy prefabs with extra weighted probability
    [SerializeField] List<ProbabilityEnemy> m_EnemyPrefabs = new List<ProbabilityEnemy>();

    //A list of spawnning points
    [SerializeField] List<Transform> m_SpawnningPoints;

    //A list of alive enemies used as a condition for stating that a wave was cleared.
    public List<GameObject> m_AliveEnemies;

    //Target
    [SerializeField] GameObject m_TargetGameObject;
    Player m_PlayerScript;

    [Header("Spawn Delay time")]
    [SerializeField]  int m_TimeInBetweenWaves;
    [SerializeField]  int m_SpawnningDelay;

    //Timer before next wave
    [SerializeField] float m_WaveCountdown;
    [SerializeField] Spawnstate m_Spawnstate;

    //Number of enemies to spawn in the wave
    int m_EnemyAmount = 2;

    //Used for checking if the alive list is empty when the spawnstate is on spawnstate.waiting
    float m_SearchCountDown;

    int m_CurrentWave;
    
    void Start()
    {
        m_Spawnstate = Spawnstate.COUNTING;
        m_WaveCountdown = m_TimeInBetweenWaves;
        m_CurrentWave = 1;
        m_SearchCountDown = 1f;
        m_PlayerScript = m_TargetGameObject.GetComponent<Player>();
    }


    private void Update()
    {
        //If player is dead, stop spawning
        if (m_PlayerScript.IsDead() == false)
        {

            //All enemies have been spawned, I am waiting for the player to kill all enemies
            if (m_Spawnstate == Spawnstate.WAITING)
            {
                //If aliveEnemies list is empty, the wave was cleared
                if (EnemyIsAlive() == false)
                {
                    //Update UI, start counting for the next wave
                    WaveCompleted();
                }
                else
                {
                    return;
                }

                return;
            }
            //Check if the countdown is over
            if (m_WaveCountdown <= 0)
            {
                //Change state
                if (m_Spawnstate != Spawnstate.SPAWNING)
                {
                    //Disable countdown UI
                    UIManager.Instance.DisableWaveCountDown();

                    //Start spawning the next wave
                    StartCoroutine(SpawnWaves());
                }
            }
            else
            {
                //Countdown before next wave
                m_WaveCountdown -= Time.deltaTime;
                UIManager.Instance.DisplayWaveCountDown(m_WaveCountdown);
            }
        }
    }

    //Time to spawn enemies based on wave rate
    IEnumerator SpawnWaves()
    {
        
        m_Spawnstate = Spawnstate.SPAWNING;
       
        //Spawn an enemy depending on the maximum number of enemies for the round
        for (int i = 0; i < m_EnemyAmount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(m_SpawnningDelay);
        }

        
        m_Spawnstate = Spawnstate.WAITING;
    }

    //Check after a second if aliveEnemies list is empty
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

    //Get a random spawn point
    Vector3 GetRandomSpawnPoint()
    {
        int randomNumber = Random.Range(0, m_SpawnningPoints.Count);

        return m_SpawnningPoints[randomNumber].position;

    }

    //Instantiate an enemy and add it on the aliveEnemies list
    void SpawnEnemy()
    {

       GameObject instanceGameObject = Instantiate(GetRandomEnemyType(), GetRandomSpawnPoint(), Quaternion.identity);
       instanceGameObject.GetComponent<AI>().SetTargetGameObject(m_TargetGameObject);
       m_AliveEnemies.Add(instanceGameObject);
    }

    //The wave was cleared so change state and update UI
    void WaveCompleted()
    {
        m_Spawnstate = Spawnstate.COUNTING;
        m_EnemyAmount += 2;
        m_WaveCountdown = m_TimeInBetweenWaves;
        m_CurrentWave++;
        UIManager.Instance.UpdateCurrentWavesUI(m_CurrentWave);   
    }

    //Get random enemy from the list based on its probability
    GameObject GetRandomEnemyType()
    {
        //Get random number from 0 - 100
        int randomNumber = Random.Range(0, 100);
        //Go through all the enemies in the list
        for (int i = 0; i < m_EnemyPrefabs.Count; i++)
        {
            //Check if the random number fits within the boundaries of the enemy probability
            if (randomNumber >= m_EnemyPrefabs[i].minProbabilityRange && randomNumber <= m_EnemyPrefabs[i].maxProbabilityRange)
            {
                //Return that enemy prefab.
                return m_EnemyPrefabs[i].spawnObject;
            }
        }

        //If all fails, return first enemy on the list
        return m_EnemyPrefabs[0].spawnObject;
    }



}

//Class used to added weighted probability to an enemy. The maxProbability range must sum up to the maximum randomNumber.
//First enemy 0-35, Second enemy 36-55, Third enemy 56-100 then rundomNumber = Random.Range(0,100);
[System.Serializable]
public class ProbabilityEnemy
{
    public GameObject spawnObject;
    public int minProbabilityRange = 0;
    public int maxProbabilityRange = 0;
}
