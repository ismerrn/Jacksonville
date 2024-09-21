using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    // ---------------------------------- SPRITE RENDERERS ---------------------------------
    // Sprite renderer access
    private SpriteRenderer ingredientChipRenderer;


    // ---------------------------------- GAME OBJECTS (GO) --------------------------------
    // Store the ingredient selected UI/Feedback
    public GameObject ingredientSelected;

    // Store the cursor's ingredients' shapes' parent
    public GameObject cIngredientShapes;


    // ---------------------------------- ARRAYS ------------------------------------------
    // Array to store all the ingredient chips with the default sprite
    public Sprite[] pantryIngrDefault;

    // Ingredient chips default sprite
    public Sprite PTomato;
    public Sprite PCarrot;
    public Sprite PEggplant;
    public Sprite PMushroom;

    // Array to store all the ingredient chips with the blocked sprite
    public Sprite[] pantryIngrBlocked;

    // Ingredient chips blocked sprite
    public Sprite PTomatoBlocked;
    public Sprite PCarrotBlocked;
    public Sprite PEggplantBlocked;
    public Sprite PMushroomBlocked;

    // Array to store all the cursor's ingredients selected
    public Sprite[] cursorIngrSelected;

    // Ingredient chips selected (cursor)
    public Sprite CTomatoSelected;
    public Sprite CCarrotSelected;
    public Sprite CEggplantSelected;
    public Sprite CMushroomSelected;

    // Array to store all the cursor ingredients' shapes
    public GameObject[] cursorIngrShape;

    // Ingredients' Shapes
    public GameObject CTomatoShape;
    public GameObject CCarrotShape;
    public GameObject CEggplantShape;
    public GameObject CMushroomShape;

    public PantryBehaviour pantryBehaviourScript;


    // ---------------------------------- MOUSE ------------------------------------------
    // Store Mouse position every frame
    public Vector3 mousePos;

    // Check if clicked (default = false)
    public bool isClicked = false;

    // Check if the ingredient has been clicked (default = false)
    public bool isIngrSelected = false;


    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    void Start()
    {
        // ---------------------------------- ACCESS --------------------------------------------
        // Access Ingredient chip sprite renderer
        ingredientChipRenderer = transform.parent.GetComponent<SpriteRenderer>();

        // Access the Pantry Behaviour script
        pantryBehaviourScript = FindObjectOfType<PantryBehaviour>();


        // ---------------------------------- ACTIVATION ----------------------------------------
        // Deactivate the renderer of the ingredient selected (as none has been selected for now)
        ingredientSelected.GetComponent<SpriteRenderer>().enabled = false;


        // ---------------------------------- SET ARRAYS ----------------------------------------
        // Add all the sprites to the Pantry's Ingredients (default) array
        pantryIngrDefault[0] = PTomato;
        pantryIngrDefault[1] = PCarrot;
        pantryIngrDefault[2] = PEggplant;
        pantryIngrDefault[3] = PMushroom;

        // Add all the sprites to the Pantry's Ingredients (blocked) array
        pantryIngrBlocked[0] = PTomatoBlocked;
        pantryIngrBlocked[1] = PCarrotBlocked;
        pantryIngrBlocked[2] = PEggplantBlocked;
        pantryIngrBlocked[3] = PMushroomBlocked;

        // Add all the sprites to the Pantry's Ingredients (default) array
        cursorIngrSelected[0] = CTomatoSelected;
        cursorIngrSelected[1] = CCarrotSelected;
        cursorIngrSelected[2] = CEggplantSelected;
        cursorIngrSelected[3] = CMushroomSelected;

        // Add all the ingredients' shapes to the Cursor array
        cursorIngrShape[0] = CTomatoShape;
        cursorIngrShape[1] = CCarrotShape;
        cursorIngrShape[2] = CEggplantShape;
        cursorIngrShape[3] = CMushroomShape;
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
        ingredientSelected.transform.position = mousePos + offsetMousePos1;

        // Set the position of the ingredient selected UI as the cursor's position + the offset position
        cIngredientShapes.transform.position = mousePos + offsetMousePos2;
    }


    // ---------------------------------- GO IS CLICKED -----------------------------------------------------
    void OnMouseDown()
    {
        // There has been a click, so activate it
        isClicked = true;

        // Active the renderer of the ingredient selected
        ingredientSelected.GetComponent<SpriteRenderer>().enabled = true;

        // Change the Ingredient Sprite
        IngredientSpriteChange();
    }


    // ---------------------------------- CHANGE INGREDIENT'S SPRITES ---------------------------------------
    void IngredientSpriteChange ()
    {
        // Loop to check which ingredient is being clicked
        for (int i = 0; i < 4; i = i + 1)
        {
            // If the ingredient clicked matches the ingredient of that round of the loop & it hasn't been selected before
            if (ingredientChipRenderer.sprite == pantryIngrDefault[i])// && pantryIngrDefault[i].GetComponent<Clicker>().isIngrSelected == false)
            { 
                if(ingredientChipRenderer.gameObject.transform.GetChild(0).gameObject.GetComponent<Clicker>().isIngrSelected == false)
                {
                    // Set the looped sprite as an ingredient selected
                    ingredientChipRenderer.gameObject.transform.GetChild(0).gameObject.GetComponent<Clicker>().isIngrSelected = true;

                    //Change the sprite for it's blocked one
                    ingredientChipRenderer.sprite = pantryIngrBlocked[i];

                    // Set the Ingredient selected UI to it's selected sprite
                    ingredientSelected.GetComponent<SpriteRenderer>().sprite = cursorIngrSelected[i];

                    // Active the loop shape's Game Object
                    cursorIngrShape[i].SetActive (true);

                    // Not clicked anymore
                    isClicked = false;

                    pantryBehaviourScript.DeactivateChilds(i);
                }
            }

        }
    }
}
