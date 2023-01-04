using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    float xBounds = -17f;

    [SerializeField]
    float speed = 3.5f;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (transform.position.x <= xBounds)
        {
            Destroy(gameObject);
        }

        transform.position += new Vector3(-1f * speed * Time.deltaTime, 0f, 0f);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
