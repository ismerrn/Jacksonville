using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    // ESTO ES UN CÓDIGO DE CLICKER EN EL PANTRY

    // ---------------------------------- INGREDIENTS -------------------------------------
    // Sprite renderer access
    private SpriteRenderer pIngredientRenderer;

    // Store the ingredient selected UI/Feedback
    public GameObject pIngredientSelected;

    // Store the shape selected UI/Feedback
    public GameObject gFeedback;

    // Check if the ingredient has been clicked (default = false)
    public bool isIngrSelected = false;


    // ---------------------------------- SCRIPTS -----------------------------------------
    // Access Game Controller script
    public GameController GameControllerScript;

    // Access Pantry Behaviour script
    public Pantry pantryScript;

    // Access Ingredient Selectable script
    public IngSelectable IngrSelectableScript;


    // ---------------------------------- ARRAYS ------------------------------------------
    // Array to store all the ingredient chips with the default sprite
    public Sprite[] pIngredientDefault;

    // Ingredient chips default sprite
    public Sprite PTomato;
    public Sprite PCarrot;
    public Sprite PEggplant;
    public Sprite PMushroom;

    // Array to store all the ingredient chips with the blocked sprite
    public Sprite[] pIngredientBlocked;

    // Ingredient chips blocked sprite
    public Sprite PTomatoBlocked;
    public Sprite PCarrotBlocked;
    public Sprite PEggplantBlocked;
    public Sprite PMushroomBlocked;

    // Array to store all the cursor's ingredients selected
    public Sprite[] cIngredientSelected;

    // Ingredient chips selected (cursor)
    public Sprite CTomatoSelected;
    public Sprite CCarrotSelected;
    public Sprite CEggplantSelected;
    public Sprite CMushroomSelected;


    // ---------------------------------- MOUSE ------------------------------------------
    // Store Mouse position every frame
    public Vector3 mousePos;

    // Check if clicked (default = false)
    public bool isClicked = false;


    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    void Start()
    {
        // ---------------------------------- ACCESS --------------------------------------------
        // Access Ingredient chip sprite renderer
        pIngredientRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

        // Access the Pantry script
        pantryScript = FindObjectOfType<Pantry>();

        // Access the Game Controller script
        GameControllerScript = FindObjectOfType<GameController>();


        // ---------------------------------- ACTIVATION ----------------------------------------
        // Deactivate the renderer of the ingredient selected (as none has been selected for now)
        pIngredientSelected.GetComponent<SpriteRenderer>().enabled = false;


        // ---------------------------------- SET ARRAYS ----------------------------------------
        // Add all the sprites to the Pantry's Ingredients (default) array
        pIngredientDefault[0] = PTomato;
        pIngredientDefault[1] = PCarrot;
        pIngredientDefault[2] = PEggplant;
        pIngredientDefault[3] = PMushroom;

        // Add all the sprites to the Pantry's Ingredients (blocked) array
        pIngredientBlocked[0] = PTomatoBlocked;
        pIngredientBlocked[1] = PCarrotBlocked;
        pIngredientBlocked[2] = PEggplantBlocked;
        pIngredientBlocked[3] = PMushroomBlocked;

        // Add all the sprites to the Pantry's Ingredients (default) array
        cIngredientSelected[0] = CTomatoSelected;
        cIngredientSelected[1] = CCarrotSelected;
        cIngredientSelected[2] = CEggplantSelected;
        cIngredientSelected[3] = CMushroomSelected;
    }


    // ---------------------------------- INGREDIENT CHIP IS CLICKED -----------------------------------------------------
    void OnMouseDown()
    {
        Debug.Log("Caaaashate");

        // There has been a click, so activate it
        isClicked = true;

        // Active the renderer of the ingredient selected
        pIngredientSelected.GetComponent<SpriteRenderer>().enabled = true;

        // Update the pantry's ingredients chips (block/available to interact, set as selected...)
        PIngredientUpdate();
    }


    // ---------------------------------- CHANGE INGREDIENT'S SPRITES ---------------------------------------
    public void PIngredientUpdate ()
    {
        Debug.Log("antihero");

        // ------------------------- CHECK PANTRY INGREDIENTS INTERACTION -----------------------------
        // Loop through the 4 Pantry's Ingredient sprites
        for (int i = 0; i < 4; i = i + 1)
        {
            // If the CLICKED INGREDIENT Matches the ingredient of that round of the LOOP
            if (pIngredientRenderer.sprite == pIngredientDefault[i])
            {
                // And if the ingredient is marked as NOT SELECTED
                if (pIngredientRenderer.transform.parent.GetComponent<Clicker>().isIngrSelected == false)
                {
                    // Mark the looped sprite as an ingredient SELECTED
                    pIngredientRenderer.transform.parent.GetComponent<Clicker>().isIngrSelected = true;

                    // Select Ingredient (UI - block pantry ingredient & set the ingredient as selected in the cursor UI)
                    SelectIngr(i);

                    // Change the sprite for it's deactivated one (blocked)
                    pIngredientRenderer.sprite = pIngredientBlocked[i];

                    // Set it as selected ingredient UI
                    pIngredientSelected.GetComponent<SpriteRenderer>().sprite = cIngredientSelected[i];

                    // Start Spawn Ingredient Grid method (from Game Controller script) adding the meaning of i in this loop
                    GameControllerScript.SpawnIngredientGrid(i);

                    // Start the method of unblocking ingredients
                    pantryScript.UnblockIngredients(i);
                }
            }

        }
    }


    // ---------------------------------- SELECT THE INGREDIENT UI -------------------------------------------
    void SelectIngr(int i)
    {
        // Change the sprite for it's deactivated one (blocked)
        pIngredientRenderer.sprite = pIngredientBlocked[i];

        // Set it as selected ingredient UI
        pIngredientSelected.GetComponent<SpriteRenderer>().sprite = cIngredientSelected[i];
    }


    // ---------------------------------- DESELECT INGREDIENT UI ---------------------------------------------
    public void DeselectIngr(int i)
    {
        // Change the sprite for it's active one (default)
        pIngredientRenderer.sprite = pIngredientDefault[i];

        // Set the Selected Ingredient UI as blank
        //pIngredientSelected.GetComponent<SpriteRenderer>().sprite = null;

        // Deactivate the renderer of the ingredient selected (as none is selected for now)
        pIngredientSelected.GetComponent<SpriteRenderer>().enabled = false;

        // Mark it as NOT CLICKED anymore
        isClicked = false;

        // Set the Ingredient as not selected
        isIngrSelected = false;
    }
}
