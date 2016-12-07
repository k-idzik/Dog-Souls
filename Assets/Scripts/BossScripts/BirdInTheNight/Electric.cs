using UnityEngine;
using System.Collections;

public class Electric : MonoBehaviour {

    [SerializeField] private EnergyTowers tower1;
    [SerializeField] private EnergyTowers tower2;
    SpriteRenderer sprite;

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
