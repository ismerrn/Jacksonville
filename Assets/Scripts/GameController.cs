using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // ---------------------------------- INGREDIENTS -------------------------------------
    // Store the the Selected Ingredient (clicked) (puede que NO sea static??)
    public static IngSelectable selectedIngr;

    // Store the pantry ingredient selected UI/Feedback
    public GameObject pIngredientSelected;

    // Store the shape selected UI/Feedback
    public GameObject gFeedback;

    // Store the number of each type of Ingredients already placed in the backpack inventory
    public int tomatosPlaced = 0;
    public int carrotsPlaced = 0;
    public int eggplantsPlaced = 0;
    public int mushroomsPlaced = 0;


    // ---------------------------------- MOUSE ------------------------------------------
    // Check if the Cursor has some Ingredient clicked or not
    public static bool emptyCursor = true;

    // Store the Mouse position each frame
    private Vector3 mousePos;

    // Set offsets for the mouse position
    private Vector3 offsetMouse;
    private Vector3 offsetMouseTomato;
    private Vector3 offsetMouseCarrot;
    private Vector3 offsetMouseEggplant;
    private Vector3 offsetMouseMushroom;


    // ---------------------------------- CAMERA ------------------------------------------
    // Set offsets for the camera position in each scene (Menu, Calendar, Backpack, Map, Execution)
    private Vector3 calendarCamOffset;
    private Vector3 backpackCamOffset;
    private Vector3 mapCamOffset;


    // ---------------------------------- ARRAYS ------------------------------------------
    // Array to store all the cursor ingredients' shapes
    public GameObject[] gChipsFeedback;

    // Array to store all the Clicker scripts in the screne
    public Clicker[] ClickerScripts;

    private IngSelectable IngSelectableScript;


    // ---------------------------------- RAYCAST -----------------------------------------
    // To specify layers to use in Physics.Raycast
    public LayerMask layerMask;


    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    private void Start()
    {
        // ---------------------------------- SET VALUES -------------------------------------------------
        // Store the offset for the Ingredient Selected chip when following the cursor
        offsetMouse = new Vector3(130, -80, 1); // new Vector3(100, -150, 1)

        // Store the offset for each ingredient when following the cursor
        offsetMouseTomato = new Vector3(-125, -15, 1); // new Vector3(-135, 5, 1)
        offsetMouseCarrot = new Vector3(-50, 50, 1); // new Vector3(-70, 65, 1)
        offsetMouseEggplant = new Vector3(-125, -15, 1); // new Vector3(-135, 5, 1)
        offsetMouseMushroom = new Vector3(-195, -15, 1); // new Vector3(-210, 5, 1)

        // Store the offset for each camera position in each scene
        calendarCamOffset = new Vector3(-3000, 0, -4);
        backpackCamOffset = new Vector3(0, 0, -4);
        mapCamOffset = new Vector3(3000, 0, -4);
    }


    // ---------------------------------- EACH FRAME -------------------------------------------------------
    void Update()
    {
        // ---------------------------------- MOVE WITH CURSOR ------------------------------------------
        // Track mouse position (related to the transform of the "World")
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        // ---------------------------------- ON RIGHT CLICK -------------------------------------------------
        // If pressed right click (only in the frame clicked)
        if (Input.GetMouseButtonDown(1))
        {
            // And If some ingredient has been selected
            if (emptyCursor == false && selectedIngr != null)
            {
                Debug.Log("II - Deselect ingredient [GameController/Update/Righ click input]");

                // Unselect that ingredient
                UnselectIngredient();
            }
        }


        // ---------------------------------- WHEN NOTHING IS SELECTED ----------------------------------
        // If nothing is selected
        if (emptyCursor == true)
        {
            // Loop through all the ClickerScripts on the scene
            for (int i = 0; i < ClickerScripts.Length; i = i + 1)
            {
                // If one of this pantries has been clicked
                if (ClickerScripts[i].isClicked == true)
                {
                    // Deselect that ingredient
                    ClickerScripts[i].UnselectIngrUI(i);
                }
            }
        }

        Debug.Log(selectedIngr);
    }

    // ---------------------------------- FRAME-RATE INDEPENDENT 4 PHYSICS CALCULATIONS --------------------
    void FixedUpdate()
    {
        // ---------------------------------- MOVE WITH CURSOR ------------------------------------------
        // Set the ingredient selected's position to follow the mouse (with an offset)
        pIngredientSelected.transform.position = mousePos + offsetMouse;

        // If there's a Selected Ingredient
        if (selectedIngr != null)
        {
            // Set the Ingredient Selected position (following the mouse)
            SetOffsetIngr();
        }
    }


    // ---------------------------------- SPAWN INGREDIENT COPIES ------------------------------------------
    public void SpawnIngredientGrid(int i)
    {
        // Ingredient selected
        emptyCursor = false;


        // ---------------------------------- DESTROY CHILDS ------------------------------------------
        // Destroy previous childs
        foreach (Transform child in gFeedback.transform)
        {
            Destroy(child.gameObject);
        }


        // Create a copy of that Ingredient Grid chip as a Child of gFeedback GO
        selectedIngr = Instantiate(gChipsFeedback[i], gFeedback.transform).GetComponent<IngSelectable>();
    }


    // ---------------------------------- CHANGE INGREDIENT SPRITES -----------------------------------------------------
    public void SetOffsetIngr()
    {
        // If the ingredient selected is a Tomato
        if (selectedIngr.gameObject.tag == "Tomato")
        {
            // Set the position of the Tomato selected with an offset regarding the mouse position
            gFeedback.transform.position = mousePos + offsetMouseTomato;
        }

        // If the ingredient selected is a Carrot
        else if (selectedIngr.gameObject.tag == "Carrot")
        {
            // Set the position of the Carrot selected with an offset regarding the mouse position
            gFeedback.transform.position = mousePos + offsetMouseCarrot;
        }

        // If the ingredient selected is a Eggplant
        else if (selectedIngr.gameObject.tag == "Eggplant")
        {
            // Set the position of the Eggplant selected with an offset regarding the mouse position
            gFeedback.transform.position = mousePos + offsetMouseEggplant;
        }

        // If the ingredient selected is a Mushroom
        else if (selectedIngr.gameObject.tag == "Mushroom")
        {
            // Set the position of the Mushroom selected with an offset regarding the mouse position
            gFeedback.transform.position = mousePos + offsetMouseMushroom;
        }
    }

    public void UnselectIngredient()
    {
        // ---------------------------------- DESTROY CHILDS ------------------------------------------
        // Delete the ingredient shape UI in the cursor
        foreach (Transform child in gFeedback.transform)
        {
            Destroy(child.gameObject);
        }

        // Deselect ingredient
        selectedIngr = null;

        // Set the cursor as empty
        emptyCursor = true;
    }
}
