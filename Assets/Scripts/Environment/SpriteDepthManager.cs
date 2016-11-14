using UnityEngine;
using System.Collections;

public class SpriteDepthManager : MonoBehaviour
{
    [SerializeField] private GameObject player; //The player
    [SerializeField] private float yPositionModifier; //Modify the Y position for order sorting
    private SpriteRenderer sRenderer; //This object's sprite renderer

	void Start() //Use this for initialization
    {
        sRenderer = GetComponent<SpriteRenderer>(); //Get this object's sprite renderer
	}

    void Update() //Update is called once per frame
    {
        if (player.transform.position.y > (transform.position.y - Mathf.Abs(yPositionModifier))) //If the player is above this object
        {
            sRenderer.sortingLayerName = "Environment"; //Move the sprite to the environment layer
        }
        else //If the player is behind this object
        {
            sRenderer.sortingLayerName = "Default"; //Move the sprite to the default layer
        }
    }
}
