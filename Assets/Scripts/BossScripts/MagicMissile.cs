using UnityEngine;
using System.Collections;

public class MagicMissile : MonoBehaviour {

    [SerializeField]
    private float speed; // the speed of the missile
    private Vector3 direction; // direction of the missile
    [SerializeField]
    float despawnTimer; // the time the missile will exist unless it hits something
    private Rigidbody2D rb;

    // setter for direction
    public Vector3 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        //rotates the sprite in the direction it's facing
        if(direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -Vector3.Angle(direction, new Vector3(0, -1, 0)));
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, Vector3.Angle(direction, new Vector3(0, -1, 0)));
        }
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = direction * speed;
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag != "Player" && coll.transform.tag != "enemy" && coll.transform.tag != "boss")
        {
            Destroy(this.gameObject);
        }
    }
}
