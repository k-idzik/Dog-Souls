using UnityEngine;
//using System; //Exceptions
using System.Collections;
using System.Collections.Generic; //Dictionary
using UnityEngine.SceneManagement; //Load scenes
using UnityEngine.EventSystems; //Event systems

public class MenuScripts : MonoBehaviour
{
    [SerializeField] private Canvas uIOverlay; //The UI overlay
    //[SerializeField] private UnityStandardAssets._2D.PlatformerCharacter2D playerControlScript; //The player's control script 
    [SerializeField] private Canvas pauseMenu; //The pause menu UI
    [SerializeField] private Canvas keyboardControlMenu; //The keyboard controls menu UI
    [SerializeField] private Canvas controllerControlMenu; //The controller controls menu UI
    private UnityEngine.UI.Text messageText; //The UI text messages
    private float messageTimer; //How long to display messages for
    private UnityEngine.UI.Image[] playerHealthIcons = new UnityEngine.UI.Image[5]; //The UI images for the player's health
    private UnityEngine.UI.Image[] pHealthOutlineIcons = new UnityEngine.UI.Image[5]; //The UI images for the player's health outlines
    private Player playerScript; //The player script
    private UnityEngine.UI.Image[] bossHealthIcons = new UnityEngine.UI.Image[10]; //The UI images for the boss's health
    private UnityEngine.UI.Image[] bHealthOutlineIcons = new UnityEngine.UI.Image[10]; //The UI images for the boss's health outlines
    private Boss bossScript; //The inherited boss script
    private int bossHealth = 0; //The boss's health
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

    void Start() //Use this for initialization
    {
        UnityEngine.UI.Image[] uIImages = uIOverlay.GetComponentsInChildren<UnityEngine.UI.Image>(); //Get the images from the children
        int playerHealthArrayTracker = 0; //The tracker for placing icons in the player's health
        int pHealthOutlineArrayTracker = 0; //The tracker for placing icons in the player's health outline
        int bossHealthArrayTracker = 0; //The tracker for placing icons in the boss's health
        int bHealthOutlineArrayTracker = 0; //The tracker for placing icons in the boss's health outline

        for (int i = 0; i < uIImages.Length; i++) //For each UI text element
        {
            if (uIImages[i].name.Contains("PlayerHealth")) //If the current UI image is the player's health
            {
                playerHealthIcons[playerHealthArrayTracker] = uIImages[i]; //Set the player's health icons
                playerHealthArrayTracker++; //Increment the tracker
            }
            else if (uIImages[i].name.Contains("PHealthOutline")) //If the current UI image is the player's health outline
            {
                pHealthOutlineIcons[pHealthOutlineArrayTracker] = uIImages[i]; //Set the player's health outline icons
                pHealthOutlineArrayTracker++; //Increment the tracker
            }
            else if (uIImages[i].name.Contains("BossHealth")) //If the current UI image is the boss's health
            {
                bossHealthIcons[bossHealthArrayTracker] = uIImages[i]; //Set the boss's health icons
                bossHealthArrayTracker++; //Increment the tracker
            }
            else if (uIImages[i].name.Contains("BHealthOutline")) //If the current UI image is the boss's health outline
            {
                bHealthOutlineIcons[bHealthOutlineArrayTracker] = uIImages[i]; //Set the boss's health outline icons
                bHealthOutlineArrayTracker++; //Increment the tracker
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
        }

        GameObject[] search = FindObjectsOfType<GameObject>(); //Get gameobjects from the scene

        Dictionary<string, Color> bossRoomColors = new Dictionary<string, Color>(); //Create a dictionary to store the boss rooms and associated colors
        bossRoomColors.Add("BossRoom0", new Color(184, 3, 144)); //Add the first boss room to the dictionary
        bossRoomColors.Add("BossRoom1", new Color(253, 248, 5)); //Add the second boss room to the dictionary
        bossRoomColors.Add("BossRoom2", new Color(67, 165, 184)); //Add the third boss room to the dictionary
        Color bossOutlineColor; //The color for the outline of the boss's health

        for (int i = 0; i < search.Length; i++) //For each gameobject in the scene
        {
            if (search[i].tag == "Player") //If the current gameobject is the player
            {
                playerScript = search[i].GetComponent<Player>(); //Set the player
            }
            else if (search[i].tag == "boss") //If the current gameobject is the boss
            {
                bossScript = search[i].GetComponent<Boss>(); //Set the boss
                bossHealth = bossScript.Health; //Set the boss's health

                bossRoomColors.TryGetValue(gameObject.scene.name, out bossOutlineColor); //Get the color for the outline of the boss's health

                for (int j = 0; j < bossHealthIcons.Length; j++) //Loop to enable the boss's health icons
                {
                    bossHealthIcons[j].enabled = true; //Enable the boss's health icons
                    bHealthOutlineIcons[j].enabled = true; //Enable the boss's health outline icons
                    bHealthOutlineIcons[j].color = new Color(bossOutlineColor.r / 255, bossOutlineColor.g / 255, bossOutlineColor.b / 255); //Set the color for the outline of the boss's health
                }

                for (int j = 0; j < pHealthOutlineIcons.Length; j++) //Loop to change the color of the player's health outline icons
                {
                    pHealthOutlineIcons[j].color = new Color(bossOutlineColor.r / 255, bossOutlineColor.g / 255, bossOutlineColor.b / 255); //Set the color for the outline of the player's health
                }

                messageText.enabled = false; //Disable the message text
            }
        }

        if (bossHealth == 0) //If there is no boss
        {
            for (int i = 0; i < bossHealthIcons.Length; i++) //Loop to disable the health icons
            {
                bossHealthIcons[i].enabled = false; //Disable the boss's health icons
                bHealthOutlineIcons[i].enabled = false; //Disable the boss's health outline icons
            }

            for (int i = 0; i < pHealthOutlineIcons.Length; i++) //Loop to change the color of the player's health outline icons
            {
                pHealthOutlineIcons[i].color = Color.grey; //Set the color for the outline of the player's health
            }
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

        if (bossHealthIcons[0].enabled) //If there is a boss and he's not dead
        {
            BossUIUpdater(); //Update the boss UI
        }

        if (playerScript.Health <= 0) //If he dead
        {
            SceneManager.LoadScene(gameObject.scene.name); //Load the scene
        }
    }

    private void HealthUIUpdater() //Updates the player's health UI
    {
        for (int i = 0; i < 5; i++) //For each unit of health the player has
        {
            if (playerScript.Health >= i + 1) //For each unit of health the player has
            {
                playerHealthIcons[i].enabled = true; //Enable the player health sprite
                pHealthOutlineIcons[i].enabled = true; //Enable the player health sprite outline
            }
            else
            {
                playerHealthIcons[i].enabled = false; //Disable the player health sprite
                pHealthOutlineIcons[i].enabled = false; //Disable the player health sprite outline
            }
        }
    }

    private void BossUIUpdater() //Updates the boss's health UI
    {
        for (int i = 0; i < 10; i++) //For each unit of health the boss has
        {
            if (bossScript.Health >= i + 1) //For each unit of health the boss has
            {
                bossHealthIcons[i].enabled = true; //Enable the boss health sprite
                bHealthOutlineIcons[i].enabled = true; //Enable the boss health sprite outline
            }
            else
            {
                bossHealthIcons[i].enabled = false; //Disable the boss health sprite
                bHealthOutlineIcons[i].enabled = false; //Disable the boss health sprite outline
            }
        }
        bossHealth = bossScript.Health; //Set the boss's health
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