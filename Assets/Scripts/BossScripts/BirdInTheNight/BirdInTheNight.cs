using UnityEngine;
using System.Collections;

public class BirdInTheNight : Boss {

    private bool[] phases = { true, false };
    private float setBombTimer; // when this drops below 0, bomb will drop
    [SerializeField]
    private float bombCooldown; // cooldown between bombs
    [SerializeField]
    private GameObject bomb;
    private float fieldMinX;
    private float fieldMaxX;
    private float fieldMinY;
    private float fieldMaxY;
    private bool vulnerable;
    private int startingHealth;
    private SpriteRenderer sprite;
    [SerializeField] private GameObject field; //input in Unity
    private double currentTime;
    private int sideMovement;
    private Vector2 velocity;
    private float maxSpeed;
    private float zapTimer;
    private float timeStaysZapped;

	// Use this for initialization
	protected override void Start()
    {
        base.Start(); //Call the base start method
        startingHealth = 10;
        health = startingHealth;
        fieldMinX = field.transform.position.x - 0.65f * field.transform.localScale.x;
        fieldMaxX = field.transform.position.x + 0.65f * field.transform.localScale.x;
        fieldMinY = field.transform.position.y - 0.65f * field.transform.localScale.y;
        fieldMaxY = field.transform.position.y + 0.65f * field.transform.localScale.y;
        currentTime = 0;
        sideMovement = 0;
        sprite = this.GetComponent<SpriteRenderer>();
        zapTimer = 0;
        timeStaysZapped = 6.0f;
        velocity = new Vector2(0f, 0f);
        maxSpeed = 0.15f;
        vulnerable = false;
        //transform.position = new Vector2(fieldMaxX, fieldMinY);

        // assign bombTimer to bombCooldown
        setBombTimer = bombCooldown;
	}
	
	// Update is called once per frame
	protected override void Update()
    {
        base.Update(); //Call the base update method

        currentTime += Time.deltaTime;

        if (currentTime >= 4f && !isVulnerable())
        {
            sideMovement = ResetPosition();
            currentTime = 0f;
        }

        // PHASE 1
        if (setBombTimer <= 0 && !isVulnerable())
        {
            FirstAttack();

            setBombTimer = bombCooldown;
        }

        setBombTimer -= Time.deltaTime;
        zapTimer -= Time.deltaTime;

        if (!isVulnerable())
        {
            Fly();
            sprite.color = new Color(1f, 1f, 1f);
        }
        else
        {
            sprite.color = new Color(0.2f, 1f, 0.2f);
            FlyToCenter();
        }
        

        //check to see if he stops being vulnerable
        

        if (zapTimer <= 0)
        {
            vulnerable = false;
        }

        transform.position += new Vector3(velocity.x, velocity.y, 0f);
	}

    //resets the position to the edge of the boss room platform and returns the side it picked
    protected int ResetPosition()
    {
        //set whether the bird moves from the top, right, bottom, or left
        int side = Random.Range(0, 4);

        //top
        if (side == 0)
        {
            transform.position = new Vector2(Random.Range(fieldMinX, fieldMaxX), fieldMaxY);
        }
        //right
        else if (side == 1)
        {
            transform.position = new Vector2(fieldMaxX, Random.Range(fieldMinY, fieldMaxY));
        }
        //bottom
        else if (side == 2)
        {
            transform.position = new Vector2(Random.Range(fieldMinX, fieldMaxX), fieldMinY);
        }
        //left
        else
        {
            transform.position = new Vector2(fieldMinX, Random.Range(fieldMinY, fieldMaxY));
        }

        return side;
    }

    protected void Fly()
    {
        if (sideMovement == 0)
        {
            velocity = new Vector2(0f, -maxSpeed);
        }
        else if (sideMovement == 1)
        {
            velocity = new Vector2(-maxSpeed, 0f);
        }
        else if (sideMovement == 2)
        {
            velocity = new Vector2(0f, maxSpeed);
        }
        else
        {
            velocity = new Vector2(maxSpeed, 0f);
        }
    }

    protected void FlyToCenter()
    {
        if (transform.position.x < (fieldMaxX + fieldMinX)/2 && velocity.x != 0)
        {
            velocity.x = maxSpeed*Mathf.Pow(2, 0.5f)/2;
        }
        if (transform.position.x > (fieldMaxX + fieldMinX) / 2 && velocity.x != 0)
        {
            velocity.x = -maxSpeed*Mathf.Pow(2, 0.5f)/2;
        }
        if (transform.position.y < (fieldMaxY + fieldMinY) / 2 && velocity.y != 0)
        {
            velocity.y = maxSpeed*Mathf.Pow(2, 0.5f)/2;
        }
        if (transform.position.y > (fieldMaxY + fieldMinY) / 2 && velocity.y != 0)
        {
            velocity.y = -maxSpeed*Mathf.Pow(2, 0.5f)/2;
        }
        if (transform.position.x == (fieldMaxX + fieldMinX) / 2)
        {
            velocity.x = 0;
        }
        if (transform.position.y == (fieldMaxY + fieldMinY) / 2)
        {
            velocity.y = 0;
        }
    }

    protected override void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "barrier" && coll.GetComponent<SpriteRenderer>().GetComponent<Renderer>().enabled)
        {
            vulnerable = true;
            zapTimer = timeStaysZapped;
            float percentLifeLeft = health / startingHealth;
            timeStaysZapped = Random.Range(2.0f + 2.0f * (1 - percentLifeLeft), 4.5f + 2.0f * (1 - percentLifeLeft));
            timeStaysZapped = Random.Range(4.0f, 6.5f);
        }
        if (coll.gameObject.tag == "weapon" && damageCooldown <= 0f && isVulnerable()) //If the boss collides with the player's weapon
        {
            health -= 1; //Decrement health
            damageCooldown = 1; //Reset the damage cooldown
        }
    }

    /// <summary>
    /// This is the bomb dropping attack, a new bomb will be
    /// instantiated at the bird's location every time the timer 
    /// reaches 0
    /// </summary>
    private void FirstAttack()
    {
        GameObject.Instantiate(bomb, transform.position, Quaternion.identity);
    }

    //temporarily always vulnerable for testing purposes
    //please change
    protected bool isVulnerable()
    {
        return vulnerable;
    }
}
