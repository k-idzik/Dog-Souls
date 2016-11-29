using UnityEngine;
using System.Collections;

public class BirdInTheNight : Boss {

    float fieldMinX;
    float fieldMaxX;
    float fieldMinY;
    float fieldMaxY;
    [SerializeField] private GameObject field; //input in Unity
    double currentTime;
    int sideMovement;
    Vector2 velocity;
    float maxSpeed;
    public bool vulnerable;

	// Use this for initialization
	protected override void Start()
    {
        base.Start(); //Call the base start method

        fieldMinX = field.transform.position.x - 0.8f * field.transform.localScale.x;
        fieldMaxX = field.transform.position.x + 0.8f * field.transform.localScale.x;
        fieldMinY = field.transform.position.y - 0.8f * field.transform.localScale.y;
        fieldMaxY = field.transform.position.y + 0.8f * field.transform.localScale.y;
        currentTime = 0;
        sideMovement = 0;
        velocity = new Vector2(0f, 0f);
        maxSpeed = 0.15f;
        vulnerable = false;
        //transform.position = new Vector2(fieldMaxX, fieldMinY);
	}
	
	// Update is called once per frame
	protected override void Update()
    {
        base.Update(); //Call the base update method


        if (!isVulnerable())
        {
            currentTime += Time.deltaTime;

            if (currentTime >= 2.5f)
            {
                sideMovement = ResetPosition();
                currentTime = 0f;
            }

            Fly();

            transform.position += new Vector3(velocity.x, velocity.y, 0f);
        }
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

    //temporarily always vulnerable for testing purposes
    //please change
    protected bool isVulnerable()
    {
        return vulnerable;
    }
<<<<<<< HEAD

    protected override void OnTriggerStay2D(Collider2D coll) //If something collides with the boss
    {

        if (coll.gameObject.tag == "barrier")
        {
            vulnerable = true;
        }

        if (coll.gameObject.tag == "weapon" && damageCooldown <= 0f) //If the boss collides with the player's weapon
        {
            health -= 10; //Decrement health
            damageCooldown = 1; //Reset the damage cooldown
            vulnerable = true;
        }

        //Debug.Log(coll.transform.name);


    }
=======
>>>>>>> 557e27e1b4153356c32dfafa2505e29990990a83
}
