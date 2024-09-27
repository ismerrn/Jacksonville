using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEditor.SearchService;
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
        offsetMouse = new Vector3(100, -150, 1);

        // Store the offset for each ingredient when following the cursor
        offsetMouseTomato = new Vector3(-135, 5, 1);
        offsetMouseCarrot = new Vector3(-70, 65, 1);
        offsetMouseEggplant = new Vector3(-135, 5, 1);
        offsetMouseMushroom = new Vector3(-210, 5, 1);
    }


    // ---------------------------------- EACH FRAME -------------------------------------------------------
    void Update()
    {
        // ---------------------------------- ACCESS SCRIPTS -------------------------------------------
        // Access the IngSelectable script
        //IngSelectableScript = FindObjectOfType<IngSelectable>();


        // ---------------------------------- MOVE WITH CURSOR ------------------------------------------
        // Track mouse position (related to the transform of the "World")
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        // ---------------------------------- ON CLICK -------------------------------------------------
        // If pressed left click (only in the frame clicked)

        //if(Input.GetKeyDown(KeyCode.A))
        if (Input.GetMouseButtonDown(0))
        {
            // If there's an ingredient selected
            if (selectedIngr != null)
            {
                Debug.Log("3756 - Clicked with ingredient selected (GameController/Update)");

                // ---------------------------------- PLACE INGREDIENTS (GRID) -----------------------------------------------------
                // Start the place ingredient in grid method
                selectedIngr.PlaceIngrGrid();
            }
        }


        // ---------------------------------- WHEN NOTHING IS SELECTED ----------------------------------
        // If nothing is selected
        if (emptyCursor == true)
        {
            // Loop through all the ClickerScripts on the scene
            for (int i = 0; i < ClickerScripts.Length; i = i + 1)
            {
                if (ClickerScripts[i].isClicked == true)
                {
                    ClickerScripts[i].DeselectIngr(i);
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
}
