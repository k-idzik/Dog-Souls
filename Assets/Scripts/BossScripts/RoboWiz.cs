using UnityEngine;
using System.Collections;
using System.Collections.Generic; //Lists

public class RoboWiz : Boss
{
    private bool[] phases = new bool[4] { false, false, false, true }; //Array of booleans to hold the current state
    [SerializeField] private List<GameObject> shields; //The shields
    [SerializeField] private GameObject magicMissiles; //The boss's magic missiles
    [SerializeField] private float timeBetweenAttacks; //The time between the boss's attacks
    [SerializeField] private float missileAttacks; //The timing for the boss's attacks

    protected override void Update() //Update is called once per frame
    {
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

    private void FirstAttack() //The boss's first attack, sprinkler
    {

    }

    private void SecondAttack() //The boss's second attack, minion spawning
    {

    }

    private void ThirdAttack() //The boss's third attack
    {
        
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