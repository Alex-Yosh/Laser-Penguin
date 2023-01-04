using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField]
    float health = 100f;
    [SerializeField] int drainingRate = 1; //per second
    [SerializeField] float collisionDamage = 30f;

    private float totalHealth = 100f;

    bool isInvincible = false;

    [SerializeField]
    float protectionDelay = 1f;

    [SerializeField]
    float flickerSpeed = 0.2f;

    [SerializeField]
    float timeScaleOnDamage = 0.5f;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    private Color spriteCol;
    
    AudioSource eataudio;
    GameObject eataudioplayer;
    
    AudioSource hitaudio;
    GameObject hitaudioplayer;
    
    AudioSource damagedaudio;
    GameObject damagedaudioplayer;

    private void Start()
    {
        totalHealth = health;
        spriteCol = spriteRenderer.color;
        eataudioplayer = GameObject.FindWithTag("EatingSound");
        eataudio = eataudioplayer.GetComponent<AudioSource>();
        hitaudioplayer = GameObject.FindWithTag("HitSound");
        hitaudio = hitaudioplayer.GetComponent<AudioSource>();
        damagedaudioplayer = GameObject.FindWithTag("DamageSound");
        damagedaudio = damagedaudioplayer.GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        UpdateHealth(drainingRate * Time.deltaTime, false); //natural drain
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyLaser")
        {
            if (isInvincible == false)
                hitaudio.Play(0);
            UpdateHealth(collisionDamage, true);
            other.GetComponent<Laser>().DestroyLaser();
        }
        else if (other.tag == "Enemy")
        {
            if (isInvincible == false)
                damagedaudio.Play(0);
            UpdateHealth(collisionDamage, true);
        }
        else if (other.tag == "Wall")
        {
            if (isInvincible == false)
                damagedaudio.Play(0);
            UpdateHealth(collisionDamage, true);
        }
        else if (other.gameObject.tag == "Fish" && other.gameObject.GetComponent<FishMovement>().GetCookedStatus())
        {//heal with cooked fish
            eataudio.Play(0);
            UpdateHealth(-other.gameObject.GetComponent<FishMovement>().GetHealAmount(), false);
            other.gameObject.GetComponent<FishMovement>().DestroyFish();
        }
    }


    private void UpdateHealth(float damage, bool enemyAttack)
    {
        if (isInvincible)
        {
            return;
        }

        health = Mathf.Clamp(health - damage, 0, totalHealth);
        if (health <= 0)
        {
            var canvas = GameObject.Find("Canvas");
            var ui = canvas.gameObject.GetComponent<UIControl>();
            ui.ShowGameOverScreen();
            Time.timeScale = 0;

        }
        else if (enemyAttack)
        {
            isInvincible = true;
            Time.timeScale = timeScaleOnDamage;
            StartCoroutine("Flicker");
            spriteCol.a = 0.5f;
            spriteRenderer.color = spriteCol;
            Invoke("MakeVulnerable", protectionDelay);
        }
    }
    IEnumerator Flicker()
    {
        bool active = true;
        int iterations = (int)(protectionDelay / flickerSpeed);
        for (var i = 0; i < iterations; i++)
        {
            spriteRenderer.gameObject.SetActive(active);
            active = !active;
            yield return new WaitForSeconds(flickerSpeed);
        }
        spriteRenderer.gameObject.SetActive(true);
    }

    private void MakeVulnerable()
    {
        Time.timeScale = 1f;
        isInvincible = false;
        spriteCol.a = 1f;
        spriteRenderer.color = spriteCol;
    }

    public float GetPlayerHealth()
    {
        return health;
    }

    public float GetPlayerHealthPercent()
    {
        return health / totalHealth;
    }
}
