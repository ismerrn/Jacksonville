using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // ---------------------------------- INGREDIENTS -------------------------------------
    // Store the the Selected Ingredient (clicked)
    // puede que NO sea static
    public static IngSelectable selectedIngr;

    // Store the pantry ingredient selected UI/Feedback
    public GameObject pIngredientSelected;

    // Store the shape selected UI/Feedback
    public GameObject gFeedback;


    // ---------------------------------- MOUSE ------------------------------------------
    // Check if the Cursor has some Ingredient clicked or not
    public static bool emptyCursor = true;


    // ---------------------------------- ARRAYS ------------------------------------------
    // Array to store all the cursor ingredients' shapes
    public GameObject[] gChipsFeedback;


    // ---------------------------------- EACH FRAME -------------------------------------------------------
    void Update()
    {
        // ---------------------------------- MOVE WITH CURSOR ------------------------------------------
        // Store a new vector to save the offset position regarding the cursor's position
        Vector3 offsetMousePos1 = new Vector3(100, -150, 1);
        Vector3 offsetMousePos2 = new Vector3(-200, 70, 1);

        // Track mouse position (related to the transform of the "World")
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Set the position of the ingredient selected UI as the cursor's position + the offset position
        pIngredientSelected.transform.position = mousePos + offsetMousePos1;

        // Set the position of ingredient selected's grid shape (feedback) as the cursor's position + the offset position
        gFeedback.transform.position = mousePos + offsetMousePos2;


        // ---------------------------------- ON CLICK -------------------------------------------------
        // If pressed left click (only in the frame clicked)
        if (Input.GetMouseButtonDown(0))       
        {
            // If there's an ingredient selected
            if (GameController.selectedIngr != null)
            {
                Debug.Log("3756 - Clicked with ingredient selected (GameController/Update)");

                // Start the place ingredient in grid method
                GameController.selectedIngr.PlaceIngrGrid();
            }
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
}
