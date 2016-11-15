using UnityEngine;
using System.Collections;

public class TestEnemy : MonoBehaviour
{
    private Vector3 arcRotation; //How far to move the enemy each frame
    public float angleOfRotation; //How fast to rotate

    void Start() //Use this for initialization
    {
        arcRotation = transform.position - new Vector3(0, 0, 0); //Get the radius of the circle
    }

    void Update() //Update is called once per frame
    {
        arcRotation = Quaternion.AngleAxis(angleOfRotation * Time.deltaTime, Vector3.forward) * arcRotation; //Calculate the arc rotation
        transform.position = new Vector3(0, 0, 0) + arcRotation; //Apply the circle's rotation
    }

    private void OnTriggerEnter2D(Collider2D coll) //When something collides with the player
    {
        if (coll.gameObject.tag == "Player") //If the player is colliding with an enemy
        {
            Player p = coll.gameObject.GetComponent<Player>();
            if(p.DamageCooldown <= 0 )
            {
                p.Health -= 1; //Decrement the player's health
                p.DamageCooldown = 1; //Reset the damage cooldown
            }
        }
        if(coll.gameObject.tag == "weapon") //If the enemy is colliding with the player's weapon.
        {
            Destroy(this.gameObject);
        }
    }
}