using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float scrollSpeed = 5f;
    
    BackgroundScroller background;
    RockSpawner rockSpawner;

    void Start()
    {
        background = FindObjectOfType<BackgroundScroller>();
        rockSpawner = FindObjectOfType<RockSpawner>();
    }

    void Update()
    {
        UpdateDifficulty();
    }
    void UpdateDifficulty()
    {
        background.SetScrollSpeed(scrollSpeed);
        rockSpawner.SetScrollSpeed(scrollSpeed);
    }
}
