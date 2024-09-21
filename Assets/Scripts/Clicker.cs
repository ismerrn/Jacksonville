using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    // ---------------------------------- INGREDIENTS -------------------------------------
    // Sprite renderer access
    private SpriteRenderer pIngredientRenderer;

    // Store the ingredient selected UI/Feedback
    public GameObject pIngredientSelected;

    // Store the cursor's ingredients' shapes' parent
    public GameObject cIngredientShapes;

    // Check if the ingredient has been clicked (default = false)
    public bool isIngrSelected = false;


    // ---------------------------------- SCRIPTS -----------------------------------------
    // Access Pantry Behaviour script
    public PantryBehaviour pantryBehaviourScript;


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

    // Array to store all the cursor ingredients' shapes
    public GameObject[] cIngredientShape;

    // Ingredients' Shapes
    public GameObject CTomatoShape;
    public GameObject CCarrotShape;
    public GameObject CEggplantShape;
    public GameObject CMushroomShape;


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

        // Access the Pantry Behaviour script
        pantryBehaviourScript = FindObjectOfType<PantryBehaviour>();


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

        // Add all the ingredients' shapes to the Cursor array
        cIngredientShape[0] = CTomatoShape;
        cIngredientShape[1] = CCarrotShape;
        cIngredientShape[2] = CEggplantShape;
        cIngredientShape[3] = CMushroomShape;
    }


    // ---------------------------------- EACH FRAME -------------------------------------------------------
    private void Update()
    {
        // Store a new vector to save the offset position regarding the cursor's position
        Vector3 offsetMousePos1 = new Vector3(100, -150, 1);
        Vector3 offsetMousePos2 = new Vector3(-100, 150, 1);

        // Track mouse position (related to the transform of the "World")
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Set the position of the ingredient selected UI as the cursor's position + the offset position
        pIngredientSelected.transform.position = mousePos + offsetMousePos1;

        // Set the position of the ingredient selected UI as the cursor's position + the offset position
        cIngredientShapes.transform.position = mousePos + offsetMousePos2;
    }


    // ---------------------------------- GO IS CLICKED -----------------------------------------------------
    void OnMouseDown()
    {
        // There has been a click, so activate it
        isClicked = true;

        // Active the renderer of the ingredient selected
        pIngredientSelected.GetComponent<SpriteRenderer>().enabled = true;

        // Change the Ingredient Sprite
        PIngredientUpdate();
    }


    // ---------------------------------- CHANGE INGREDIENT'S SPRITES ---------------------------------------
    void PIngredientUpdate ()
    {
        // Loop through 4 Ingredient sprites - to check which one is being clicked
        for (int i = 0; i < 4; i = i + 1)
        {
            // If the ingredient clicked matches the ingredient of that round of the loop
            if (pIngredientRenderer.sprite == pIngredientDefault[i])
            {
                // And if the ingredient is marked as NOT SELECTED
                if (pIngredientRenderer.transform.parent.GetComponent<Clicker>().isIngrSelected == false)
                {
                    // Mark the looped sprite as an ingredient SELECTED
                    pIngredientRenderer.transform.parent.GetComponent<Clicker>().isIngrSelected = true;

                    // Change the sprite for it's blocked one
                    pIngredientRenderer.sprite = pIngredientBlocked[i];

                    // Set it asto it's selected ingredient UI
                    pIngredientSelected.GetComponent<SpriteRenderer>().sprite = cIngredientSelected[i];

                    // Active it's Shape's GO
                    cIngredientShape[i].SetActive (true);

                    // Mark it as NOT CLICKED anymore
                    isClicked = false;

                    // Start the method of unblocking ingredients
                    pantryBehaviourScript.UnblockIngredients(i);
                }
            }

        }
    }
}
