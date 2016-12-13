using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //Load scenes

public abstract class Boss : MonoBehaviour
{
    protected bool[] phases = new bool[4] { true, false, false, false }; //Array of booleans to hold the current state
    protected int health; //The boss's health
    protected SpriteRenderer bossSR; //The boss's sprite renderer
    protected float damageCooldown; //The time between when the player can take damage
    [SerializeField] private GameObject missile; //The boss's magic missiles
    [SerializeField] protected GameObject explosion; //The explosion to play on a boss's death
    private float deathTimer; //The death timer for the boss
    private bool[] explosionFlags = new bool[5]; //Explosion flags

    public int Health //The boss's health
    {
        get
        {
            return health; //Return the boss's health
        }
    }

    protected virtual void Start() //Use this for initialization
    {
        health = 10; //Give the boss 10 health
        bossSR = GetComponent<SpriteRenderer>(); //Get the boss's sprite renderer
        damageCooldown = -1; //Set the damageCooldown
        deathTimer = 0; //Initialize the deathTimer

        for (int i = 0; i < explosionFlags.Length; i++) //For each value in explosionFlags
        {
            explosionFlags[i] = false; //Set the explosionFlag to false
        }
    }

    protected virtual void Update() //Update is called once per frame
    {
        Blink(); //Blink
        
        if ((health * 10) <= 75 && health > 0 && !phases[(100 - (health * 10)) / 25]) //If the boss's health is down by an even quarter
        {
            PhaseChange(); //Change the phase
        }

        KillBoss(); //Kill the boss
    }

    protected virtual void OnTriggerStay2D(Collider2D coll) //If something collides with the boss
    {
        if (coll.gameObject.tag == "weapon" && damageCooldown <= 0f) //If the boss collides with the player's weapon
        {
            health -= 1; //Decrement health
            damageCooldown = 1; //Reset the damage cooldown
        }
    }

    protected virtual void PhaseChange() //When the boss should change phases
    {
        for (int i = 0; i < phases.Length; i++) //For each phase
        {
            if (phases[i] == true) //If the phase is active
            {
                phases[i] = false; //Deactivate the previous phase
                phases[i + 1] = true; //Activate the next phase
                break; //Leave the loop
            }
        }
    }

    protected void KillBoss() //Kill the boss
    {
        if (health == 0) //If the boss is dead
        {
            if (deathTimer == 0 && !explosionFlags[0]) //If the timer is at 0
            {
                Instantiate(explosion, new Vector3(transform.position.x + Random.Range(-.75f, .75f), transform.position.y + Random.Range(-.75f, .75f), transform.position.z), Quaternion.identity); //Instantiate the explosion
                explosionFlags[0] = true; //Set the explosion flag to true
            }
            else if ((deathTimer >= .25f && deathTimer <= .5f) && !explosionFlags[1]) //If the timer is between .25 and .5
            {
                Instantiate(explosion, new Vector3(transform.position.x + Random.Range(-.75f, .75f), transform.position.y + Random.Range(-.75f, .75f), transform.position.z), Quaternion.identity); //Instantiate the explosion
                explosionFlags[1] = true; //Set the explosion flag to true
            }
            else if ((deathTimer >= .5f && deathTimer <= .75f) && !explosionFlags[2]) //If the timer is between .5 and .75
            {
                Instantiate(explosion, new Vector3(transform.position.x + Random.Range(-.75f, .75f), transform.position.y + Random.Range(-.75f, .75f), transform.position.z), Quaternion.identity); //Instantiate the explosion
                explosionFlags[2] = true; //Set the explosion flag to true
            }
            else if ((deathTimer >= .75f && deathTimer <= 1f) && !explosionFlags[3]) //If the timer is between .75 and 1
            {
                Instantiate(explosion, new Vector3(transform.position.x + Random.Range(-.75f, .75f), transform.position.y + Random.Range(-.75f, .75f), transform.position.z), Quaternion.identity); //Instantiate the explosion
                explosionFlags[3] = true; //Set the explosion flag to true
            }
            else if ((deathTimer >= 1f && deathTimer <= 1.25f) && !explosionFlags[4]) //If the timer is between 1 and 1.25
            {
                Instantiate(explosion, transform.position, Quaternion.identity); //Instantiate the explosion
                bossSR.enabled = false; //Disable the boss's sprite renderer
                explosionFlags[4] = true; //Set the explosion flag to true
            }
            else if (deathTimer >= 2f) //If the timer is greater than 2
            {
                Destroy(gameObject); //He dead
                SceneManager.LoadScene("MainRoom"); //Load the main room
            }

            deathTimer += Time.deltaTime; //Increment the timer
        }
    }

    protected void Blink() //Make the boss blink while in cooldown
    {
        if (damageCooldown >= 0f) //If the boss is in cooldown
        {
            if (bossSR.color.a == 1) //If the boss's sprite is not transparent
            {
                bossSR.color = new Color(bossSR.color.r, bossSR.color.g, bossSR.color.b, 0); //Make the boss's sprite transparent
            }
            else //If the boss's sprite is transparent
            {
                bossSR.color = new Color(bossSR.color.r, bossSR.color.g, bossSR.color.b, 1); //Make the boss's sprite not transparent
            }
        }
        else //If the boss is not in cooldown
        {
            bossSR.color = new Color(bossSR.color.r, bossSR.color.g, bossSR.color.b, 1); //Make the boss's sprite not transparent
        }

        if (damageCooldown > 0f) //If the cooldown is active
        {
            damageCooldown -= Time.deltaTime; //Decrement the cooldown timer
        }
    }

    /// <summary>
    /// Spawns a magic missile in whatever direction is given to it
    /// </summary>
    protected void SpawnMissile(Vector3 direction, Vector3 initialPos)
    {
        // add this missile to the list
        GameObject newMissile = GameObject.Instantiate(missile, initialPos, Quaternion.identity) as GameObject;

        // assign newMissile's direction using the setter
        newMissile.GetComponent<MagicMissile>().Direction = direction;
    }
}