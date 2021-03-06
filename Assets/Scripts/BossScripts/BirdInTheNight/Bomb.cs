﻿using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    [SerializeField]
    private float fuse; // how long until the bomb explodes
    [SerializeField]
    private int damage; // damage the bomb does
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject itself;

    private SpriteRenderer spriteRender;
    private float maxFuse;

	// Use this for initialization
	void Start () {
        spriteRender = this.GetComponent<SpriteRenderer>();
        int random = Random.Range(0, 3);
        switch(random)
        {
            case 0:
                fuse = Random.Range(2.0f, 30.0f);
                break;

            case 1:
                fuse = Random.Range(2.0f, 20.0f);
                break;

            case 2:
                fuse = Random.Range(2.0f, 10.0f);
                break;
        }
        fuse = Random.Range(2.0f, 10.0f);
        maxFuse = fuse;
	}
	
	// Update is called once per frame
	void Update () {
        fuse -= Time.deltaTime;
        spriteRender.color = Color.Lerp(Color.white, Color.black, 1 - (fuse/maxFuse));
        if (fuse <= 0)
        {
            Explode();
        }
	}

    /// <summary>
    /// bomb go boom
    /// </summary>
    private void Explode ()
    {
        GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(itself);
    }
}
