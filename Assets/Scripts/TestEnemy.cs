using UnityEngine;
using System.Collections;

public class TestEnemy : MonoBehaviour //KI
{
    private Vector3 arcRotation; //How far to move the enemy each frame
    [SerializeField] float angleOfRotation; //How fast to rotate

    void Start() //Use this for initialization
    {
        arcRotation = transform.position - new Vector3(0, 0, 0); //Get the radius of the circle
    }

    void Update() //Update is called once per frame
    {
        arcRotation = Quaternion.AngleAxis(angleOfRotation * Time.deltaTime, Vector3.forward) * arcRotation; //Calculate the arc rotation
        transform.position = new Vector3(0, 0, 0) + arcRotation; //Apply the circle's rotation
    }
}