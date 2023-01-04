using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] bool altControl = false;
    [SerializeField] bool mouseControl = false;

    [Header("Player limit")]
    [SerializeField] float y_limit = 5f;
    [SerializeField] float x_limit = 9f;

    [Header("Player meta stats")]
    [SerializeField] float thrust = 1f; //translation thrust
    [SerializeField] float maxSpeed = 3f; //translation max speed
    [SerializeField] float rotationSpeed = 1f; 
    [SerializeField] float maxRotationSpeed = 3f;

    [SerializeField] GameObject laser;
    [SerializeField] bool laserReady = true; //off cooldown and ready to fire?
    [SerializeField] float laserCooldown = 1f;

    Rigidbody2D rb;

    float horizontal = 0;//holds input from controller
    float vertical = 0;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if(Input.GetButtonDown("Fire1") && laserReady)
        {
            Fire();
        }

        //Limit player position
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -x_limit, x_limit), Mathf.Clamp(transform.position.y, -y_limit, y_limit), 0);
    }

    void FixedUpdate()
    {
        if(mouseControl) //MOUSE CONTROL 
        {
            if(Input.GetKey(KeyCode.Space)) MovePlayer(); //boost
            return;
        }

        MovePlayer();
        RotatePlayer();
    }

    void MovePlayer()
    {
        if(altControl) //ALTERNET CONTROL
        {
            if(Mathf.Abs(rb.velocity.x) < maxSpeed | Mathf.Sign(rb.velocity.x) != Mathf.Sign(horizontal)) //speed limiter
            {
                rb.AddForce(horizontal*thrust*Vector2.right, ForceMode2D.Impulse); //move horizontally
            }
            if(Mathf.Abs(rb.velocity.y) < maxSpeed | Mathf.Sign(rb.velocity.y) != Mathf.Sign(vertical))
            {
                rb.AddForce(vertical*thrust*Vector2.up, ForceMode2D.Impulse); //move vertically
            }
            return;
        }
        

        if(rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(vertical*thrust*transform.right, ForceMode2D.Impulse); //boost
        }
    }

    void RotatePlayer()
    {
        if(altControl) //NO ROTATION IN ALTCONTROL
        {
            return;
        }

        rb.angularVelocity = -horizontal*rotationSpeed; //move horizontally

    }

/* OLD MOVE AND ROTATE
    void MovePlayer()//react to keyinputs for movement
    {
        if(Mathf.Abs(rb.velocity.x) < maxSpeed | Mathf.Sign(rb.velocity.x) != Mathf.Sign(horizontal)) //speed limiter
        {
            rb.AddForce(horizontal*thrust*Vector2.right, ForceMode2D.Impulse); //move horizontally
        }
        if(Mathf.Abs(rb.velocity.y) < maxSpeed | Mathf.Sign(rb.velocity.y) != Mathf.Sign(vertical))
        {
            rb.AddForce(vertical*thrust*Vector2.up, ForceMode2D.Impulse); //move vertically
        }
    }

    void RotatePlayer()
    {
        if(Mathf.Abs(rb.angularVelocity) < maxRotationSpeed | Mathf.Sign(rb.angularVelocity) == Mathf.Sign(horizontal)) //speed limiter
        {
            rb.AddTorque(-horizontal*rotationSpeed, ForceMode2D.Impulse); //move horizontally
        }
    }
*/

    void Fire() //probably gonna have to switch off to object pooling. Janky spawning for now
    {
        Instantiate(laser, transform.position, transform.rotation);
        audio.Play();
    }
}
