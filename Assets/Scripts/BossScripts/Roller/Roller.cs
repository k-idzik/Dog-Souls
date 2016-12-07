using UnityEngine;
using System.Collections;

public class Roller : Boss
{
    [SerializeField] private Sprite vulnerableSprite; //The roller's vulnerable sprite
    private GameObject []pylons; //The moving pylons
    private float timer; //Timer for battle sequencing
    private int maxPylons; //The maximum number of pylons
    private int[] selectedPylons; //The pylons to move
    private Vector2[] startPosition; //The start position of each pylon
    private Vector2[] endPosition; //The end position of each pylon
    private bool stop; //If the roller should stop

    new void Start() //Use this for initialization
    {
        base.Start(); //Call the base start method

        pylons = GameObject.FindGameObjectsWithTag("barrier"); //Find all the pylons

        timer = 0; //Set the timer to 0

        stop = false; //The boss should not stop
    }

    protected override void Update() //Update is called once per frame
    {
        base.Update(); //Call the base update method

        if (timer == 0) //To select pylons
        {
            SelectPylons(); //Select the pylons
        }
        else if (timer >= 5 && timer < 10) //To move pylons
        {
            MovePylons(); //Move the pylons
        }
        else if (!stop && timer >= 10 && timer < 20) //Make the boss attack
        {
            Attack(); //Attack
        }
        else if (timer >= 20 && timer < 25) //To move pylons
        {
            ResetPylons(); //Reset the pylons
        }

        timer += Time.deltaTime; //Increment the timer

        if (timer >= 25) //To reset the cycle
        {
            timer = 0; //Reset the timer
        }
    }

    private void SelectPylons() //Select the pylons
    {
        int numberPylons = 0; //Keep track of the number of pylons to move
        maxPylons = Random.Range(4, 12); //The maximum number of pylons
        selectedPylons = new int[maxPylons]; //The pylons to move
        startPosition = new Vector2[maxPylons]; //The start position of each pylon
        endPosition = new Vector2[maxPylons]; //The end position of each pylon

        while (numberPylons < maxPylons) //While not all of the pylons are chosen
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

                startPosition[numberPylons] = new Vector3(pylons[randomPylonNumber].transform.position.x, 0, 0); //Add the start position of the pylon to the array

                if (pylons[randomPylonNumber].name.Contains("R")) //If this is a right side pylon
                {
                    endPosition[numberPylons] = new Vector3(pylons[randomPylonNumber].transform.position.x - (Random.Range(1, 2) * 2.5f), 0, 0); //Add the end position to the array
                }
                else if (pylons[randomPylonNumber].name.Contains("L")) //If this is a left side pylon
                {
                    endPosition[numberPylons] = new Vector3(pylons[randomPylonNumber].transform.position.x + (Random.Range(1, 2) * 2.5f), 0, 0); //Add the end position to the array
                }

                numberPylons++; //Increment selectedPylons
            }
        }
    }

    private void MovePylons() //Move the pylons
    {
        for (int i = 0; i < selectedPylons.Length; i++) //For all of the pylons to move
        {
            if (pylons[selectedPylons[i]].name.Contains("R") && pylons[selectedPylons[i]].transform.position.x > endPosition[i].x) //If this is a right side pylon
            {
                pylons[selectedPylons[i]].transform.position -= (Vector3)endPosition[i] * Time.deltaTime * 3; //Move the pylon
            }
            else if (pylons[selectedPylons[i]].name.Contains("L") && pylons[selectedPylons[i]].transform.position.x < endPosition[i].x) //If this is a left side pylon
            {
                pylons[selectedPylons[i]].transform.position -= (Vector3)endPosition[i] * Time.deltaTime * 3; //Move the pylon
            }
        }
    }

    private void ResetPylons() //Move the pylons
    {
        for (int i = 0; i < selectedPylons.Length; i++) //For all of the pylons to move
        {
            if (pylons[selectedPylons[i]].name.Contains("R") && pylons[selectedPylons[i]].transform.position.x < startPosition[i].x) //If this is a right side pylon
            {
                pylons[selectedPylons[i]].transform.position += (Vector3)startPosition[i] * Time.deltaTime * .5f; //Move the pylon
            }
            else if (pylons[selectedPylons[i]].name.Contains("L") && pylons[selectedPylons[i]].transform.position.x > startPosition[i].x) //If this is a left side pylon
            {
                pylons[selectedPylons[i]].transform.position += (Vector3)startPosition[i] * Time.deltaTime * .5f; //Move the pylon
            }
        }
    }

    private void Attack() //Make the boss attack
    {
        bossSR.color = Color.red; //Change the color of the boss to indicate anger
        transform.position += new Vector3(0, -8.5f, 0) * Time.deltaTime; //Move the boss to attack
        bossSR.color = Color.white; //Change the color of the boss to indicate anger
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "BossRoom2Sprite") //If colliding with the room
        {
            stop = true; //Make the roller stop
            bossSR.sprite = vulnerableSprite; //Switch the roller's sprite
        }
    }
}