using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //Reloading scenes

public class UIOverlayScript : MonoBehaviour //KI
{
    //[SerializeField] private Canvas uIOverlay; //The UI overlay
    //[SerializeField] private HealthTracker playerHealthTracker; //The player's health
    //[SerializeField] private ItemPickup playerInventory; //The player's inventory script
    //[SerializeField] private UnityStandardAssets._2D.PlatformerCharacter2D playerControlScript; //The player's control script 
    //private UnityEngine.UI.Image[] uIImages; //The UI images for the player's health
    //private UnityEngine.UI.Text messageText; //The UI text messages
    //private float messageTimer; //How long to display messages for
    //private bool orbInventoryFull; //If the player's orb inventory is full
    //private bool firstHealthPickup; //If the health pickup is the first one
    //
    //public Canvas UIOverlay //UIOverlay property
    //{
    //    get
    //    {
    //        return uIOverlay; //Return the UI overlay
    //    }
    //}
    //
    //public HealthTracker PlayerHealthTracker //PlayerHealthTracker property
    //{
    //    get
    //    {
    //        return playerHealthTracker; //Return the player's health tracker
    //    }
    //}
    //
    //public UnityStandardAssets._2D.PlatformerCharacter2D PlayerControlScript //PlayerControlScript property
    //{
    //    get
    //    {
    //        return playerControlScript; //Return the player's playerControlScript
    //    }
    //}
    //
    //public ItemPickup PlayerInventory //PlayerInventory property
    //{
    //    get
    //    {
    //        return playerInventory; //Return the player's inventory
    //    }
    //}
    //
    //public UnityEngine.UI.Text MessageText //MessageText property
    //{
    //    get
    //    {
    //        return messageText;
    //    }
    //    set
    //    {
    //        messageText = value; //Set the value of the message text
    //    }
    //}
    //
    //void Start() //Use this for initialization
    //{
    //    uIImages = uIOverlay.GetComponentsInChildren<UnityEngine.UI.Image>(); //Get the images from the children
    //    orbInventoryFull = false; //The player's orb inventory is not full
    //    firstHealthPickup = true; //The player's health pickup will be the first one
    //}

    //void Update() //Update is called once per frame
    //{
    //    HealthUIUpdater(); //Update the player's health UI
    //    HealthRestoreUIUpdater(); //Update the player's health restore UI
    //    OrbUIUpdater(); //Update the player's orb UI
    //    WeaponUIUpdater(); //Update the player's weapon UI
    //
    //    if (messageText.text != "" && messageTimer < 7.5) //If the message box is not empty
    //    {
    //        messageTimer += Time.deltaTime; //Update the message timer
    //    }
    //    else if (messageTimer >= 7.5) //If the message box is empty
    //    {
    //        messageText.text = ""; //Set the message to not display
    //        messageTimer = 0; //Reset the message timer
    //    }
    //}


    //private void HealthRestoreUIUpdater() //Updates the player's health restore UI
    //{
    //
    //    if (playerInventory.HealthInventory == 0) //If the player has no health pickups
    //    {
    //        uIImages[10].enabled = false; //Show the health pickup on the UI
    //        uIImages[11].enabled = false; //Hide the health pickup on the UI
    //        uIImages[12].enabled = false; //Hide the health pickup on the UI
    //    }
    //    else if (playerInventory.HealthInventory == 1) //If the player has one health pickup
    //    {
    //        if (firstHealthPickup == true) //If this is the player's first health pickup
    //        {
    //            messageText.text = "Health picked up. Right click to heal if using a mouse. Press Y to heal if using a controller."; //Set a message for the player
    //            messageTimer = 0; //Reset the message timer
    //            firstHealthPickup = false; //It will no longer be the player's first health pickup
    //        }
    //        uIImages[10].enabled = true; //Show the health pickup on the UI
    //        uIImages[11].enabled = false; //Hide the health pickup on the UI
    //        uIImages[12].enabled = false; //Hide the health pickup on the UI
    //    }
    //    else if (playerInventory.HealthInventory == 2) //If the player has two health pickups
    //    {
    //        uIImages[10].enabled = true; //Show the health pickup on the UI
    //        uIImages[11].enabled = true; //Show the health pickup on the UI
    //        uIImages[12].enabled = false; //Hide the health pickup on the UI
    //    }
    //    else if (playerInventory.HealthInventory == 3) //If the player has three health pickups
    //    {
    //        uIImages[10].enabled = true; //Show the health pickup on the UI
    //        uIImages[11].enabled = true; //Show the health pickup on the UI
    //        uIImages[12].enabled = true; //Show the health pickup on the UI
    //    }
    //}

    //private void OrbUIUpdater() //Updates the player's orb UI
    //{
    //    if (!orbInventoryFull) //If the player's orb inventory is not full
    //    {
    //        for (int i = 0; i < playerInventory.OrbInventory.Capacity; i++) //For each orb in the orb inventory
    //        {
    //            if (playerInventory.OrbInventory.Contains(uIImages[13 + i].name) && !playerInventory.ActivatedOrbEffect[i]) //If the orb inventory contains the orb and it has not yet been activated
    //            {
    //                uIImages[13 + i].color = new Color(uIImages[13 + i].color.r, uIImages[13 + i].color.g, uIImages[13 + i].color.b, 1); //Make the orb's UI less transparent
    //                messageText.text = "Orb picked up. Health restored."; //Set a message for the player
    //                messageTimer = 0; //Reset the message timer
    //                playerHealthTracker.Health = 5; //Restore the player's health
    //                playerInventory.ActivatedOrbEffect[i] = true; //The orb's effect has been activated
    //            }
    //
    //            if (playerInventory.OrbInventory.Count == playerInventory.OrbInventory.Capacity) //If the player's orb inventory is ful
    //            {
    //                orbInventoryFull = true; //The player's orb inventory is full
    //            }
    //        }
    //    }
    //}

    //private void WeaponUIUpdater() //Updates the player's weapon UI
    //{
    //    for (int i = 0; i < playerControlScript.weapons.Length; i++) //For each weapon
    //    {
    //        if (playerControlScript.activeWeapon == i) //If the player's active weapon is the one to be enabled
    //        {
    //            if (i == 0) //If the first weapon is enabed
    //            {
    //                uIImages[19].enabled = true; //Enable the first weapon
    //                uIImages[20].enabled = false; //Disable the second weapon
    //                uIImages[21].enabled = false; //Disable the third weapon
    //            }
    //            else if (i == 1) //If the second weapon is enabled
    //            {
    //                uIImages[19].enabled = false; //Disable the first weapon
    //                uIImages[20].enabled = true; //Enable the second weapon
    //                uIImages[21].enabled = false; //Disable the third weapon
    //            }
    //            else if (i == 2) //If the third weapon is enabled
    //            {
    //                uIImages[19].enabled = false; //Disable the first weapon
    //                uIImages[20].enabled = false; //Disable the second weapon
    //                uIImages[21].enabled = true; //Enable the third weapon
    //            }
    //        }
    //    }
    //}
}