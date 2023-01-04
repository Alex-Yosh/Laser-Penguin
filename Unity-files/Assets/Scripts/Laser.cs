using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float speed = 1;
    [SerializeField] int damage = 1;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right*speed;
    }

    void Update()
    {
        SelfDestruction();
    }

    void SelfDestruction()
    {
        if(Mathf.Abs(transform.position.x) > 20 | Mathf.Abs(transform.position.y) > 20)
        {
            DestroyLaser();
        }
    }

    public int GetLaserDamage()
    {
        return damage;
    }

    public void DestroyLaser()
    {
        Destroy(gameObject);
    }
}
