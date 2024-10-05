using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class DeliverBut : MonoBehaviour
{
    // ---------------------------------- DELIVERY -----------------------------------------
    // Store if the Delivery Button has been clicked
    public bool isDeliveryClicked = false;


    // ---------------------------------- GAME CONTROLLER ------------------------------------------
    // Reference to the Game Controller GO
    private GameObject gameController;

    // Reference to the Game Controller Script
    private GameController GameControllerScript;


    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    void Start()
    {
        // ---------------------------------- ACCESS --------------------------------------------
        // Access Game Controller GO
        gameController = GameObject.Find("Game Controller");

        // Access the Game Controller Script from the Game Controller GO
        GameControllerScript = gameController.GetComponent<GameController>();
    }


    // When Deliver Button (in-game - map) gets clicked
    public void DeliverButton()
    {
        // Set the Delivery button as clicked
        isDeliveryClicked = true;

        // ---------------------------------- EMPTY CURSOR UI -----------------------------------
        // If there's an ingredient selected
        if (GameController.emptyCursor == false)
        {
            // Unselect Ingredient
            GameControllerScript.UnselectIngredient();
        }

        // Deactivate the deliver button
        gameObject.SetActive(false);
    }


    /*public void StartGame()
    {
        SceneManager.LoadScene("Calendar");
    }*/


    /*public void ReturnGame()
    {
        SceneManager.LoadScene("Menú");
    }*/


    /*public void CreditsGame()
    {
        SceneManager.LoadScene("Credits");
    }*/


    /*public void ExitGame()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }*/
}
