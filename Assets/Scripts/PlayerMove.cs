using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public Animator movementAnim;
    public Vector3 velocity;
    public float maxSpeed;
    public Vector3 acceleration;

	// Use this for initialization
	void Start () {
        maxSpeed = 0.1f;
        movementAnim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("w"))
        {
            movementAnim.Play("DogAway");
            if (velocity.y <=0)
            {
                velocity.x = 0;
                velocity.y = 0;
            }
            acceleration = new Vector3(0, 0.03f);
        }
        else if (Input.GetKey("a"))
        {
            movementAnim.Play("DogLeft");
            if (velocity.x >= 0)
            {
                velocity.x = 0;
                velocity.y = 0;
            }
            acceleration = new Vector3(-0.03f, 0);
        }
        else if (Input.GetKey("s"))
        {
            movementAnim.Play("DogTowards");
            if (velocity.y >= 0)
            {
                velocity.x = 0;
                velocity.y = 0;
            }
            acceleration = new Vector3(0, -0.03f);
        }
        else if (Input.GetKey("d"))
        {
            movementAnim.Play("DogRight");
            if (velocity.x <= 0 )
            {
                velocity.x = 0;
                velocity.y = 0;
            }
            acceleration = new Vector3(0.03f, 0);
        }
        else
        {
            if (velocity.x > 0)
            {
                acceleration += new Vector3(-0.003f, 0);
            }
            if (velocity.x < 0)
            {
                acceleration += new Vector3(0.003f, 0);
            }
            if (velocity.y > 0)
            {
                acceleration += new Vector3(0, -0.003f);
            }
            if (velocity.y < 0)
            {
                acceleration += new Vector3(0, 0.003f);
            }
        }

        if (velocity.x*velocity.x + velocity.y*velocity.y + velocity.z*velocity.z >= maxSpeed*maxSpeed)
        {
            velocity.Normalize();
            velocity *= maxSpeed;
        }

        velocity += acceleration;
        transform.position += velocity;

        acceleration = new Vector3(0, 0, 0);
    }
}
