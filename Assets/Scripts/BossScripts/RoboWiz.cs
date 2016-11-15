using UnityEngine;
using System.Collections;
using System.Collections.Generic; //Lists

public class RoboWiz : MonoBehaviour
{
    private bool[] phases = new bool[4] { false, false, false, true }; //Array of booleans to hold the current state
    [SerializeField] private int health; //The player's health
    [SerializeField] private List<GameObject> shields; //The shields
    [SerializeField] private GameObject magicMissiles; //The boss's magic missiles
    [SerializeField] private float timeBetweenAttacks; //The time between the boss's attacks
    [SerializeField] private float missileAttacks; //The timing for the boss's attacks

    void Update() //Update is called once per frame
    {
        if (!phases[0] && !phases[(health - 25) / 25]) //If the boss's health is down by an even quarter
        {
            PhaseChange(); //Change the phase
        }

        if(health == 0) //If the boss is dead
        {
            Destroy(gameObject); //He dead
        }
	}

    IEnumerator Cooldown(float time) //Co-routine timer
    {
        yield return new WaitForSeconds(time); //Timer
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
        phases[(health - 25) / 25] = true; //Activate the next phase
        phases[((health - 25) / 25) + 1] = false; //Deactivate the previous phase

        if (shields.Count > 0) //If there are still shields left
        {
            int shieldNumber = Random.Range(0, shields.Count); //Get the shield to destroy
            Destroy(shields[shieldNumber]); //Destory the shield
            shields.RemoveAt(shieldNumber); //Remove the shield from the array
        }
    }
}