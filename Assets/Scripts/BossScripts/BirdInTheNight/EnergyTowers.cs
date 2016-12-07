using UnityEngine;
using System.Collections;

public class EnergyTowers : MonoBehaviour {

    private SpriteRenderer sprite;
    private Color baseColor;
    private Color hitColor;
    private Color settleColor;
    //private float randomResetTime;
    private double currentTime;
    private double savedTime;
    private float blueInc;
    private float greenInc;

	// Use this for initialization
	void Start () {
        sprite = this.GetComponent<SpriteRenderer>();
        baseColor = new Color(1f, 1f, 1f, 1f);
        hitColor = new Color(0.7f, 0f, 0f, 1f);
        settleColor = new Color(0.7f, 0.3f, 0.3f);
        savedTime = 0;
        currentTime = 0;
        blueInc = 0.01f;
        greenInc = 0.01f;
    }
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;
        if (currentTime >= savedTime + Random.Range(7.0f, 15.0f))
        {
            sprite.color = baseColor;
        }
        if (sprite.color != baseColor && sprite.color != settleColor)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g + greenInc, sprite.color.b + blueInc);
        }
	}

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "weapon" && sprite.color != hitColor)
        {
            sprite.color = hitColor;
            savedTime = currentTime;
        }
        if (coll.gameObject.tag == "weapon")
        {
            savedTime = currentTime;
        }
    }

    public bool isHit()
    {
        if (sprite.color != baseColor)
        {
            return true;
        }
        return false;
    }
}
