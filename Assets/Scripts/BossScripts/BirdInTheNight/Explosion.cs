using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    [SerializeField]
    private float startRadius;
    [SerializeField]
    private float addedBloatRadius;
    [SerializeField] private float timeActive;
    private float currentTimeActive;
    [SerializeField]
    private GameObject itself;

	// Use this for initialization
	void Start () {
        timeActive = 1.5f;
        currentTimeActive = timeActive;
        transform.localScale = new Vector2(startRadius, startRadius);
	}
	
	// Update is called once per frame
	void Update () {
        Bloat();
        currentTimeActive -= Time.deltaTime;
        if (currentTimeActive <= 0)
        {
            Destroy(itself);
        }
	}

    private void Bloat()
    {
        float lifePercentLeft = currentTimeActive / timeActive;
        transform.localScale = new Vector2(startRadius + addedBloatRadius * (1 - lifePercentLeft), startRadius + addedBloatRadius * (1 - lifePercentLeft));
    }
}
