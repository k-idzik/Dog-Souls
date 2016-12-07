using UnityEngine;
using System.Collections;

public abstract class Boss : MonoBehaviour
{
    protected bool[] phases = new bool[4] { true, false, false, false }; //Array of booleans to hold the current state
    [SerializeField] protected int health; //The player's health
    protected SpriteRenderer bossSR; //The boss's sprite renderer
    protected float damageCooldown; //The time between when the player can take damage

    public int Health //The boss's health
    {
        get
        {
            return health; //Return the boss's health
        }
    }

    protected virtual void Start() //Use this for initialization
    {
        bossSR = GetComponent<SpriteRenderer>(); //Get the boss's sprite renderer
        damageCooldown = -1; //Set the damageCooldown
    }

    protected virtual void Update() //Update is called once per frame
    {
        Blink(); //Blink
        
        if ((health * 10) <= 75 && health > 0 && !phases[(100 - (health * 10)) / 25]) //If the boss's health is down by an even quarter
        {
            PhaseChange(); //Change the phase
        }
    }

    protected IEnumerator Cooldown(float time) //Co-routine timer
    {
        yield return new WaitForSeconds(time); //Timer
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

    private void Blink() //Make the boss blink while in cooldown
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
}