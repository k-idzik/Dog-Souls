using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour
{
    [SerializeField]
    private float angleOfRotation; //How fast to rotate
    private bool canRotate; //If the beam can rotate
    int randomDirection; //Which direction the beam should rotate

    void Start() //Use this for initialization
    {
        canRotate = false; //The beam cannot rotate
        randomDirection = Random.Range(0, 2); //Choose which direction to shoot the beam
    }

    void Update() //Update is called once per frame
    {
        BeamAttack(); //Use the beam attack
    }

    private void BeamAttack() //The wizard's beam attack
    {
        if (!canRotate) //If the beam cannot rotate
        {
            StartCoroutine(Cooldown(1)); //Wait before attacking
        }
        else //If the beam can rotate
        {
            if (randomDirection == 0) //If the random number generated is 0
            {
                transform.RotateAround(transform.position, new Vector3(0, 0, 1), angleOfRotation); //Rotate the beam counterclockwise
            }
            if (randomDirection == 1) //If the random number generated is 1
            {
                transform.RotateAround(transform.position, new Vector3(0, 0, 1), -angleOfRotation); //Rotate the beam clockwise
            }
        }
    }

    private IEnumerator Cooldown(float time) //Co-routine timer
    {
        yield return new WaitForSeconds(time); //Timer
        canRotate = true; //Set canRotate as true
    }

    private void OnTriggerEnter2D(Collider2D coll) //When something collides with the beam
    {
        if (coll.transform.tag == "barrier") //If the beam is colliding with a barrier
        {
            Destroy(this.gameObject); //Destroy the beam
        }
    }
}