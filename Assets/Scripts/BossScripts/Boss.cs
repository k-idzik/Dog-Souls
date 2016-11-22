using UnityEngine;
using System.Collections;

public abstract class Boss : MonoBehaviour
{
    [SerializeField] protected int health; //The player's health
    private SpriteRenderer bossSR; //The boss's sprite renderer
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
    }

    protected IEnumerator Cooldown(float time) //Co-routine timer
    {
        yield return new WaitForSeconds(time); //Timer
    }

    protected abstract void OnTriggerStay2D(Collider2D coll); //If something collides with the boss

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