using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour
{
    [SerializeField] private float angleOfRotation; //How fast to rotate

    void Update() //Update is called once per frame
    {
        BeamAttack(); //Use the beam attack
    }

    private void BeamAttack() //The wizard's beam attack
    {
        StartCoroutine(Cooldown(5)); //Wait before attacking
        transform.RotateAround(transform.position, new Vector3(0, 0, 1), angleOfRotation); //Rotate the beam
    }

    private IEnumerator Cooldown(float time) //Co-routine timer
    {
        yield return new WaitForSeconds(time); //Timer
    }

    private void OnTriggerEnter2D(Collider2D coll) //When something collides with the beam
    {
        //if (coll.gameObject.tag == "Player") //If the player is colliding with an enemy
        //{
        //    Player p = coll.gameObject.GetComponent<Player>();
        //    if(p.DamageCooldown <= 0 )
        //    {
        //        p.Health -= 1; //Decrement the player's health
        //        p.DamageCooldown = 1; //Reset the damage cooldown
        //    }
        //}
        if(coll.gameObject.tag == "weapon") //If the enemy is colliding with the player's weapon.
        {
            Destroy(this.gameObject);
        }
    }
}