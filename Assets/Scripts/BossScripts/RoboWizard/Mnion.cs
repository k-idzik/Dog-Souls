using UnityEngine;
using System.Collections;

public class Mnion : MonoBehaviour {

    //-----ATTRIBUTES-----
    [SerializeField]
    protected float knockbackScale;
    public float moveSpeed;
    public int health;
    public int damage;
    protected Rigidbody2D rb;
    public float maxSpeed;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }

        Move();
	}

    /// <summary>
    /// responsible for moving the enemy
    /// </summary>
    public void Move()
    {
        Vector3 unitOffset = Seek();
        transform.up = unitOffset;
        Vector3 acceleration = unitOffset * moveSpeed;
        rb.AddForce(acceleration);
        float velX = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
        float velY = Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed);

        rb.velocity = new Vector3(velX, velY, 0);
    }

    /// <summary>
    /// Method resposible for seeking the player
    /// </summary>
    /// <returns>offset vector</returns>
    public Vector3 Seek()
    {
        Vector3 offset = GameObject.FindGameObjectWithTag("Player").transform.position - this.transform.position;
        Vector3 unitOffset = offset.normalized;
        return unitOffset;
    }

    /// <summary>
    /// to check if the enemy is colliding with a player
    /// </summary>
    void OnCollisionEnter2D(Collision2D coll)
    {
        //if (coll.transform.tag == "Player")
        //{
        // knockback along inverted up vector
        rb.AddForce(-1 * transform.up * knockbackScale);

        //}
    }

    private void OnTriggerEnter2D(Collider2D coll) //When something collides with the player
    {
        if(coll.gameObject.tag == "weapon") //If the enemy is colliding with the player's weapon.
        {
            health--;
        }
    }
}
