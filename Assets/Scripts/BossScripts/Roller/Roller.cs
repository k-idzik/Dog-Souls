using UnityEngine;
using System.Collections;
using System.Collections.Generic; //Lists

public class Roller : Boss
{
    private List<GameObject> pylons; //The moving pylons

    new void Start() //Use this for initialization
    {
        base.Start(); //Call the base start method

        GameObject []objectsInScene = FindObjectsOfType<GameObject>(); //Get all gameobjects in the scene
        Debug.Log(objectsInScene.Length);
        for (int i = 0; i < objectsInScene.Length; i++) //For all the objects in the scene
        {
            if (objectsInScene[i].tag == "barrier") //If the object is a pylon
            {
                Debug.Log(objectsInScene[i].name);
                pylons.Add(objectsInScene[i]); //Add the pylon to the list of pylons
            }
        }
    }

    protected override void Update() //Update is called once per frame
    {
        base.Update(); //Call the base update method
        Debug.Log(pylons.Count);
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
