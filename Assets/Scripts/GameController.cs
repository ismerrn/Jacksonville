using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // ---------------------------------- INGREDIENTS -------------------------------------
    // Store the the Selected Ingredient
    // puede que NO sea static
    public static IngSelectable selectedIngr;

    // Store the shape selected UI/Feedback
    public GameObject gFeedback;


    // ---------------------------------- MOUSE ------------------------------------------
    // Check if the Cursor has some Ingredient clicked or not
    public static bool emptyCursor = true;


    // ---------------------------------- ARRAYS ------------------------------------------
    // Array to store all the cursor ingredients' shapes
    public GameObject[] gChipsFeedback;


    // ---------------------------------- ARRAYS ------------------------------------------
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
