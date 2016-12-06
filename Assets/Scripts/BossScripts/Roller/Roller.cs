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

        MovePylons(); //Move the pylons
    }

    private void MovePylons() //Move the pylons
    {
        int numberPylons = 0; //Keep track of the number of pylons to move
        int maxPylons = 12; //The maximum number of pylons
        int []selectedPylons = new int[maxPylons]; //The pylons to move

        while(numberPylons < maxPylons) //While not all of the pylons are chosen
        {
            int randomPylonNumber = Random.Range(0, 16); //Choose a random pylon
            bool hasPylonBeenSelected = false; //If the pylon has already been selected

            for (int i = 0; i < numberPylons; i++) //Check all of the current pylons
            {
                if (selectedPylons[i] == randomPylonNumber) //Check if the pylon is already in the array
                {
                    hasPylonBeenSelected = true; //THe pylon has already been selected
                }
            }

            if (!hasPylonBeenSelected) //If the pylon has not already been selected
            {
                selectedPylons[numberPylons] = randomPylonNumber; //Add the new pylon to the array
                numberPylons++; //Increment selectedPylons
            }
        }

        for (int i = 0; i < selectedPylons.Length; i++) //For all of the pylons to move
        {
            if (pylons[selectedPylons[i]].name.Contains("R")) //If this is a right side pylon
            {
                pylons[selectedPylons[i]].transform.position = new Vector3(pylons[selectedPylons[i]].transform.position.x - (Random.Range(1, 3) * 2.55f), pylons[selectedPylons[i]].transform.position.y, pylons[selectedPylons[i]].transform.position.z); //Move the pylon
            }
            else //If this is a left side pylon
            {
                pylons[selectedPylons[i]].transform.position = new Vector3(pylons[selectedPylons[i]].transform.position.x + (Random.Range(1, 3) * 2.55f), pylons[selectedPylons[i]].transform.position.y, pylons[selectedPylons[i]].transform.position.z); //Move the pylon
            }
        }
    }
}