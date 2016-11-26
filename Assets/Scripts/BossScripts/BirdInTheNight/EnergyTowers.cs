using UnityEngine;
using System.Collections;

public class EnergyTowers : MonoBehaviour {

    public SpriteRenderer sprite;
    public Color baseColor;
    public Color hitColor;
    public double currentTime;
    public double savedTime;

	// Use this for initialization
	void Start () {
        sprite = this.GetComponent<SpriteRenderer>();
        baseColor = new Color(1f, 1f, 1f, 1f);
        hitColor = new Color(0.7f, 0f, 0f, 1f);
        savedTime = 0;
        currentTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;
        if (currentTime >= savedTime + 10)
        {
            sprite.color = baseColor;
        }
	}

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "weapon" && sprite.color == baseColor)
        {
            sprite.color = hitColor;
            savedTime = currentTime;
        }
    }

    public bool isHit()
    {
        if (sprite.color == hitColor)
        {
            return true;
        }
        return false;
    }
}
