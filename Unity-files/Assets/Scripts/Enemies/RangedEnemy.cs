using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    Transform player;

    [SerializeField]
    float shootingRange = 5.0f;

    [SerializeField] 
    GameObject laser;

    [SerializeField]
    float fireRate = 1f;

    [SerializeField]
    float firstFireDelay = 1f;

    [SerializeField]
    float xBound = 8f;

    bool isFollowingPlayer = true;
    
    [SerializeField] Transform launcher; //launcher that rotates
    
    AudioSource audio;


    private void Start()
    {
        player = GameObject.Find("Player").transform;
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (player)
        {
            // look at player
            launcher.right = player.position - transform.position;

            float dist = Vector2.Distance(transform.position, player.position);

            if (dist <= shootingRange && transform.position.x < xBound)
            {        
                // if we were just following but now in range, start shooting
                if (isFollowingPlayer)
                {
                    InvokeRepeating("Fire", firstFireDelay, fireRate);
                }

                isFollowingPlayer = false;

            } else
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                // if we were just shooting but now too far, start following
                if (isFollowingPlayer == false)
                {
                    CancelInvoke("Fire");
                }
                isFollowingPlayer = true;
            }
            
        }
    }

    void Fire() //probably gonna have to switch off to object pooling. Janky spawning for now
    {
        audio.Play();
        Instantiate(laser, launcher.position, launcher.rotation);
    }


}
