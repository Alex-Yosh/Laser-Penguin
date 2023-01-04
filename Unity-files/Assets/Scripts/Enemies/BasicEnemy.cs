using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    [SerializeField]
    float xBounds = -17f;

    void Update()
    {
        if (transform.position.x <= xBounds)
        {
            Destroy(gameObject);
        }

        transform.position += new Vector3(-1f * speed * Time.deltaTime, 0f, 0f);
    }
}
