using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //Load scenes

public class LoadScene : MonoBehaviour
{
    private static string[] canLoadScene = new string[3] { "BossRoom0", "BossRoom1", "BossRoom2" }; //If this collider can load the scene

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainRoom" && canLoadScene[0] == "" && canLoadScene[1] == "" && canLoadScene[2] == "") //If the game has been won
        {
            SceneManager.LoadScene("Title"); //Load the scene
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        for (int i = 0; i < canLoadScene.Length; i++) //Loop through the list of scenes
        {
            if (canLoadScene[i].Contains(gameObject.name)) //If the scene can be loaded
            {
                canLoadScene[i] = ""; //Set canLoadScene to false
                SceneManager.LoadScene(gameObject.name); //Load the scene
            }
        }
    }
}
