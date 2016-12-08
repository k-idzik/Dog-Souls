using UnityEngine;
using System.Collections;

public class Roller : Boss
{
    [SerializeField] private Sprite vulnerableSprite; //The roller's vulnerable sprite
    private Sprite normalSprite; //The roller's normal sprite
    private GameObject []pylons; //The moving pylons
    private float timer; //Timer for battle sequencing
    private int maxPylons; //The maximum number of pylons
    private int[] selectedPylons; //The pylons to move
    private Vector2[] startPosition; //The start position of each pylon
    private Vector2[] endPosition; //The end position of each pylon
    private GameObject player;
    private CircleCollider2D cCollider; //The roller's circle collider

    // knockback variables
    private bool canKnockback = true; // bool for tracking whether or not the boss can knockback the player
    [SerializeField]
    private float knockbackScale; 

    // spike shooting variables
    [SerializeField]
    private float aimTime; // amount of time before spike fires
    private GameObject reticle;
    private SpriteRenderer reticleSR; //The reticle's sprite renderer
    private float aimTimer; // timer to track when a new missile will spawn
    private bool hasNotFired = true;

    new void Start() //Use this for initialization
    {
        base.Start(); //Call the base start method

        pylons = GameObject.FindGameObjectsWithTag("barrier"); //Find all the pylons
        player = GameObject.FindGameObjectWithTag("Player");
        reticle = GameObject.FindGameObjectWithTag("Reticle");

        CircleCollider2D[] circleColliders = gameObject.GetComponents<CircleCollider2D>(); //Get the circle colliders
        for (int i = 0; i < circleColliders.Length; i++) //For all of the circle colliders
        {
            if (circleColliders[i].radius == 1.28f) //If this is the right collider
            {
                cCollider = circleColliders[i]; //Save the circle collider
            }
        } 

        normalSprite = bossSR.sprite; //Get the normal sprite

        timer = 0; //Set the timer to 0
        aimTimer = aimTime;

        reticleSR = reticle.GetComponent<SpriteRenderer>(); //Get the reticle's sprite renderer
        reticle.SetActive(false);
    }

    protected override void Update() //Update is called once per frame
    {
        Blink(); //Call the blink method

        if (timer == 0) //To select pylons
        {
            SelectPylons(); //Select the pylons
        }
        else if (timer >= 3 && timer < 4) //To move pylons
        {
            MovePylons(); //Move the pylons
        }
        else if (timer >= 4 && timer < 4.05) //Before the attack
        {
            bossSR.color = Color.red; //Change the color of the boss to indicate anger
        }
        else if (timer >= 4.05 && timer < 4.75) //Before the attack
        {
            bossSR.color = Color.white; //Change the color of the boss back to default
            cCollider.isTrigger = true; //Make the collider a trigger
        }
        else if (timer >= 4.75 && timer < 6.75) //Make the boss attack
        {
            RollingAttack(new Vector2(0, 20f), -7.5f); //Roll
        }
        else if (timer >= 6.75 && timer < 13.75) //Make the boss attack
        {
            ShootSpike(); //Pew pew
        }
        else if (timer >= 13.75 && timer < 13.8) //Before the reverse
        {
            reticle.SetActive(false); //Disable the reticle
            bossSR.color = Color.red; //Change the color of the boss to indicate anger
        }
        else if (timer >= 13.8 && timer < 14.5) //Before the reverse
        {
            bossSR.color = Color.white; //Change the color of the boss back to default
        }
        else if (timer >= 14.5 && timer < 16.5) //To move pylons
        {
            bossSR.sprite = normalSprite; //Reset the sprite
            ReverseRollingAttack(new Vector2(0, 10f), 7.5f); //Roll
            ResetPylons(); //Reset the pylons
        }

        timer += Time.deltaTime; //Increment the timer

        if (timer >= 16.5) //To reset the cycle
        {
            timer = 0; //Reset the timer
            cCollider.isTrigger = false; //Make the collider normal
            hasNotFired = true; //Reset hasNotFired
        }
    }

    private void SelectPylons() //Select the pylons
    {
        int numberPylons = 0; //Keep track of the number of pylons to move
        maxPylons = Random.Range(4, 12); //The maximum number of pylons
        selectedPylons = new int[maxPylons]; //The pylons to move
        startPosition = new Vector2[maxPylons]; //The start position of each pylon
        endPosition = new Vector2[maxPylons]; //The end position of each pylon

        while (numberPylons < maxPylons) //While not all of the pylons are chosen
        {
            int randomPylonNumber = Random.Range(0, 16); //Choose a random pylon
            bool hasPylonBeenSelected = false; //If the pylon has already been selected

            for (int i = 0; i < numberPylons; i++) //Check all of the current pylons
            {
                if (selectedPylons[i] == randomPylonNumber) //Check if the pylon is already in the array
                {
                    hasPylonBeenSelected = true; //THe pylon has already been selected
                }
            }

            if (!hasPylonBeenSelected) //If the pylon has not already been selected
            {
                selectedPylons[numberPylons] = randomPylonNumber; //Add the new pylon to the array

                startPosition[numberPylons] = new Vector3(pylons[randomPylonNumber].transform.position.x, 0, 0); //Add the start position of the pylon to the array

                if (pylons[randomPylonNumber].name.Contains("R")) //If this is a right side pylon
                {
                    endPosition[numberPylons] = new Vector3(pylons[randomPylonNumber].transform.position.x - (Random.Range(1, 2) * 2.5f), 0, 0); //Add the end position to the array
                }
                else if (pylons[randomPylonNumber].name.Contains("L")) //If this is a left side pylon
                {
                    endPosition[numberPylons] = new Vector3(pylons[randomPylonNumber].transform.position.x + (Random.Range(1, 2) * 2.5f), 0, 0); //Add the end position to the array
                }

                numberPylons++; //Increment selectedPylons
            }
        }
    }

    private void MovePylons() //Move the pylons
    {
        for (int i = 0; i < selectedPylons.Length; i++) //For all of the pylons to move
        {
            if (pylons[selectedPylons[i]].name.Contains("R") && pylons[selectedPylons[i]].transform.position.x > endPosition[i].x) //If this is a right side pylon
            {
                pylons[selectedPylons[i]].transform.position -= (Vector3)endPosition[i] * Time.deltaTime * 3; //Move the pylon
            }
            else if (pylons[selectedPylons[i]].name.Contains("L") && pylons[selectedPylons[i]].transform.position.x < endPosition[i].x) //If this is a left side pylon
            {
                pylons[selectedPylons[i]].transform.position -= (Vector3)endPosition[i] * Time.deltaTime * 3; //Move the pylon
            }
        }
    }

    private void ResetPylons() //Move the pylons
    {
        for (int i = 0; i < selectedPylons.Length; i++) //For all of the pylons to move
        {
            if (pylons[selectedPylons[i]].name.Contains("R") && pylons[selectedPylons[i]].transform.position.x < startPosition[i].x) //If this is a right side pylon
            {
                pylons[selectedPylons[i]].transform.position += (Vector3)startPosition[i] * Time.deltaTime * .5f; //Move the pylon
            }
            else if (pylons[selectedPylons[i]].name.Contains("L") && pylons[selectedPylons[i]].transform.position.x > startPosition[i].x) //If this is a left side pylon
            {
                pylons[selectedPylons[i]].transform.position += (Vector3)startPosition[i] * Time.deltaTime * .5f; //Move the pylon
            }
        }
    }

    private void RollingAttack(Vector2 moveVector, float stoppingPoint) //Make the boss attack
    {
        if (transform.position.y >= stoppingPoint) //If the roller should move
        {
            transform.position -= (Vector3)moveVector * Time.deltaTime; //Move the boss to attack
        }
        else //If the roller is stopped
        {
            bossSR.sprite = vulnerableSprite; //Switch the roller's sprite
            cCollider.enabled = false; //Disable the collider
        }
    }

    private void ReverseRollingAttack(Vector2 moveVector, float stoppingPoint) //Move the boss back
    {
        if (transform.position.y <= stoppingPoint) //If stop is false
        {
            cCollider.enabled = true; //Enable the circle collider
            transform.position += (Vector3)moveVector * Time.deltaTime; //Move the boss to attack
        }
    }

    /// <summary>
    /// this will target the player and shoot a spike after a cerain amount of time
    /// </summary>
    private void ShootSpike()
    {
        // activate the reticle
        reticle.SetActive(true);

        if (hasNotFired)
        {
            // fire spike
            if (aimTimer <= 0)
            {
                SpawnMissile(TrackPlayer().normalized, transform.position);

                aimTimer = aimTime;

                hasNotFired = false;

                reticleSR.color = Color.white; //Change the color of the reticle
            }
            else if (aimTimer <= .5f && aimTimer > 0)
            {
                aimTimer -= Time.deltaTime;

                reticle.transform.position = player.transform.position;

                reticleSR.color = Color.red; //Change the color of the reticle
            }
            else
            {
                aimTimer -= Time.deltaTime;

                reticle.transform.position = player.transform.position;
            }
        }
        else //Reset
        {
            hasNotFired = true;
        }
    }

    /// <summary>
    /// Will find the player and apply a knockback force to them
    /// </summary>
    private void Knockback()
    {
        // calculate distance from player center to roller center
        // normalize this vector, reverse it and this will be the 
        // vector on which knockback will be applied

        player.transform.GetComponent<Rigidbody2D>().AddForce(TrackPlayer().normalized * knockbackScale);
    }

    /// <summary>
    /// calculates a vector between the boss and player
    /// </summary>
    /// <returns>that vector</returns>
    private Vector3 TrackPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 dist = player.transform.position - this.transform.position;
        return dist;
    }

    /// <summary>
    /// this will be where the knockback function is called, should be called whenever the roller
    /// collides with a player
    /// </summary>
    /// <param name="coll">collision parameter</param>
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Player")
        {
            Knockback(); //Apply knockback
        }
    }

    protected override void OnTriggerStay2D(Collider2D coll) //If something collides with the boss
    {
        if (coll.gameObject.tag == "weapon" && damageCooldown <= 0f && bossSR.sprite == vulnerableSprite) //If the boss collides with the player's weapon and is vulnerable
        {
            health -= 1; //Decrement health
            damageCooldown = 1; //Reset the damage cooldown
        }
    }
}