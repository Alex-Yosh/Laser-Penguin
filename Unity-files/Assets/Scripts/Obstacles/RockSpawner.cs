using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField]
    float rockSpeed = 3.5f;
    [SerializeField]
    GameObject rock;

    [SerializeField]
    float topOffset = -2f;

    [SerializeField]
    float bottomOffset = -8f;

    [SerializeField]
    float minSpawnTime = 2f;

    [SerializeField]
    float maxSpawnTime = 10f;

    [SerializeField]
    float randomRotation = 45f;

    [SerializeField]
    float minGapSize = 2f;

    [SerializeField]
    float maxGapSize = 6f;

    [SerializeField]
    float rockSize = 8f;

    RandomEventManager randomEventManager;

    private bool eventOccuring = false;

    void Event_OnStart()
    {
        eventOccuring = true;
        CancelInvoke("SpawnRock");
    }

    void Event_OnStop()
    {
        eventOccuring = false;
        Invoke("SpawnRock", minSpawnTime);
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
        
        Invoke("SpawnRock", 0f);
    }

    void SpawnRock()
    {
        if (eventOccuring)
        {
            return;
        }

        float range = Random.Range(bottomOffset, topOffset);

        float degreeBottom = Random.Range(-randomRotation, randomRotation);
        float degreeTop = Random.Range(-randomRotation, randomRotation);
        Quaternion rotationBottom = Quaternion.Euler(new Vector3(0, 0, degreeBottom));
        Quaternion rotationTop = Quaternion.Euler(new Vector3(0, 0, degreeTop));

        float gapSize = Random.Range(minGapSize, maxGapSize);

        GameObject rockTop = Instantiate(rock, new Vector3(transform.position.x, transform.position.y + range), rotationBottom, transform);
        GameObject rockBot = Instantiate(rock, new Vector3(transform.position.x, transform.position.y + range + rockSize + gapSize), rotationTop, transform);

        rockTop.GetComponent<Rock>().SetSpeed(rockSpeed);
        rockBot.GetComponent<Rock>().SetSpeed(rockSpeed);

        float timeTillNextRock = Random.Range(minSpawnTime, maxSpawnTime);
        Invoke("SpawnRock", timeTillNextRock);
    }

    public void SetScrollSpeed(float spd)
    {
        rockSpeed = spd;
        foreach(Transform child in transform)
        {
            child.gameObject.GetComponent<Rock>().SetSpeed(spd);
        }
    
    }
}
