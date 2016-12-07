using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //Load scenes

public class LoadScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        SceneManager.LoadScene(gameObject.name); //Load the scene
    }
}
