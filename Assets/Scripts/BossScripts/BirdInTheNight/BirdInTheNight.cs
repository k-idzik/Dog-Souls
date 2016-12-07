using UnityEngine;
using System.Collections;

public class BirdInTheNight : Boss {

    private bool[] phases = { true, false };
    private float bombTimer; // when this drops below 0, bomb will drop
    [SerializeField]
    private float bombCooldown; // cooldown between bombs
    [SerializeField]
    private GameObject bomb;
    float fieldMinX;
    float fieldMaxX;
    float fieldMinY;
    float fieldMaxY;
    public GameObject field; //input in Unity
    public double currentTime;
    public int sideMovement;
    public Vector2 velocity;
    float maxSpeed;

	// Use this for initialization
	protected override void Start()
    {
        base.Start(); //Call the base start method

        fieldMinX = field.transform.position.x - 0.65f * field.transform.localScale.x;
        fieldMaxX = field.transform.position.x + 0.65f * field.transform.localScale.x;
        fieldMinY = field.transform.position.y - 0.65f * field.transform.localScale.y;
        fieldMaxY = field.transform.position.y + 0.65f * field.transform.localScale.y;
        currentTime = 0;
        sideMovement = 0;
        velocity = new Vector2(0f, 0f);
        maxSpeed = 0.15f;
        //transform.position = new Vector2(fieldMaxX, fieldMinY);

        // assign bombTimer to bombCooldown
        bombTimer = bombCooldown;
	}
	
	// Update is called once per frame
	protected override void Update()
    {
        base.Update(); //Call the base update method

        currentTime += Time.deltaTime;

        if (currentTime >= 4f)
        {
            sideMovement = ResetPosition();
            currentTime = 0f;
        }

        // PHASE 1
        if (bombTimer <= 0)
        {
            FirstAttack();

            bombTimer = bombCooldown;
        }

        bombTimer -= Time.deltaTime;

        Fly();

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
        return true;
    }
}
