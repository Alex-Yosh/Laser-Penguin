using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    int level = 1;
    int totalToSpawn = 1;
    int totalSpawned = 0;

    [SerializeField]
    GameObject[] enemies;

    [SerializeField]
    float spawnDelayMin = 2f;

    [SerializeField]
    float spawnDelayMax = 4f;

    [SerializeField]
    float enemyTopSpawn = 5f;

    [SerializeField]
    float enemyBottomSpawn = -3f;

    [SerializeField]
    float spawnDelayDecreasePerLevel = 0.1f;

    [SerializeField]
    float timeUntilNextWaveForceStarts = 5f;

    RandomEventManager randomEventManager;

    private bool eventOccuring = false;


    void Event_OnStart()
    {
        eventOccuring = true;
        CancelInvoke("SpawnEnemy");
    }

    void Event_OnStop()
    {
        eventOccuring = false;
        Invoke("SpawnEnemy", spawnDelayMin);
    }


    // Start is called before the first frame update
    void Start()
    {
        randomEventManager = GameObject.Find("RandomEventSpawner").GetComponent<RandomEventManager>();
        if (randomEventManager)
        {
            randomEventManager.eventStart += Event_OnStart;
            randomEventManager.eventStop += Event_OnStop;
        }

        Invoke("SpawnEnemy", 5f);
    }

    void PrepareNextLevel()
    {
        totalToSpawn++;
        totalSpawned = 0;
        level++;
        spawnDelayMax = Mathf.Clamp(spawnDelayDecreasePerLevel - 0.1f, spawnDelayMin, float.MaxValue);

        // start countdown until next wave starts
        Invoke("SpawnEnemy", timeUntilNextWaveForceStarts);
    }

    void SpawnEnemy()
    {
        float y = Random.Range(enemyBottomSpawn, enemyTopSpawn);
        int enemyType = Random.Range(0, enemies.Length);
        Instantiate(enemies[enemyType], new Vector3(transform.position.x, y), transform.rotation, transform);
        totalSpawned++;


        if (eventOccuring)
        {
            return;
        }

        if (totalSpawned == totalToSpawn)
        {
            PrepareNextLevel();
        }
        else
        {
            float delayTillNextEnemy = Random.Range(spawnDelayMin, spawnDelayMax);
            Invoke("SpawnEnemy", delayTillNextEnemy);
        }
    }
}
