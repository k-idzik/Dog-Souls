using UnityEngine;
using System.Collections;

public class Electric : MonoBehaviour {

    public EnergyTowers tower1;
    public EnergyTowers tower2;
    public SpriteRenderer sprite;

    // Use this for initialization
    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tower1.isHit() && tower2.isHit())
        {
            sprite.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            sprite.GetComponent<Renderer>().enabled = false;
        }
    }
}
