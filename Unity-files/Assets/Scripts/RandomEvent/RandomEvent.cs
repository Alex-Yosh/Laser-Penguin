using UnityEngine;

public class RandomEvent : MonoBehaviour
{
    public bool isTimeBased = true;
    public bool isChildCountBased = false;
    public bool disableOtherSpawing = true;

    [SerializeField]
    float eventDuration = 15f;

    private void Start()
    {
        if (isTimeBased)
        {
            Invoke("End", eventDuration);
        }
    }

    private void End()
    {
        GameObject.Find("RandomEventSpawner").GetComponent<RandomEventManager>().EndEvent();
        Destroy(gameObject);
    }

    private void Update()
    {
        if (isChildCountBased)
        {
            if (transform.childCount == 0)
            {
                End();
            }
        }
    }
}
