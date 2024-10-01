using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    // ---------------------------------- CAMERA ------------------------------------------
    // Reference to the Main Camera (with the Canvas as a child object)
    private GameObject mainCamera;

    // Reference to the Main Camera's position
    private Vector3 mainCameraPos;

    // Set offsets for the camera position in each scene (Menu, Calendar, Backpack, Map, Execution)
    private Vector3 calendarCamOffset;
    private Vector3 backpackCamOffset;
    private Vector3 mapCamOffset;


    // ---------------------------------- GAME CONTROLLER ------------------------------------------
    // Reference to the Game Controller GO
    private GameObject gameController;

    // Reference to the Game Controller Script
    private GameController GameControllerScript;


    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    void Start()
    {
        // ---------------------------------- ACCESS --------------------------------------------
        // Access Main Camera GO
        mainCamera = GameObject.Find("Main Camera");

        // Access Game Controller GO
        gameController = GameObject.Find("Game Controller");

        // Access the Game Controller Script from the Game Controller GO
        GameControllerScript = gameController.GetComponent<GameController>();

        // Store the offset for each camera position in each scene
        calendarCamOffset = new Vector3(-3000, 0, -4);
        backpackCamOffset = new Vector3(0, 0, -4);
        mapCamOffset = new Vector3(3000, 0, -4);
    }


    // When Calendar Button (in-game) gets clicked
    public void CalendarButton()
    {
        Debug.Log("Change to Calendar screen");

        // ---------------------------------- EMPTY CURSOR UI -----------------------------------
        // If there's an ingredient selected
        if (GameController.emptyCursor == false)
        {
            // Unselect Ingredient
            GameControllerScript.UnselectIngredient();
        }


        // ---------------------------------- CHANGE CAMERA POSITION ----------------------------
        // Set the Main Camera's position as the Calendar screen position
        mainCamera.transform.position = calendarCamOffset;


        // ---------------------------------- SET CAMERA BOOLS ----------------------------------
        // Store that Camera is focusing the Calendar screen
        GameControllerScript.isInCalendar = true;

        // Store that Camera isn't focusing in the Backpack screen
        GameControllerScript.isInBackpack = false;

        // Store that Camera isn't focusing in the Map screen
        GameControllerScript.isInMap = false;
    }


    // When Backpack Button (in-game) gets clicked
    public void BackpackButton()
    {
        Debug.Log("Change to Backpack screen");


        // ---------------------------------- EMPTY CURSOR UI -----------------------------------
        // If there's an ingredient selected
        if (GameController.emptyCursor == false)
        {
            // Unselect Ingredient
            GameControllerScript.UnselectIngredient();
        }


        // ---------------------------------- CHANGE CAMERA POSITION ----------------------------
        // Set the Main Camera's position as the Backpack screen position
        mainCamera.transform.position = backpackCamOffset;


        // ---------------------------------- SET CAMERA BOOLS ----------------------------------
        // Store that Camera isn't focusing in the Calendar screen
        GameControllerScript.isInCalendar = false;

        // Store that Camera is focusing the Backpack screen
        GameControllerScript.isInBackpack = true;

        // Store that Camera isn't focusing in the Map screen
        GameControllerScript.isInMap = false;
    }


    // When Map Button (in-game) gets clicked
    public void MapButton()
    {
        Debug.Log("Change to Map screen");


        // ---------------------------------- EMPTY CURSOR UI -----------------------------------
        // If there's an ingredient selected
        if (GameController.emptyCursor == false)
        {
            // Unselect Ingredient
            GameControllerScript.UnselectIngredient();
        }


        // ---------------------------------- CHANGE CAMERA POSITION ----------------------------
        // Set the Main Camera's position as the Map screen position
        mainCamera.transform.position = mapCamOffset;


        // ---------------------------------- SET CAMERA BOOLS ----------------------------------
        // Store that Camera isn't focusing the Calendar screen
        GameControllerScript.isInCalendar = false;

        // Store that Camera isn't focusing the Backpack screen
        GameControllerScript.isInBackpack = false;

        // Store that Camera is focusing the Map screen
        GameControllerScript.isInMap = true;
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
