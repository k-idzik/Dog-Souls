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
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = direction * speed;
	}
}
