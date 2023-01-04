using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class RandomEventManager : MonoBehaviour
{
    [SerializeField]
    float timeTillNextEvent = 30f;

    public UnityAction eventStart;

    public UnityAction eventStop;

    [SerializeField]
    RandomEvent[] randomEvents;

    bool currentEventPreventSpawing = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartEvent", timeTillNextEvent);
    }

    public void EndEvent()
    {
        if (currentEventPreventSpawing)
        {
            eventStop?.Invoke();
        }

        CancelInvoke("StartEvent");
        Invoke("StartEvent", timeTillNextEvent);
    }

    void StartEvent()
    {
        int randomEventIndex = UnityEngine.Random.Range(0, randomEvents.Length);

        GameObject randomEvent = randomEvents[randomEventIndex].gameObject;
        Instantiate(randomEvent, transform.position, transform.rotation, transform);

        currentEventPreventSpawing = randomEvents[randomEventIndex].disableOtherSpawing;

        if (currentEventPreventSpawing)
        {
            eventStart?.Invoke();
        }
    }

}
