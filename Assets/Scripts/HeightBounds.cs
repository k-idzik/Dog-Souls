using UnityEngine;
using System.Collections;

public class HeightBounds : MonoBehaviour {

    public EdgeCollider2D[] edgeColliders;
    public GameObject player;

	// Use this for initialization
	void Start () {
        edgeColliders = new EdgeCollider2D[10];
        edgeColliders = this.GetComponents<EdgeCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	    foreach (EdgeCollider2D edge in edgeColliders)
        {
            SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
            SpriteRenderer thisSprite = this.GetComponent<SpriteRenderer>();
            if (playerSprite.sortingOrder == thisSprite.sortingOrder)
            {
                edge.isTrigger = false;
            }
            else
            {
                edge.isTrigger = true;
            }
        }
	}
}
