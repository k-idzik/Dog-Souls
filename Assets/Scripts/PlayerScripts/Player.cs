using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private Animator animator;
    //public Vector3 velocity;
    private float maxSpeed;
    //public Vector3 acceleration;
    //public float maxAcceleration;
    //public float maxDecceleration;
    private float prevVertical = 0;
    private float prevHorizontal = 0;
    public GameObject sword;
    private string pauseState;
    private int health; //Doogo's health
    private float damageCooldown; //The time between when the player can take damage
    private SpriteRenderer playerSR; //The player's sprite renderer
    private Rigidbody2D playerRB; //The player's rigidbody
    private BoxCollider2D playerBC; //The player's box collider
    private float timer;
    public float attackCooldown;

    public int Health //Health property
    {
        get
        {
            return health; //Return the player's health
        }
        set
        {
            health = value;
        }
    }

    public float DamageCooldown
    {
        get { return damageCooldown; }
        set { damageCooldown = value; }
    }

    // Use this for initialization
    void Start ()
    {
        maxSpeed = 1.0f; //Set doggo's max speed
        animator = this.GetComponent<Animator>();
        pauseState = "DogTowards";
        health = 5; //Set the player's health equal to 5
        damageCooldown = 1; //Set the damageCooldown
        playerSR = gameObject.GetComponentInChildren<SpriteRenderer>(); //Get the player's sprite renderer
        playerRB = gameObject.GetComponentInChildren<Rigidbody2D>(); //Get the player's rigidbody
        playerBC = gameObject.GetComponentInChildren<BoxCollider2D>(); //Get the player's box collider
        timer = damageCooldown;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        AnimationControl();
        Attack();
        Movement();
        Blink(); //Blink for cooldown
    }

    void Movement() //Move the player
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); //Horizontal input detection
        float verticalInput = Input.GetAxisRaw("Vertical"); //Vertical input detection

        if ((horizontalInput != 0.0f || verticalInput != 0.0f) && maxSpeed >= 6) //If the player is moving and at max speed
        {
            maxSpeed = 6.0f; //Increase the player's max speed
        }
        else if (horizontalInput == 0.0f && verticalInput == 0.0f) //If the player is not moving
        {
            maxSpeed = 1.0f; //Reset the player's max speed
        }
        else //If the player is moving and not at max speed
        {
            maxSpeed += 0.25f; //Increase the player's max speed
        }

        //if (verticalInput != 0.0f)
        //{
        //    playerBC.offset = new Vector2(-0.04f, 0.0f); //Update the offset of the player's hitbox
        //    playerBC.size = new Vector2(0.6f, 1.3f); //Update the size of the player's hitbox
        //}
        //if (horizontalInput != 0.0f)
        //{
        //    playerBC.offset = new Vector2(0.0f, -0.04f); //Update the offset of the player's hitbox
        //    playerBC.size = new Vector2(1.25f, 1.15f); //Update the size of the player's hitbox
        //}

        Vector2 movementSpeed = new Vector2(horizontalInput, verticalInput).normalized * maxSpeed; //Normalize the player's movement and multiply it by the player's max speed

        playerRB.velocity = movementSpeed; //Add the input to the player's velocity

        //if ((velocity.x * velocity.x) + (velocity.y * velocity.y) + (velocity.z * velocity.z) >= maxSpeed * maxSpeed) //If the combination of all velocities squared is greater than or equal to the maximum speed squared
        //{
        //    velocity.Normalize(); //Normalize the velocity
        //    velocity *= maxSpeed; //Set the velocity equal to the maximum speed
        //}
        //
        //velocity += acceleration; //Add the acceleration to the velocity
        //transform.position += velocity; //Add the velocity to the position of the player
        //
        //acceleration = new Vector3(0, 0, 0); //Reset the player's acceleration to zero


        //if (Input.GetKey("w"))
        //{
        //    if (velocity.y <= 0)
        //    {
        //        velocity.x = 0;
        //        velocity.y = 0;
        //    }
        //    acceleration = new Vector3(0, maxAcceleration);
        //}
        //else if (Input.GetKey("a"))
        //{
        //    if (velocity.x >= 0)
        //    {
        //        velocity.x = 0;
        //        velocity.y = 0;
        //    }
        //    acceleration = new Vector3(-maxAcceleration, 0);
        //}
        //else if (Input.GetKey("s"))
        //{
        //    if (velocity.y >= 0)
        //    {
        //        velocity.x = 0;
        //        velocity.y = 0;
        //    }
        //    acceleration = new Vector3(0, -maxAcceleration);
        //}
        //else if (Input.GetKey("d"))
        //{
        //    if (velocity.x <= 0)
        //    {
        //        velocity.x = 0;
        //        velocity.y = 0;
        //    }
        //    acceleration = new Vector3(maxAcceleration, 0);
        //}
        //else
        //{
        //    if (velocity.x > 0)
        //    {
        //        acceleration += new Vector3(-maxDecceleration, 0);
        //    }
        //    if (velocity.x < 0)
        //    {
        //        acceleration += new Vector3(maxDecceleration, 0);
        //    }
        //    if (velocity.y > 0)
        //    {
        //        acceleration += new Vector3(0, -maxDecceleration);
        //    }
        //    if (velocity.y < 0)
        //    {
        //        acceleration += new Vector3(0, maxDecceleration);
        //    }
        //}
        //
        //if (velocity.x * velocity.x + velocity.y * velocity.y + velocity.z * velocity.z >= maxSpeed * maxSpeed)
        //{
        //    velocity.Normalize();
        //    velocity *= maxSpeed;
        //}
        //
        //velocity += acceleration;
        //transform.position += velocity;
        //
        //acceleration = new Vector3(0, 0, 0);
    }

    void AnimationControl()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        animator.speed = 0.8f;

        //checking if joysticks are connected
        //if (Input.GetJoystickNames().Length == 0) //If there are no joysticks connected
        //{
            if (vertical > .01)
            {
                animator.SetInteger("Direction", 3);
                pauseState = "DogAway";
                playerBC.offset = new Vector2(-0.04f, 0.0f); //Update the offset of the player's hitbox
                playerBC.size = new Vector2(0.6f, 1.3f); //Update the size of the player's hitbox
            }
            else if (vertical < -.01)
            {
                animator.SetInteger("Direction", 1);
                pauseState = "DogTowards";
                playerBC.offset = new Vector2(-0.04f, 0.0f); //Update the offset of the player's hitbox
                playerBC.size = new Vector2(0.6f, 1.3f); //Update the size of the player's hitbox
            }
            else if (horizontal > .01)
            {
                animator.SetInteger("Direction", 2);
                pauseState = "DogRight";
                playerBC.offset = new Vector2(0.0f, -0.04f); //Update the offset of the player's hitbox
                playerBC.size = new Vector2(1.25f, 1.15f); //Update the size of the player's hitbox
            }
            else if (horizontal < -.01)
            {
                animator.SetInteger("Direction", 4);
                pauseState = "DogLeft";
                playerBC.offset = new Vector2(0.0f, -0.04f); //Update the offset of the player's hitbox
                playerBC.size = new Vector2(1.25f, 1.15f); //Update the size of the player's hitbox
            }
            else
            {
                animator.speed = 0.0f;
                animator.Play(pauseState, 0, 0f);
            }
        //}
        //else //If there are joysticks connected
        //{
        //    //if (Mathf.Abs(prevVertical) > Mathf.Abs(vertical) && Mathf.Abs(prevHorizontal) > Mathf.Abs(horizontal))
        //    //{
        //    //    animator.SetInteger("Direction", 0);
        //    //    Debug.Log("ADGTasd");
        //    //}
        //    if (vertical > .01)
        //    {
        //        animator.SetInteger("Direction", 3);
        //        playerBC.offset = new Vector2(-0.04f, 0.0f); //Update the offset of the player's hitbox
        //        playerBC.size = new Vector2(0.6f, 1.3f); //Update the size of the player's hitbox
        //    }
        //    else if (vertical < -.01)
        //    {
        //        animator.SetInteger("Direction", 1);
        //        playerBC.offset = new Vector2(-0.04f, 0.0f); //Update the offset of the player's hitbox
        //        playerBC.size = new Vector2(0.6f, 1.3f); //Update the size of the player's hitbox
        //    }
        //    else if (horizontal > .01)
        //    {
        //        animator.SetInteger("Direction", 2);
        //        playerBC.offset = new Vector2(0.0f, -0.04f); //Update the offset of the player's hitbox
        //        playerBC.size = new Vector2(1.25f, 1.15f); //Update the size of the player's hitbox
        //    }
        //    else if (horizontal < -.01)
        //    {
        //        animator.SetInteger("Direction", 4);
        //        playerBC.offset = new Vector2(0.0f, -0.04f); //Update the offset of the player's hitbox
        //        playerBC.size = new Vector2(1.25f, 1.15f); //Update the size of the player's hitbox
        //    }
        //    //prevHorizontal = horizontal;
        //    //prevVertical = vertical;
        //}
    }

    void Attack()
    {
        if(timer >= attackCooldown)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Vector3 pos = this.transform.position;
                Quaternion q = new Quaternion(0, 0, 0, 0);
                int dir = animator.GetInteger("Direction");
                if (dir == 0 || dir == 1)
                {
                    pos.y -= 1;
                    q = Quaternion.AngleAxis(180, Vector3.forward);
                }
                else if (dir == 2)
                {
                    pos.x += 1;
                    q = Quaternion.AngleAxis(-90, Vector3.forward);
                }
                else if (dir == 3)
                {
                    pos.y += 1;
                }
                else if (dir == 4)
                {
                    pos.x -= 1;
                    q = Quaternion.AngleAxis(90, Vector3.forward);
                }
                Instantiate(sword, pos, q, this.transform);
                timer = 0f;
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D coll) //When something collides with the player
    //{
    //    if (coll.gameObject.tag == "enemy") //If the player is colliding with an enemy
    //    {
    //        health--; //Decrement the player's health
    //        damageCooldown = 1; //Reset the damage cooldown
    //    }
    //}

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