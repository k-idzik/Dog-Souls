using UnityEngine;
using System.Collections;

public class ExplosionAnimationScript : MonoBehaviour {

    Collider2D col;

    void Start()
    {
        col = this.GetComponent<Collider2D>();
    }

	void Destroy()
    {
        Destroy(this.gameObject);
    }

    void ColliderActive()
    {
        col.enabled = true;
    }
    void ColliderInactive()
    {
        col.enabled = false;
    }
}
