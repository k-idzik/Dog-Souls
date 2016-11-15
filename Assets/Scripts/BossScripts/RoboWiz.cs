using UnityEngine;
using System.Collections;

public class RoboWiz : MonoBehaviour
{
    private enum Phases { PHASE1, PHASE2, PHASE3, PHASE4 }; //Finite state machine
    [SerializeField] private int health; //The player's health
    [SerializeField] private GameObject[] shields; //The shields
    [SerializeField] private GameObject magicMissiles; //The boss's magic missiles
    [SerializeField] private float timeBetweenAttacks; //The time between the boss's attacks
    [SerializeField] private float timeAttacks; //The timeing for the boss's attacks

    void Update() //Update is called once per frame
    {
        if (health % 25 == 0.0f) //If the boss's health is down by an even quarter
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

    }
}