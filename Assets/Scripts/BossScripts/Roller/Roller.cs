using UnityEngine;
using System.Collections;

public class Roller : Boss
{
    new void Start() //Use this for initialization
    {
        base.Start(); //Call the base start method
    }

    protected override void Update() //Update is called once per frame
    {
        base.Update(); //Call the base update method
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
