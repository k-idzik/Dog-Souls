using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private Animator animator;
    public Vector3 velocity;
    public float maxSpeed;
    public Vector3 acceleration;
    public float maxAcceleration;
    public float maxDecceleration;
    private float prevVertical = 0;
    private float prevHorizontal = 0;
    public GameObject sword;
    private string pauseState;
    private int health; //Doogo's health
    private float damageCooldown; //The time between when the player can take damage
    private SpriteRenderer playerSR; //The player's sprite renderer

    public int Health //Health property
    {
        get
        {
            return health; //Return the player's health
        }
    }

    // Use this for initialization
    void Start () {
        maxSpeed = 0.1f;
        animator = this.GetComponent<Animator>();
        pauseState = "DogTowards";
        health = 5; //Set the player's health equal to 5
        damageCooldown = 1; //Set the damageCooldown
        playerSR = gameObject.GetComponentInChildren<SpriteRenderer>(); //Get the player's sprite renderer
    }
	
	// Update is called once per frame
	void Update () {
        AnimationControl();
        Attack();
        Movement();
        Blink(); //Blink for cooldown
    }

    void Movement()
    {

        if (Input.GetKey("w"))
        {
            if (velocity.y <= 0)
            {
                velocity.x = 0;
                velocity.y = 0;
            }
            acceleration = new Vector3(0, maxAcceleration);
        }
        else if (Input.GetKey("a"))
        {
            if (velocity.x >= 0)
            {
                velocity.x = 0;
                velocity.y = 0;
            }
            acceleration = new Vector3(-maxAcceleration, 0);
        }
        else if (Input.GetKey("s"))
        {
            if (velocity.y >= 0)
            {
                velocity.x = 0;
                velocity.y = 0;
            }
            acceleration = new Vector3(0, -maxAcceleration);
        }
        else if (Input.GetKey("d"))
        {
            if (velocity.x <= 0)
            {
                velocity.x = 0;
                velocity.y = 0;
            }
            acceleration = new Vector3(maxAcceleration, 0);
        }
        else
        {
            if (velocity.x > 0)
            {
                acceleration += new Vector3(-maxDecceleration, 0);
            }
            if (velocity.x < 0)
            {
                acceleration += new Vector3(maxDecceleration, 0);
            }
            if (velocity.y > 0)
            {
                acceleration += new Vector3(0, -maxDecceleration);
            }
            if (velocity.y < 0)
            {
                acceleration += new Vector3(0, maxDecceleration);
            }
        }

        if (velocity.x * velocity.x + velocity.y * velocity.y + velocity.z * velocity.z >= maxSpeed * maxSpeed)
        {
            velocity.Normalize();
            velocity *= maxSpeed;
        }

        velocity += acceleration;
        transform.position += velocity;

        acceleration = new Vector3(0, 0, 0);
    }

    void AnimationControl()
    {
        //checking if joysticks are connected
        if (Input.GetJoystickNames().Length == 0)
        {
            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");
            animator.speed = 0.8f;
            if (vertical > .01)
            {
                animator.SetInteger("Direction", 3);
                pauseState = "DogAway";
            }
            else if (vertical < -.01)
            {
                animator.SetInteger("Direction", 1);
                pauseState = "DogTowards";
            }
            else if (horizontal > .01)
            {
                animator.SetInteger("Direction", 2);
                pauseState = "DogRight";
            }
            else if (horizontal < -.01)
            {
                animator.SetInteger("Direction", 4);
                pauseState = "DogLeft";
            }
            else
            {
                animator.speed = 0.0f;
                animator.Play(pauseState, 0, 0f);
            }
        }
        else
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            if (Mathf.Abs(prevVertical) > Mathf.Abs(vertical) && Mathf.Abs(prevHorizontal) > Mathf.Abs(horizontal))
            {
                animator.SetInteger("Direction", 0);
            }
            else if (vertical > .1)
            {
                animator.SetInteger("Direction", 3);
            }
            else if (vertical < -.01)
            {
                animator.SetInteger("Direction", 1);
            }
            else if (horizontal > .01)
            {
                animator.SetInteger("Direction", 2);
            }
            else if (horizontal < -.01)
            {
                animator.SetInteger("Direction", 4);
            }
            prevHorizontal = horizontal;
            prevVertical = vertical;
        }
    }

    void Attack()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Vector3 pos = this.transform.position;
            Quaternion q = new Quaternion(0, 0, 0, 0);
            int dir = animator.GetInteger("Direction");
            if(dir == 0 || dir == 1)
            {
                pos.y -= 1;
                q = Quaternion.AngleAxis(180, Vector3.forward);
            }
            else if(dir == 2)
            {
                pos.x += 1;
                q = Quaternion.AngleAxis(-90, Vector3.forward);
            }
            else if(dir == 3)
            {
                pos.y += 1;
            }
            else if(dir == 4)
            {
                pos.x -= 1;
                q = Quaternion.AngleAxis(90, Vector3.forward);
            }
            Instantiate(sword, pos, q, this.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll) //When something collides with the player
    {
        if (coll.gameObject.tag == "enemy") //If the player is colliding with an enemy
        {
            health--; //Decrement the player's health
            damageCooldown = 1; //Reset the damage cooldown
        }
    }

    private void Blink() //Make Huebert blink while in cooldown
    {
        if (damageCooldown >= 0f) //If Huebert is in cooldown
        {
            if (playerSR.enabled) //If Huebert's sprite is on
            {
                playerSR.enabled = false; //Turn off Huebert's sprite
            }
            else //If Huebert's sprite is off
            {
                playerSR.enabled = true; //Turn on Huebert's sprite
            }
        }
        else //If Huebert is not in cooldown
        {
            playerSR.enabled = true; //Turn on Huebert's sprite
        }

        if (damageCooldown > 0f) //If the cooldown between hits is less than two
        {
            damageCooldown -= Time.deltaTime; //Increment the cooldown timer
        }
    }
}