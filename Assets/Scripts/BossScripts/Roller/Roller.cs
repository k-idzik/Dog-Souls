using UnityEngine;
using System.Collections;

public class Roller : Boss
{
    private GameObject []pylons; //The moving pylons

    new void Start() //Use this for initialization
    {
        base.Start(); //Call the base start method

        pylons = GameObject.FindGameObjectsWithTag("barrier"); //Find all the pylons
    }

    protected override void Update() //Update is called once per frame
    {
        base.Update(); //Call the base update method
    }
}