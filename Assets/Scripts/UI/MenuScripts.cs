using UnityEngine;
using System; //Exceptions
using System.Collections;
using UnityEngine.SceneManagement; //Load scenes
using UnityEngine.EventSystems; //Event systems

public class MenuScripts : MonoBehaviour
{
    [SerializeField] private Canvas uIOverlay; //The UI overlay
    //[SerializeField] private UnityStandardAssets._2D.PlatformerCharacter2D playerControlScript; //The player's control script 
    [SerializeField] private Player player; //The player script
    [SerializeField] private Canvas pauseMenu; //The pause menu UI
    [SerializeField] private Canvas keyboardControlMenu; //The keyboard controls menu UI
    [SerializeField] private Canvas controllerControlMenu; //The controller controls menu UI
    private UnityEngine.UI.Image[] uIImages; //The UI images for the player's health
    private UnityEngine.UI.Text messageText; //The UI text messages
    private float messageTimer; //How long to display messages for
    private GameObject[] bossSearch; //The objects stored when looking for the boss
    private GameObject boss; //The boss
    private RoboWiz roboWizScript; //The robo wiz's script
    private UnityEngine.UI.Text bossText; //The UI boss text
    private UnityEngine.UI.Image bossHealthBar; //The UI boss healthbar
    private int bossHealth; //The boss's health
    //private UnityEngine.UI.Text qText; //The UI text for weapon changing
    //private UnityEngine.UI.Text eText; //The UI text for weapon changing
    //private UnityEngine.UI.Text lBText; //The UI text for weapon changing
    //private UnityEngine.UI.Text rBText; //The UI text for weapon changing
    //private bool controllerConnected; //If a controller is connected

    //public bool ControllerConnected //ControllerConnected property
    //{
    //    get
    //    {
    //        return controllerConnected; //Return controllerConnected
    //    }
    //}

    void Start() //Use this for initilaization
    {
        uIImages = uIOverlay.GetComponentsInChildren<UnityEngine.UI.Image>(); //Get the images from the children

        for (int i = 0; i < uIImages.Length; i++) //For each UI text element
        {
            if (uIImages[i].name == "BossHealth") //If the current gameobject is the boss
            {
                bossHealthBar = uIImages[i]; //Set the boss health bar
                break; //Break out of the loop
            }
        }

        UnityEngine.UI.Text[] uIText = uIOverlay.GetComponentsInChildren<UnityEngine.UI.Text>(); //Temporarily store all of the UI text
        for (int i = 0; i < uIText.Length; i++) //For each UI text element
        {
        //    if (uIText[i].name == "Q") //If the Q text is found
        //    {
        //        qText = uIText[i]; //Save the Q text
        //    }
        //    else if (uIText[i].name == "E") //If the E text is found
        //    {
        //        eText = uIText[i]; //Save the E text
        //    }
        //    else if (uIText[i].name == "LB") //If the LB text is found
        //    {
        //        lBText = uIText[i]; //Save the LB text
        //    }
        //    else if (uIText[i].name == "RB") //If the RB text is found
        //    {
        //        rBText = uIText[i]; //Save the RB text
        //    }
            if (uIText[i].name == "Message") //If the message text is found
            {
                messageText = uIText[i]; //Save the message text
                messageTimer = 0; //Initialize the message timer
            }
            if (uIText[i].name == "BossText") //If the boss text is found
            {
                bossText = uIText[i]; //Save the boss text
            }
        }

        bossSearch = FindObjectsOfType<GameObject>(); //Get gameobjects from the scene

        for (int i = 0; i < bossSearch.Length; i++) //For each gameobject in the scene
        {
            boss = bossSearch[i]; //Save the boss

            if (bossSearch[i].tag == "boss") //If the current gameobject is the boss
            {
                switch(bossSearch[i].name) //Switch based on the boss's name
                {
                    case "RoboWiz": //If the boss's name is RoboWiz
                        roboWizScript = bossSearch[i].GetComponent<RoboWiz>(); //Set the boss
                        bossHealth = roboWizScript.Health; //Set the boss's health
                        break; //Break out of the loop
                }

                bossHealthBar.enabled = true; //Enable the boss's health bar
                bossText.enabled = true; //Enable the boss's text
                break; //Break out of the loop
            }

            bossHealthBar.enabled = false; //Disable the boss's health bar
            bossText.enabled = false; //Disable the boss's text
        }
    }

    void Update() //Update is called once per frame
    {
        if (Input.GetButtonDown("Pause") && !keyboardControlMenu.gameObject.activeInHierarchy && !controllerControlMenu.gameObject.activeInHierarchy/* && !inventoryMenu.gameObject.activeInHierarchy*/) //If the pause button is pressed and the control menu is not active and the inventory menu is not active
        {
            Pause(); //Pause or unpause the game
        }
        else if ((Input.GetButtonDown("Pause") || Input.GetButtonDown("Cancel")) && pauseMenu.gameObject.activeInHierarchy && !keyboardControlMenu.gameObject.activeInHierarchy && !controllerControlMenu.gameObject.activeInHierarchy/* && !inventoryMenu.gameObject.activeInHierarchy*/) //If the pause or cancel button is pressed while the pause menu is active and the control menu is not active and the inventory menu is not active
        {
            Pause(); //Pause or unpause the game
        }
        
        if ((Input.GetButtonDown("Pause") || Input.GetButtonDown("Cancel")) && !pauseMenu.gameObject.activeInHierarchy && (keyboardControlMenu.gameObject.activeInHierarchy || controllerControlMenu.gameObject.activeInHierarchy)) //Boot from a control menu to the pause menu using the back or cancel buttons
        {
            keyboardControlMenu.gameObject.SetActive(false); //Disable the keyboard controls menu
            controllerControlMenu.gameObject.SetActive(false); //Disable the controller controls menu
            pauseMenu.gameObject.SetActive(true); //Enable the pause menu
        }
        
        //if (Input.GetButtonDown("Heal")) //If the heal button is pressed
        //{
        //    Heal(); //Heal our savior Huebert
        //}

        //ControllerConnectionManager(); //Manages controller connection behavior

        HealthUIUpdater(); //Update the player's health UI
        MessageTiming(7.5f); //Time how long messages appear for

        if (bossHealthBar.enabled == true && bossHealth > 0) //If there is a boss
        {
            BossUIUpdater(); //Update the boss UI
        }

        if (player.Health <= 0) //If he dead
        {
            SceneManager.LoadScene(0); //Load the scene
        }
    }

    private void HealthUIUpdater() //Updates the player's health UI
    {
        for (int i = 0; i < 5; i++) //For each unit of health the player has
        {
            if (player.Health >= i + 1) //For each unit of health the player has
            {
                uIImages[i].enabled = true; //Enable the player health sprite
            }
            else
            {
                uIImages[i].enabled = false; //Disable the player health sprite
            }
        }
    }

    private void BossUIUpdater() //Updates the boss's health UI
    {
        switch (boss.name) //Switch based on the boss's name
        {
            case "RoboWiz": //If the boss's name is RoboWiz
                bossHealthBar.rectTransform.sizeDelta = new Vector2(roboWizScript.Health * 10, 25); //Rescale the boss's health as it takes damage
                bossHealth = roboWizScript.Health; //Set the boss's health
                break; //Break out of the loop
        }
    }

    private void MessageTiming(float maxTime) //Timing for how long messages should appear
    {
        if (messageText.text != "" && messageTimer < maxTime) //If the message box is not empty
        {
            messageTimer += Time.deltaTime; //Update the message timer
        }
        else if (messageTimer >= maxTime) //If the message box is empty
        {
            messageText.text = ""; //Set the message to not display
            messageTimer = 0; //Reset the message timer
        }
    }

    //private void Heal() //Heal our savior Huebert
    //{
    //    if (uIOverlay.PlayerHealthTracker.Health < 5 && uIOverlay.PlayerInventory.HealthInventory > 0) //If the player's health is not at it's maximum and the player has health pickups
    //    {
    //        uIOverlay.PlayerHealthTracker.Health++; //Heal the player
    //        uIOverlay.PlayerInventory.HealthInventory--; //Use a health pickup
    //    }
    //}

    private void Pause() //When the pause button is pressed
    {
        if (!pauseMenu.gameObject.activeInHierarchy) //If the pause menu is disabled
        {
            pauseMenu.gameObject.SetActive(true); //Enable the pause menu
            Time.timeScale = 0; //Pause the game
            messageText.enabled = false; //Hide messages
            //uIOverlay.PlayerControlScript.M_Grounded = false; //Make the player not jump
        }
        else //If the pause menu is enabled
        {
            pauseMenu.gameObject.SetActive(false); //Disable the pause menu
            Time.timeScale = 1; //Run the game
            messageText.enabled = true; //Show messages
        }
    }

    public void StartButton(string loadLevel) //When the start button is clicked
    {
        SceneManager.LoadScene(loadLevel); //Load the scene
    }

    public void ResumeButton() //When the resume button is clicked
    {
        pauseMenu.gameObject.SetActive(false); //Disable the pause menu
        Time.timeScale = 1; //Run the game
    }

    public void QuitButton() //When the exit button is clicked
    {
        Application.Quit(); //Quit the game
    }

    public void ControlsButton() //When the controls button is clicked
    {
        if (!keyboardControlMenu.gameObject.activeInHierarchy && !controllerControlMenu.gameObject.activeInHierarchy) //If the keyboard controls menu is disabled and the controller controls menu is disabled
        {
            //if (!controllerConnected) //If there is no controller connected
            //{
                keyboardControlMenu.gameObject.SetActive(true); //Enable the keyboard controls menu
                pauseMenu.gameObject.SetActive(false); //Disable the pause menu
            //}
            //else //If there is a controller connected
            //{
            //    controllerControlMenu.gameObject.SetActive(true); //Enable the controller controls menu
            //    pauseMenu.gameObject.SetActive(false); //Disable the pause menu
            //}
        }
        else //In any other case
        {
            keyboardControlMenu.gameObject.SetActive(false); //Disable the keyboard controls menu
            controllerControlMenu.gameObject.SetActive(false); //Disable the controller controls menu
            pauseMenu.gameObject.SetActive(true); //Enable the pause menu
        }
    }

    public void QuitToMenu(string scene) //Quits to the main menu
    {
        SceneManager.LoadScene(scene); //Load the main menu
        Time.timeScale = 1; //Unpause the game
    }

    //private void ControllerConnectionManager() //Manages controller connection behavior
    //{
    //    try //Try to do this
    //    {
    //        if (Input.GetJoystickNames()[0] == "") //If there is not a controller connected
    //        {
    //            controllerConnected = false; //There is no controller connected
    //            qText.enabled = true; //Enable the Q text
    //            eText.enabled = true; //Enable the E text
    //            lBText.enabled = false; //Disable the LB text
    //            rBText.enabled = false; //Disable the RB text
    //        }
    //        else //If there is a controller connected
    //        {
    //            controllerConnected = true; //There is a controller connected
    //            qText.enabled = false; //Disable the Q text
    //            eText.enabled = false; //Disable the E text
    //            lBText.enabled = true; //Enable the LB text
    //            rBText.enabled = true; //Enable the RB text
    //        }
    //    }
    //    catch (IndexOutOfRangeException) //If the game is starting while no controller is connected
    //    {
    //        controllerConnected = false; //There is no controller connected
    //        qText.enabled = true; //Enable the Q text
    //        eText.enabled = true; //Enable the E text
    //        lBText.enabled = false; //Disable the LB text
    //        rBText.enabled = false; //Disable the RB text
    //    }
    //
    //    if (controllerConnected && keyboardControlMenu.gameObject.activeInHierarchy && !controllerControlMenu.gameObject.activeInHierarchy) //If a controller is connected while the keyboard controls menu is enabled and the controller controls menu is disabled
    //    {
    //        keyboardControlMenu.gameObject.SetActive(false); //Disable the keyboard controls menu
    //        controllerControlMenu.gameObject.SetActive(true); //Enable the controller controls menu
    //    }
    //    else if (!controllerConnected && !keyboardControlMenu.gameObject.activeInHierarchy && controllerControlMenu.gameObject.activeInHierarchy) //If a controller is disconnected while the keyboard controls menu is disabled and the controller controls menu is enabled
    //    {
    //        keyboardControlMenu.gameObject.SetActive(true); //Enable the keyboard controls menu
    //        controllerControlMenu.gameObject.SetActive(false); //Disable the controller controls menu
    //    }
    //}
}