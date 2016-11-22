using UnityEngine;
using System.Collections;
using System.Collections.Generic; //Lists

public class RoboWiz : Boss
{
    private bool[] phases = new bool[4] { false, false, false, true }; //Array of booleans to hold the current state
    [SerializeField] private List<GameObject> shields; //The shields
    [SerializeField] private GameObject magicMissile; //The boss's magic missiles
    [SerializeField] private GameObject beam; //The boss's SPECIAL BEAM CANNON
    [SerializeField] private GameObject minion;
    [SerializeField] private float timeBetweenAttacks; //The time between the boss's attacks
    [SerializeField] private float missileAttacks; //The timing for the boss's attacks
    [SerializeField] private float numMissiles; // number of missiles
    [SerializeField] private float missileCooldown; // time between missile barrages
    [SerializeField] private float minionCooldown;
    [SerializeField] private float beamCooldown; //Time between beams
    private float missileTimer; // to count down between barrages of missiles
    private float minionTimer;
    private float beamTimer; //The beam timer
    private float missileAngle; // current angle of the missile, should always start at 0
    private float angleBetweenMissiles; // angle between each missile, should be 360 / numMissiles
    private float numSpawned; // counter for the number of missiles spawned in each attack
    private List<GameObject> missiles; // for keeping track of missiles

    protected override void Start()
    {
        base.Start();

        // assign missileAngle to 0
        missileAngle = 0;

        // calculate angle between missiles spawning
        angleBetweenMissiles = 360 / numMissiles;

        missileTimer = missileCooldown;

        minionTimer = minionCooldown;

        beamTimer = beamCooldown; //Set the beam timer equal to the beam cooldown
    }

    protected override void Update() //Update is called once per frame
    {
        // FIRST PHASE
        if (phases[3] == true)
        {
            if (missileTimer <= 0)
            {
                // call first attack if timer is up
                FirstAttack();

                // reset timer
                missileTimer = missileCooldown;
            }
            
            // decrement the timer
            missileTimer -= Time.deltaTime;

            //Debug.Log(missileTimer);
        }

        // SECOND PHASE
        if(phases[2] == true)
        {
            if (missileTimer <= 0)
            {
                // call first attack if timer is up
                FirstAttack();

                // reset timer
                missileTimer = missileCooldown;
            }

            // decrement the timer
            missileTimer -= Time.deltaTime;

            if (minionTimer <= 0)
            {
                // call second attack if timer is up
                SecondAttack();

                // reset timer
                minionTimer = minionCooldown;
            }

            // decrement timer
            minionTimer -= Time.deltaTime;
        }

        // THIRD PHASE
        if (phases[1])
        {
            if (missileTimer <= 0)
            {
                // call first attack if timer is up
                FirstAttack();

                // reset timer
                missileTimer = missileCooldown;
            }

            // decrement the timer
            missileTimer -= Time.deltaTime;

            if (beamTimer <= 0)
            {
                ThirdAttack(); //IT'S BEAM TIME

                // reset timer
                beamTimer = beamCooldown;
            }

            // decrement the timer
            beamTimer -= Time.deltaTime;

            //if (minionTimer <= 0)
            //{
            //    // call second attack if timer is up
            //    SecondAttack();
            //
            //    // reset timer
            //    minionTimer = minionCooldown;
            //}
            //
            //// decrement timer
            //minionTimer -= Time.deltaTime;
        }

        if (health <= 75 && health > 0 && !phases[health / 25]) //If the boss's health is down by an even quarter
        {
            PhaseChange(); //Change the phase
        }

        if(health == 0) //If the boss is dead
        {
            Destroy(gameObject); //He dead
        }

        base.Update(); //Call the base update method
	}

    /// <summary>
    /// will be a "sprinkler" attack, using a timer to determine when each missile
    /// should be spawned
    /// </summary>
    private void FirstAttack() //The boss's first attack, sprinkler
    {
        for (int i = 0; i < numMissiles; i++)
        {
            // spawn a missile at whatever the current missileAngle is
            SpawnMissile(Quaternion.AngleAxis(missileAngle, transform.position) * transform.up);

            // calculate angle of next missile
            missileAngle += angleBetweenMissiles;
        }
    }

    /// <summary>
    /// Spawns a magic missile in whatever direction is given to it
    /// </summary>
    private void SpawnMissile(Vector3 direction)
    {
        // add this missile to the list
        GameObject newMissile =  GameObject.Instantiate(magicMissile, (transform.position + direction) * 3, Quaternion.identity) as GameObject;

        // assign newMissile's direction using the setter
        newMissile.GetComponent<MagicMissile>().Direction = direction;
    }

    private void SecondAttack() //The boss's second attack, minion spawning
    {
        float randx = Random.Range(GameObject.FindGameObjectWithTag("Player").transform.position.x - 5, GameObject.FindGameObjectWithTag("Player").transform.position.x + 5);
        float randy = Random.Range(GameObject.FindGameObjectWithTag("Player").transform.position.y - 5, GameObject.FindGameObjectWithTag("Player").transform.position.y + 5);

        // instantiate new enemy at random location
        Instantiate(minion, new Vector3(randx, randy, 0), Quaternion.identity);

    }

    private void ThirdAttack() //The boss's third attack
    {
        Instantiate(beam, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, Random.Range(0, 4) * 90)); //Instantiate a beam object
    }

    private void PhaseChange() //When the boss should change phases
    {
        phases[health / 25] = true; //Activate the next phase
        phases[(health / 25) + 1] = false; //Deactivate the previous phase

        if (shields.Count > 0) //If there are still shields left
        {
            int shieldNumber = Random.Range(0, shields.Count); //Get the shield to destroy
            Destroy(shields[shieldNumber]); //Destory the shield
            shields.RemoveAt(shieldNumber); //Remove the shield from the array
        }
    }

    protected override void OnTriggerStay2D(Collider2D coll) //If something collides with the boss
    {
        if (coll.gameObject.tag == "weapon" && damageCooldown <= 0f) //If the boss collides with the player's weapon
        {
            health -= 10; //Decrement health
            damageCooldown = 1; //Reset the damage cooldown
        }
    }
}