using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] Image hungerBar;

    private PlayerDamage playerHealth;


    [SerializeField] Text pointsText;
    [SerializeField] float pointsIncrement = 0.5f;
    [SerializeField] float pointsEvery = 1f;
    public static float points = 0f;

    void Start()
    {   
        playerHealth = FindObjectOfType<PlayerDamage>();
        InvokeRepeating("IncrementPoints", 0f, pointsEvery);
    }


    void IncrementPoints ()
    {
        points += pointsIncrement;
    }

    // Update is called once per frame
    void Update()
    {
        hungerBar.fillAmount = playerHealth.GetPlayerHealthPercent();
        pointsText.text = "Score: " + points.ToString();
    }

    void Awake()
    {
        points = 0f;
    }

}
