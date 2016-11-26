using UnityEngine;
using System.Collections;

public class CollisionTest : MonoBehaviour
{	
	void Start() //Use this for initialization
    {
	
	}
	
	void Update() //Update is called once per frame
    {
	
	}

    void OnTriggerEnter2D(Collider2D coll) //If something triggers this object
    {
        Debug.Log("Trigger: " + coll.gameObject.name); //Log the trigger
    }

    void OnCollisionEnter2D(Collision2D coll) //If something collides with this object
    {
        Debug.Log("Collider: " + coll.gameObject.name); //Log the collision
    }
}