using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : Enemy
{

    Transform player;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (player)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
         
            // look at player
            transform.right = player.position - transform.position;
        }
    }


}
